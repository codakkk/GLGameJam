using System;
using System.Collections.Generic;
using System.Text;
using GLGameJam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GLJamGame
{

    public enum FontSize
    {
        Small,
        Medium,
        Normal
    }

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

        public void Draw(Rectangle source, Rectangle destination, Color color)
        {
            var (x, y, width, height) = destination;
            var rect = new Rectangle(originX + x, originY + y, width, height);
            Draw(AssetManager.MainTexture, rect, source, Color.White);
        }

        public void Draw(string key, Rectangle destination)
        {
            destination = new Rectangle(originX + destination.X, originY + destination.Y, destination.Width, destination.Height);
            Draw(AssetManager.MainTexture, destination, AssetManager.Get(key), Color.White);
        }

        public void Draw(string key, Rectangle destination, Color color)
        {
            destination = new Rectangle(originX + destination.X, originY + destination.Y, destination.Width, destination.Height);
            Draw(AssetManager.MainTexture, destination, AssetManager.Get(key), color);
        }

        public void Draw(string key, Vector2 position, Vector2 size, Color color)
        {
            var scale = new Vector2(1.0f / size.X, 1.0f / size.Y);
            Draw(AssetManager.MainTexture, new Vector2(originX, originY) + position, AssetManager.Get(key), color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
        }

        public void Draw(string key, int x, int y)
        {
            var rect = AssetManager.Get(key);
            Draw(AssetManager.MainTexture, new Vector2(originX + x, originY + y), rect, Color.White);
        }

        public void Draw(string key, int x, int y, Color color)
        {
            var rect = AssetManager.Get(key);
            Draw(AssetManager.MainTexture, new Vector2(originX + x, originY + y), rect, color);
        }

        public void DrawNineRect(int x, int y, int width, int height, Color color)
        {
            AssetManager.NinePatch.Draw(this, new Rectangle(x, y, width, height), color);
        }

        public void DrawPixelString(Vector2 position, string text, Color color, FontSize fontSize = FontSize.Normal)
        {
            position = new Vector2(originX + position.X, originY + position.Y);

            var sz = 8;
            var spacing = 2f;
            var suffix = "";
            if (fontSize == FontSize.Small)
            {
                sz = 4;
                spacing = 1f;
                suffix = "_small";
            }

            var meGarbage = text.ToUpper();
            for (var i = 0; i < meGarbage.Length; ++i)
            {
                var rect = AssetManager.Get(meGarbage[i] + suffix);

                Draw(AssetManager.MainTexture, position + new Vector2(sz * i, 0) + Vector2.UnitX * spacing * i, rect, color);
            }
        }

        public void SetOrigin(int x, int y)
        {
            this.originX = x;
            this.originY = y;
        }

        public Point GetOrigin()
        {
            return new Point(originX, originY);
        }
    }
}
