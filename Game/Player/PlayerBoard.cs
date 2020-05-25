using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Player;
using GLGameJam.Screens;
using GLGameJam.UI;
using GLGameJam.UI.Widgets;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.Game.Player
{
    public class PlayerBoard
    {
        private PlayerResources playerResources;

        private Point position;

        public PlayerBoard(PlayerResources player, Point position)
        {
            this.playerResources = player;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(CustomBatch customBatch)
        {
            customBatch.SetOrigin(position.X, position.Y);
            for (int i = 0; i < GameScreen.MaxPlayerCards; ++i)
            {
                customBatch.Draw("player_board_tile", i * GameBoard.TileSize, 0);

                var card = playerResources.PlayerCards[i];
                if (card?.CardDefinition != null)
                {
                    card.DrawInList(customBatch, -2 + i * GameBoard.TileSize, -8);
                }
            }
        }
    }
}
