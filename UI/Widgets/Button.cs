using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI.Widgets
{
    public class Button : Widget
    {
        public Action OnClick { get; set; }
        public Action OnHover { get; set; }
        public Action OnRelease { get; set; }

        public string Text { get; set; }

        public Color TextColor { get; set; }

        private NinePatch ninePatch;

        public Button(string text, Point pos, Color backgroundColor, Color textColor) : base(pos, new Point())
        {
            Text = text;
            Color = backgroundColor;
            TextColor = textColor;
            Size = new Point(text.Length * 6 + 4, 16);
        }

        public override void LoadContent(AssetManager assetManager)
        {
            ninePatch = new NinePatch(assetManager.Get("button_patch"), 4, 4, 4, 4);
        }

        public override void Draw(CustomBatch customBatch)
        {
            ninePatch.Draw(customBatch, new Rectangle(Position, Size), Color);
            customBatch.DrawPixelString(new Vector2(Position.X + 4, Position.Y + Size.Y / 2 - 4), Text, TextColor, FontSize.Small);
        }
    }
}
