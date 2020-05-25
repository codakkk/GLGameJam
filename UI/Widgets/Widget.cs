using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI.Widgets
{
    public abstract class Widget
    {
        public Point Position { get; set; }
        public Point Size { get; set; }

        public Color Color { get; set; }

        protected Widget(Point position, Point size)
        {
            this.Position = position;
            this.Size = size;
        }

        public virtual void LoadContent(AssetManager assetManager)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(CustomBatch customBatch)
        {
        }
    }
}
