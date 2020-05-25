using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI.Widgets
{
    public class ImageWidget : Widget
    {
        public string ResourceName { get; set; }

        public ImageWidget(string resourceName, Point position) : base(position, new Point())
        {
            this.ResourceName = resourceName;
        }

        public override void Draw(CustomBatch customBatch)
        {
            customBatch.Draw(this.ResourceName, 0, 0, Color);
        }
    }
}
