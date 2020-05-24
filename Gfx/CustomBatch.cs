using System;
using System.Collections.Generic;
using System.Text;
using GLGameJam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GLJamGame
{
    public class CustomBatch : SpriteBatch
    {
        public AssetManager AssetManager { get; }

        private int originX, originY;

        public CustomBatch(GraphicsDevice gd, AssetManager assetManager) : base(gd)
        {
            this.AssetManager = assetManager;
        }

        public void BeginPixel()
        {
            Begin(sortMode: SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
        }

        // This is not translated by origin
        public void Draw(Rectangle source, Rectangle destination)
        {
            Draw(AssetManager.MainTexture, destination, source, Color.White);
        }

        public void Draw(string key, Rectangle destination)
        {
            destination = new Rectangle(originX + destination.X, originY + destination.Y, destination.Width, destination.Height);
            Draw(AssetManager.MainTexture, destination, AssetManager.Get(key), Color.White);
        }

        public void Draw(string key, int x, int y)
        {
            var rect = AssetManager.Get(key);
            Draw(AssetManager.MainTexture, new Vector2(originX + x, originY + y), rect, Color.White);
        }

        public void DrawNineRect(int x, int y, int width, int height)
        {
            AssetManager.NinePatch.Draw(this, new Rectangle(originX + x, originY + y, width, height));
        }

        public void DrawPixelString(Vector2 position, string text, Color color)
        {
            position = new Vector2(originX + position.X, originY + position.Y);

            const int sz = 8;
            const float spacing = 2f;

            var meGarbage = text.ToUpper();
            for (var i = 0; i < meGarbage.Length; ++i)
            {
                var rect = AssetManager.Get(meGarbage[i].ToString());

                Draw(AssetManager.MainTexture, position + new Vector2(sz * i, 0) + Vector2.UnitX * spacing * i, rect, color);
            }
        }

        public void SetOrigin(int x, int y)
        {
            this.originX = x;
            this.originY = y;
        }
    }
}
