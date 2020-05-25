using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLGameJam.Game.Player;
using GLGameJam.Input;
using GLGameJam.Player;
using GLGameJam.UI;
using GLJamGame;
using GLJamGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GLGameJam.Screens
{
    public class GameScreen : BaseScreen
    {

        public const int ShopRefreshPrice = 2;
        public const int ShopExpPrice = 2;

        public const int MaxPlayerCards = 8;

        public PlayerResources PlayerResources { get; }
        public PlayerBoard PlayerBoard { get; }
        public InputManager InputManager { get; }
        public Shop Shop { get; }
        public GameBoard GameBoard { get; }
        
        
        
        public GameScreen(ScreenManager screenManager) : base(screenManager)
        {
            this.PlayerResources = new PlayerResources();
            this.InputManager = new InputManager();
            this.Shop = new Shop(PlayerResources, InputManager);
            this.GameBoard = new GameBoard(InputManager);

            var playerBoardPosition = new Point(GameBoard.GameBoardStartX + GameBoard.TileSize * GameScreen.MaxPlayerCards/4,
                GameBoard.GameBoardStartY + GameBoard.GameBoardSizeY * GameBoard.TileSize +
                GameBoard.TileSpacing * GameBoard.GameBoardSizeY + GameBoard.TileSize * 1);
            this.PlayerBoard = new PlayerBoard(PlayerResources, playerBoardPosition);
            PlayerResources.Gold = 999;
            PlayerResources.Exp = 0;
            PlayerResources.Level = 1;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            this.Shop.LoadContent(assetManager);
        }

        public override void Input()
        {
            Shop.Input(InputManager);
            GameBoard.Input();
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);

            GameBoard.Update(gameTime);
            PlayerBoard.Update(gameTime);

            Shop.Update(gameTime);
        }

        public override void Draw(CustomBatch customBatch)
        {
            this.GameBoard.Draw(customBatch);
            this.PlayerBoard.Draw(customBatch);
            this.Shop.Draw(customBatch);

            customBatch.SetOrigin(8, 8);

            customBatch.SetOrigin(0, 0);
            InputManager.DrawDebug(customBatch);
        }
    }
}
