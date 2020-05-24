using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLGameJam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GLJamGame.Screens
{
    public class ScreenManager
    {

        private AssetManager assetManager;

        private readonly Stack<BaseScreen> screens;

        private BaseScreen toPush;

        public ScreenManager(AssetManager assetManager)
        {
            this.assetManager = assetManager;
            this.screens = new Stack<BaseScreen>(1);
        }

        public void Draw(CustomBatch batch)
        {
            foreach (var screen in screens)
            {
                screen.Draw(batch);
            }
        }

        public void Update(GameTime gameTime)
        {
            var screen = screens.Peek();

            if (screen != null)
            {
                screen.Input();
                screen.Update(gameTime);
            }

            if (toPush != null)
            {
                this.screens.Push(toPush);
                
                toPush.LoadContent(assetManager);
                toPush.OnEnter();

                toPush = null;
            }
        }

        public bool Push(BaseScreen baseScreen, bool nextFrame = false)
        {
            if (baseScreen == null)
                return false;
            this.screens.Push(baseScreen);

            if (nextFrame)
            {
                toPush = baseScreen;
            }
            else
            {
                baseScreen.LoadContent(assetManager);
                baseScreen.OnEnter();
            }
            return true;
            
        }

        public bool Set(BaseScreen baseScreen)
        {
            if (baseScreen == null)
                return false;
            foreach (var screen in screens)
            {
                screen.OnExit();
            }
            
            this.screens.Clear();

            this.screens.Push(baseScreen);

            baseScreen.LoadContent(assetManager);
            baseScreen.OnEnter();
            return true;
        }
    }
}
