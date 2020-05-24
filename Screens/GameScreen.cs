using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public PlayerResources PlayerResources { get; private set; }

        public InputManager InputManager { get; private set; }
        
        public Shop Shop { get; private set; }

        public GameScreen(ScreenManager screenManager) : base(screenManager)
        {
            this.PlayerResources = new PlayerResources();
            this.InputManager = new InputManager();
            this.Shop = new Shop(PlayerResources);
            
        }

        public override void LoadContent(AssetManager assetManager)
        {
            this.Shop.LoadContent(assetManager);
        }

        public override void Input()
        {
            Shop.Input(InputManager);
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
        }

        public override void Draw(CustomBatch customBatch)
        {
            this.Shop.Draw(customBatch);
            string playerLevel = null;
            if (PlayerResources.Level == PlayerResources.MaxLevel)
            {
                playerLevel = "Livello Max.";
            }
            else
            {
                playerLevel = $"Livello {PlayerResources.Level}";
            }
            customBatch.DrawPixelString(new Vector2(160 - 7 * 8, 0), playerLevel, Color.Yellow);
            customBatch.DrawPixelString(Vector2.UnitY * 24, "Sono un test e tu mi devi guardare", Color.White);
        }
    }
}
