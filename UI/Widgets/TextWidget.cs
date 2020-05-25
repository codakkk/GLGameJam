using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI.Widgets
{
    public class TextWidget : Widget
    {
        public string Text { get; set; }
        public FontSize FontSize { get; set; }

        public TextWidget(Point position, string text, FontSize fontSize = FontSize.Normal) : base(position, new Point())
        {
            this.Text = text;
            this.FontSize = fontSize;
        }

        public override void Draw(CustomBatch customBatch)
        {
            customBatch.DrawPixelString(Vector2.Zero, Text, Color, FontSize);
        }
    }
}
