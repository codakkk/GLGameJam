using System;
using GLGameJam;
using GLGameJam.Gfx;
using GLGameJam.Screens;
using GLJamGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GLJamGame
{
    public class CoreGame : Game
    {

        #region Constants

        public const int GameSizeX = 480;//384;
        public const int GameSizeY = 270;//216;

        public static int WindowSizeX = 1280;
        public static int WindowSizeY = 720;

        #endregion

        public static int FramesPerSecond = 0;

        public static float GameScaleX => (float)GameSizeX / WindowSizeX;
        public static float GameScaleY => (float)GameSizeY / WindowSizeY;



        private RenderTarget2D renderTarget;

        #region Publics
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public CustomBatch CustomBatch { get; private set; }
        public ScreenManager ScreenManager { get; private set; }
        public AssetManager AssetManager { get; private set; }
        #endregion

        public CoreGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = WindowSizeX,
                PreferredBackBufferHeight = WindowSizeY,
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnWindowSizeChanged;
            Mouse.WindowHandle = Window.Handle;
        }

        private void OnWindowSizeChanged(object sender, EventArgs e)
        {
            WindowSizeX = Window.ClientBounds.Width;
            WindowSizeY = Window.ClientBounds.Height;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetManager = new AssetManager(Content);
            AssetManager.LoadContent();

            CustomBatch = new CustomBatch(GraphicsDevice, AssetManager);

            this.renderTarget = new RenderTarget2D(GraphicsDevice, GameSizeX, GameSizeY);


            ScreenManager = new ScreenManager(AssetManager);

            ScreenManager.Push(new GameScreen(ScreenManager), true);
        }

        protected override void Update(GameTime gameTime)
        {
            FramesPerSecond = (int) (1.0f / gameTime.ElapsedGameTime.TotalSeconds);

            Window.Title = $"Coda-Chess SP - FPS: {FramesPerSecond}";

            ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            // First -> draw on RenderTarget
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Colors.BackgroundColor);
            CustomBatch.BeginPixel();
            ScreenManager.Draw(CustomBatch);
            CustomBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            CustomBatch.BeginPixel();
            CustomBatch.Draw(renderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            CustomBatch.End();
            base.Draw(gameTime);
        }
    }
}
