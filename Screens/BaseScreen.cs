using GLGameJam;
using GLJamGame.Screens;
using Microsoft.Xna.Framework;

namespace GLJamGame
{
    public abstract class BaseScreen
    {
        protected ScreenManager ScreenManager { get; }

        protected BaseScreen(ScreenManager screenManager)
        {
            this.ScreenManager = screenManager;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        public virtual void LoadContent(AssetManager assetManager) { }

        public virtual void Input()
        {
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(CustomBatch customBatch) { }

        public virtual void Dispose() { }
    }
}
