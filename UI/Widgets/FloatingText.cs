using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI.Widgets
{
    public class FloatingText : TextWidget
    {

        private float ySpace;
        private float lifeTime;

        public FloatingText(Point position, string text) : base(position, text, GLJamGame.FontSize.Small)
        {
            Color = Color.White;
            Reset();
        }

        private void Reset()
        {
            IsVisible = false;
            lifeTime = 255.0f;
            ySpace = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsVisible)
                return;
            base.Update(gameTime);

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.lifeTime -= 255 * delta;

            if (lifeTime <= 0.0f)
            {
                Reset();
            }

            ySpace -= (10.0f * delta);
        }

        public override void Draw(CustomBatch customBatch)
        {
            var lengthPx = (Text.Length/2) * 5;
            customBatch.DrawPixelString(new Vector2(-lengthPx, ySpace), Text, new Color(Color, lifeTime), FontSize);
        }
    }
}
