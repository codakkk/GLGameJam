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
        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (!value)
                    IsHovered = false;
                isVisible = value;
            }
        }

        private Point position;
        public Point Position
        {
            get
            {
                var result = position;
                if (Parent != null && ParentAsOrigin)
                {
                    result += Parent.Position;
                }

                return result;
            }
            set => position = value;
        }
        public Point Size { get; set; }
        public Color Color { get; set; } = Color.White;

        public bool IsHovered { get; set; }

        private Widget parent;
        public Widget Parent
        {
            get => parent;
            set
            {
                if (value == this)
                    return;
                parent = value;
            }
        }

        public Rectangle Bounds => new Rectangle(Position, Size);

        public bool ParentAsOrigin { get; set; } = true;

        public Action OnPress { get; set; }
        public Action OnHoverEnter { get; set; }
        public Action OnHoverMoved { get; set; }
        public Action OnHoverExit { get; set; }

        protected Widget(Point position, Point size)
        {
            this.Position = position;
            this.Size = size;
            this.IsVisible = true;
        }

        public virtual void LoadContent(AssetManager assetManager)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public abstract void Draw(CustomBatch customBatch);
    }
}
