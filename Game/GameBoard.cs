using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLGameJam.Gfx;
using GLGameJam.Input;
using GLJamGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GLGameJam
{
    public class GameBoard
    {
        public const int GameBoardStartX = CoreGame.GameSizeX / 2 - (GameBoardSizeX / 2) * 14 - 2;
        public const int GameBoardStartY = 10;
        public const int GameBoardSizeX = 12;
        public const int GameBoardSizeY = 8;
        public const int TileSpacing = 1;
        public const int TileSize = 14;

        private Card[] boardCards;

        private InputManager inputManager;

        public GameBoard(InputManager inputManager)
        {
            this.inputManager = inputManager;
            this.boardCards = new Card[GameBoardSizeX * GameBoardSizeY];
            SetCard(0, 0, new Card(CardDefinitions.Mage));
        }

        public void Input()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(CustomBatch customBatch)
        {
            customBatch.SetOrigin(GameBoardStartX, GameBoardStartY);

            var mouseTile = WorldToScreen(inputManager.MousePosition.X, inputManager.MousePosition.Y);

            var yes = false;
            for (var ty = 0; ty < GameBoardSizeY; ++ty)
            {
                for (var tx = 0; tx < GameBoardSizeX; ++tx)
                {
                    var color = Colors.BoardColor;
                    
                    var type = !yes ? "tile_empty" : "tile_fill";

                    var card = GetCard(tx, ty);
                    
                    if(card != null)
                        color = Colors.EnemyBoardColor;

                    var wx = tx * TileSize + TileSpacing * tx;
                    var wy = ty * TileSize + TileSpacing * ty;

                    int offsetY = 0;
                    if (mouseTile == new Point(tx, ty))
                    {
                        offsetY = 4;
                        color = Colors.HoveredBoardColor;
                    }

                    customBatch.Draw(type, new Rectangle(wx, wy - offsetY, TileSize, TileSize), color);

                    card?.DrawInBoard(customBatch, wx - 1, wx - 6 - offsetY);

                    yes = !yes;
                }

                yes = !yes;
            }

            customBatch.SetOrigin(0, 0);

            //customBatch.DrawPixelString(new Vector2(0, 12), $"{mouseTile.X} - {mouseTile.Y}", Color.White);

        }

        public bool IsFree(int x, int y)
        {
            return boardCards[x + y * GameBoardSizeX] == null;
        }

        public Card GetCard(int x, int y)
        {
            if (x < 0 || y < 0 || x >= GameBoardSizeX || y >= GameBoardSizeY)
                return null;
            return boardCards[x + y * GameBoardSizeX];
        }

        public void SetCard(int x, int y, Card card)
        {
            if (x < 0 || y < 0 || x >= GameBoardSizeX || y >= GameBoardSizeY)
                return;
            boardCards[x + y * GameBoardSizeX] = card;
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
    }
}
