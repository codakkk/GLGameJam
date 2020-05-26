using System;
using System.Collections.Generic;
using GLGameJam.Gfx;
using GLGameJam.Input;
using GLGameJam.Screens;
using GLGameJam.UI;
using GLGameJam.UI.Widgets;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.Game
{

    public class CardPopup : BaseContainer
    {
        private Card card;

        public Card Card
        {
            get => card;
            set
            {
                card = value;

                IsVisible = card != null;

                if (card == null) 
                    return;

                atkText.Text = "ATK " + card.Attack;
                defText.Text = "DEF " + card.Attack;
                hpText.Text = "HP  " + card.Attack;
                titleText.Text = card.CardDefinition.Name;
            }
        }

        private readonly TextWidget titleText;
        private readonly TextWidget atkText;
        private readonly TextWidget defText;
        private readonly TextWidget hpText;

        public CardPopup() : base(Point.Zero, new Point(38, 34))
        {
            ParentAsOrigin = false;

            titleText = new TextWidget(new Point(0, -6), "NAME", FontSize.Small);
            AddWidget(titleText);

            atkText = new TextWidget(new Point(2, 2), "ATK", FontSize.Small);
            AddWidget(atkText);

            defText = new TextWidget(new Point(2, 12), "DEF", FontSize.Small);
            AddWidget(defText);

            hpText = new TextWidget(new Point(2, 21), "HP", FontSize.Small);
            AddWidget(hpText);
        }

        public override void Draw(CustomBatch customBatch)
        {
            customBatch.DrawNineRect(0, 0, Size.X, Size.Y, Color.White);
            base.Draw(customBatch);

        }
    }

    internal struct CardDragData
    {
        public Card Card { get; set; }
        public BoardTile RelativeTo { get; set; }
    }

    public class GameBoard : BaseContainer
    {
        public const int GameBoardStartX = CoreGame.GameSizeX / 2 - (GameBoardSizeX / 2) * 14 + 4;
        public const int GameBoardStartY = 40;
        public const int GameBoardSizeX = 12;
        public const int GameBoardSizeY = 8;
        public const int TileSpacing = 1;
        public const int TileSize = 16;

        private readonly Player player;
        private readonly InputManager inputManager;


        private List<BoardTile> allBoardCards;
        private readonly BoardTile[] boardCards;
        private BoardTile[] playerBoardCards;

        private CardDragData cardDragData;

        private CardPopup cardPopup;

        public GameBoard(Player player, InputManager inputManager) : base(new Point(GameBoardStartX, GameBoardStartY), new Point(GameBoardSizeX * TileSize + TileSpacing * GameBoardSizeY, GameBoardSizeY * TileSize + TileSpacing * GameBoardSizeY))
        {
            this.player = player;
            this.inputManager = inputManager;
            
            this.allBoardCards = new List<BoardTile>(GameBoardSizeX * GameBoardSizeY + Player.MaxBenchCards);
            this.boardCards = new BoardTile[GameBoardSizeX * GameBoardSizeY];
            this.playerBoardCards = new BoardTile[Player.MaxBenchCards];
            
            this.cardPopup = new CardPopup { IsVisible = false };

            this.player.OnAddCard += (i, card) =>
            {
                playerBoardCards[i].Card = card;
            };

            this.player.OnRemoveCard += (card) =>
            {
                var boardCard = allBoardCards.Find(bc => bc.Card == card);
                if (boardCard != null)
                {
                    boardCard.Card = null;
                }
            };
        }

        public override void LoadContent(AssetManager assetManager)
        {
            var fill = false;
            for (var y = 0; y < GameBoardSizeY; ++y)
            {
                for (var x = 0; x < GameBoardSizeX; ++x)
                {
                    var boardCard = new BoardTile(BoardType.Field, fill)
                    {
                        Position = new Point(x * TileSize + TileSpacing * x, y * TileSize + TileSpacing * y),
                        Color = Colors.BoardColor
                    };

                    boardCards[x + y * GameBoardSizeX] = boardCard;

                    AddWidget(boardCard);
                    fill = !fill;
                }
                fill = !fill;
            }

            for (var i = 0; i < Player.MaxBenchCards; ++i)
            {
                var (sx, sy) = new Point(TileSize * Player.MaxBenchCards / 4,
                    GameBoardSizeY * TileSize +
                    TileSpacing * GameBoardSizeY + TileSize * 1);

                var boardCard = new BoardTile(BoardType.Player, false)
                {
                    Position = new Point(sx + i * TileSize + TileSpacing * i, sy),
                    Color = Color.White
                };

                playerBoardCards[i] = boardCard;

                AddWidget(boardCard);
            }

            cardPopup.LoadContent(assetManager);
            AddWidget(cardPopup);

            allBoardCards.AddRange(playerBoardCards);
            allBoardCards.AddRange(boardCards);

            foreach (var boardCard in allBoardCards)
            {
                boardCard.OnHoverEnter += () =>
                {
                    if (boardCard.Card != null)
                        OnHoverCard(boardCard, boardCard.Card);
                };

                boardCard.OnHoverMoved += () =>
                {
                    if(boardCard.Card != null)
                        OnHoverCardMoved(boardCard, boardCard.Card);
                };

                boardCard.OnHoverExit += () =>
                {
                    OnHoverCard(boardCard, null);
                };

                boardCard.OnPress += () =>
                {
                    if (boardCard.Card != null)
                    {
                        if (cardDragData.Card != null)
                        {
                            var temp = cardDragData.Card;

                            cardDragData.Card = boardCard.Card;
                            cardDragData.RelativeTo = boardCard;

                            boardCard.Card = temp;
                        }
                        else
                        {
                            cardDragData.Card = boardCard.Card;
                            cardDragData.RelativeTo = boardCard;
                            boardCard.Card = null;
                        }
                    }
                    else
                    {
                        if (cardDragData.Card != null)
                        {
                            boardCard.Card = cardDragData.Card;
                            cardDragData = new CardDragData();
                        }
                    }
                };
            }
        }

        public void Input()
        {
            base.Input(inputManager);
        }

        public override void Draw(CustomBatch customBatch)
        {
            base.Draw(customBatch);

            customBatch.SetOrigin(0, 0);
            if (cardDragData.Card != null)
            {
                customBatch.Draw(cardDragData.Card.CardDefinition.SourceName, inputManager.MousePosition.X-8, inputManager.MousePosition.Y-8, cardDragData.Card.Color);
            }
        }

        public bool IsFree(int x, int y)
        {
            return boardCards[x + y * GameBoardSizeX].Card == null;
        }

        public Card GetCard(int x, int y)
        {
            if (x < 0 || y < 0 || x >= GameBoardSizeX || y >= GameBoardSizeY)
                return null;
            return boardCards[x + y * GameBoardSizeX].Card;
        }

        public void SetCard(int x, int y, Card card)
        {
            if (x < 0 || y < 0 || x >= GameBoardSizeX || y >= GameBoardSizeY)
                return;
            boardCards[x + y * GameBoardSizeX].Card = card;
        }

        public Vector2 ScreenToWorld(int x, int y)
        {
            return new Vector2(GameBoardStartX + x * TileSize + x, GameBoardStartY + y * TileSize + y);
        }

        public Point WorldToScreen(float x, float y)
        {
            var gameBoardPosition = new Vector2(x - GameBoardStartX, y - GameBoardStartY);
            var rx = gameBoardPosition.X - TileSpacing * Math.Floor(gameBoardPosition.X / TileSize);
            var ry = gameBoardPosition.Y - TileSpacing * Math.Floor(gameBoardPosition.Y / TileSize) + 1;
            
            var tx = (int) rx / TileSize;
            var ty = (int) ry / TileSize;
            return new Point(tx, ty);//new Point((int) ((x) / TileSize), (int) ((y) / TileSize));
        }

        public void OnHoverCard(BoardTile boardTile, Card card)
        {
            if (card == null)
            {
                cardPopup.Card = null;
                return;
            }
            cardPopup.Position = new Point(inputManager.MousePosition.X - cardPopup.Size.X / 2, boardTile.Position.Y - cardPopup.Size.Y);
            cardPopup.Card = card;
        }

        public void OnHoverCardMoved(BoardTile boardTile, Card card)
        {
            cardPopup.Position = new Point(inputManager.MousePosition.X - cardPopup.Size.X / 2, boardTile.Position.Y - cardPopup.Size.Y);
        }
    }
}
