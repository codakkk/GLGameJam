using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GLGameJam.UI
{
    public class NinePatch
    {
        private int left;
        private int right;
        private int top;
        private int bot;

        private Rectangle source;
        private Rectangle[] sourcePatches;

        private Rectangle lastRectangle;
        private Rectangle[] destPatches;

        public NinePatch(Rectangle source, int left, int right, int top, int bot)
        {
            this.source = source;
            this.left = left;
            this.right = right;
            this.top = top;
            this.bot = bot;
            sourcePatches = GeneratePatches(source);
            destPatches = new Rectangle[9];
        }

        public void Draw(CustomBatch customBatch, Rectangle rectangle, Color color)
        {
            if (lastRectangle != rectangle)
            {
                destPatches = GeneratePatches(rectangle);
                lastRectangle = rectangle;
            }
            for(var i = 0; i < sourcePatches.Length; ++i)
            {
                customBatch.Draw(sourcePatches[i], destPatches[i], color);
            }
        }

        private Rectangle[] GeneratePatches(Rectangle source)
        {
            var sourceX = source.X;
            var sourceY = source.Y;
            var sourceW = source.Width;
            var sourceH = source.Height;
            var middleW = sourceW - left - right;
            var middleH = sourceH - top - bot;

            var topY = sourceY + top;
            var botY = sourceY + sourceH - bot;
            var leftX = sourceX + left;
            var rightX = sourceX + sourceW - right;
            return new[]
            {
                // Top
                new Rectangle(sourceX, sourceY, left, top),
                new Rectangle(leftX, sourceY, middleW, top),
                new Rectangle(rightX, sourceY, right, top),

                // Center   
                new Rectangle(sourceX, topY, left, middleH),
                new Rectangle(leftX, topY, middleW, middleH),
                new Rectangle(rightX, topY, right, middleH),

                // Bot
                new Rectangle(sourceX, botY, left, bot),
                new Rectangle(leftX, botY, middleW, bot),
                new Rectangle(rightX, botY, right, bot),
            };
        }
    }
}
