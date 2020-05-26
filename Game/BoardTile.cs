using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.UI.Widgets;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.Game
{

    public enum BoardType
    {
        Field,
        Player,
        Drag
    }

    // BoardTile just defines a "physical" card that is on PlayerBoard/GameBoard
    public class BoardTile : Widget
    {
        public Card Card { get; set; }
        public bool HasCard => Card != null;

        public BoardType BoardType { get; private set; }

        private bool fill;


        public BoardTile(BoardType boardType, bool fill = false) : base(Point.Zero, new Point(16, 18))
        {
            this.BoardType = boardType;
            this.fill = fill;
            this.Card = null;
        }

        public override void Draw(CustomBatch customBatch)
        {
            var offsetY = IsHovered && HasCard ? /*-4*/0 : 0;
            if (BoardType == BoardType.Field)
            {
                customBatch.Draw(fill ? "tile_fill" : "tile_empty", new Rectangle(0, 4 + offsetY, GameBoard.TileSize, GameBoard.TileSize), Color);
            }
            else if (BoardType == BoardType.Player)
            {
                customBatch.Draw("player_board_tile", 2, 8);
                //card.DrawInList(customBatch, -2 + i * GameBoard.TileSize, -8);
            }

            if (Card == null)
                return;

            if (BoardType == BoardType.Field)
            {
                customBatch.Draw(Card.CardDefinition.SourceName, 0, 0, Card.Color);
            }
            else if (BoardType == BoardType.Player)
            {
                customBatch.Draw(Card.CardDefinition.SourceName, 0, 0, Card.Color);
            }
        }
    }
}
