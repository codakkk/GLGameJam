using GLGameJam;
using GLGameJam.Screens;
using GLJamGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GLJamGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        CustomBatch customBatch;

        private RenderTarget2D renderTarget;

        public ScreenManager ScreenManager { get; private set; }
        public AssetManager AssetManager { get; private set; }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1280
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetManager = new AssetManager(Content);
            AssetManager.LoadContent();

            customBatch = new CustomBatch(GraphicsDevice, AssetManager);

            this.renderTarget = new RenderTarget2D(GraphicsDevice, 320, 180);


            ScreenManager = new ScreenManager(AssetManager);

            ScreenManager.Push(new GameScreen(ScreenManager), true);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            // First -> draw on RenderTarget
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(new Color(71, 45, 60));
            customBatch.BeginPixel();
            ScreenManager.Draw(customBatch);
            customBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            customBatch.BeginPixel();
            customBatch.Draw(renderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            customBatch.End();
            base.Draw(gameTime);
        }
    }
}
