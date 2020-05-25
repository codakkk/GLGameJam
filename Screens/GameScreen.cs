using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
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
        public InputManager InputManager { get; }
        public Shop Shop { get; }
        public GameBoard GameBoard { get; }
        
        
        
        public GameScreen(ScreenManager screenManager) : base(screenManager)
        {
            this.PlayerResources = new PlayerResources();
            this.InputManager = new InputManager();
            this.Shop = new Shop(PlayerResources, InputManager);
            this.GameBoard = new GameBoard(InputManager);
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

            PlayerResources.Update(gameTime);
        }

        public override void Draw(CustomBatch customBatch)
        {
            this.GameBoard.Draw(customBatch);
            this.Shop.Draw(customBatch);

            customBatch.SetOrigin(8, 8);
            this.PlayerResources.DrawSidebar(customBatch);
            customBatch.SetOrigin(0, 0);
            InputManager.DrawDebug(customBatch);
        }
    }
}
