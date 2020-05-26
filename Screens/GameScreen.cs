using GLGameJam.Game;
using GLGameJam.Input;
using GLGameJam.UI;
using GLJamGame;
using GLJamGame.Screens;
using Microsoft.Xna.Framework;

namespace GLGameJam.Screens
{
    public class GameScreen : BaseScreen
    {

        public const int ShopRefreshPrice = 2;
        public const int ShopExpPrice = 2;

        public Player Player { get; }
        public InputManager InputManager { get; }
        public Shop Shop { get; }

        public GameBoard GameBoard { get; }
        
        public BoardTile CurrentDragged { get; set; }
        
        public GameScreen(ScreenManager screenManager) : base(screenManager)
        {
            this.Player = new Player();
            this.InputManager = new InputManager();
            this.Shop = new Shop(Player, InputManager);

            this.GameBoard = new GameBoard(Player, InputManager);

            Player.Gold = 999;
            Player.Exp = 0;
            Player.Level = 1;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            this.Shop.LoadContent(assetManager);
            this.GameBoard.LoadContent(assetManager);
        }

        public override void Input()
        {
            Shop.Input(InputManager);
            GameBoard.Input();

            if (InputManager.IsActionJustDown("click"))
            {

            }
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);

            GameBoard.Update(gameTime);

            Shop.Update(gameTime);
        }

        public override void Draw(CustomBatch customBatch)
        {
            this.GameBoard.Draw(customBatch);

            this.Shop.Draw(customBatch);

            customBatch.SetOrigin(8, 8);

            customBatch.SetOrigin(0, 0);
            InputManager.DrawDebug(customBatch);
        }
    }
}
