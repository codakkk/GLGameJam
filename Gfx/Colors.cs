using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GLGameJam.Gfx
{
    public static class Colors
    {
        public static Color BackgroundColor = new Color(0, 0, 0);
        public static Color BoardColor = new Color(71, 45, 60); //new Color(244, 180, 27);
        public static Color ExpColor = new Color(60, 172, 215);
        public static Color EnemyBoardColor = new Color(145, 30, 53);
        public static Color HoveredBoardColor = new Color(244, 180, 27);
        public static Color ErrorColor = new Color(145, 30, 53);

        public static Color LevelOneCard = new Color(60, 172, 215);
        public static Color LevelTwoCard = new Color(60, 120, 215);
        public static Color LevelThreeCard = new Color(60, 0, 215);
    }
}
