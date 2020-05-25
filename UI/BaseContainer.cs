using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Input;
using GLGameJam.UI.Widgets;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI
{
    public abstract class BaseContainer : Widget
    {
        private List<Widget> Widgets { get; }

        protected BaseContainer(Point position, Point size) : base(position, size)
        {
            Widgets = new List<Widget>();
        }

        public virtual void Input(InputManager inputManager)
        {
            var mousePosition = inputManager.MousePosition;
            foreach(var widget in Widgets)
            {
                if (!widget.IsVisible)
                    continue;
                if (widget.Bounds.Contains(mousePosition))
                {
                    if (!widget.IsHovered)
                    {
                        widget.IsHovered = true;
                        widget.OnHoverEnter?.Invoke();
                    }
                    if (inputManager.IsActionJustDown("click"))
                        widget.OnPress?.Invoke();
                }
                else if (widget.IsHovered)
                {
                    widget.IsHovered = false;
                    widget.OnHoverExit?.Invoke();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var widget in Widgets)
            {
                if (!widget.IsVisible)
                    continue;
                widget.Update(gameTime);
            }
        }

        public override void Draw(CustomBatch customBatch)
        {
            foreach(var widget in Widgets)
            {
                if (!widget.IsVisible)
                    continue;
                customBatch.SetOrigin(widget.Position.X, widget.Position.Y);
                widget.Draw(customBatch);
            }
        }

        public void AddWidget(Widget widget)
        {
            if (widget == null || widget == this)
                return;
            widget.Parent = this;
            Widgets.Add(widget);
        }

        public void RemoveWidget(Widget widget)
        {
            if (widget == null)
                return;
            if (Widgets.Remove(widget))
            {
                widget.Parent = null;
            }
        }
    }
}
