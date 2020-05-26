using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.UI;
using GLJamGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GLGameJam
{
    public class AssetManager
    {
        private readonly ContentManager contentManager;

        public Texture2D MainTexture { get; private set; }
        public NinePatch NinePatch { get; private set; }
        public NinePatch ButtonNinePatch { get; private set; }

        private readonly Dictionary<string, Rectangle> rects;

        private readonly Dictionary<string, object> objects;

        public AssetManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            rects = new Dictionary<string, Rectangle>();
            objects = new Dictionary<string, object>();
        }

        public void LoadContent()
        {
            MainTexture = contentManager.Load<Texture2D>("spritesheet");
            CreateRegion("pointer", 647, 171, 7, 12);
            CreateRegion("menu_patch", 0, 0, 16, 16);
            CreateRegion("button_patch", 16, 0, 16, 16);
            CreateRegion("gold", 0, 16, 16, 16);
            CreateRegion("exp", 16, 16, 16, 16);
            CreateRegion("mage", 0, 64, 16, 16);
            CreateRegion("warrior", 16, 64, 16, 16);
            CreateRegion("chicken", 32, 64, 16, 16);
            CreateRegion("tile_empty", 33, 1, 14, 14);
            CreateRegion("tile_fill", 49, 1, 14, 14);

            // icons
            CreateRegion("coin_icon", 37, 21, 6, 6);
            CreateRegion("mage_icon", 48, 16, 16, 16);
            CreateRegion("ranged_icon", 64, 16, 16, 16);
            CreateRegion("melee_icon", 80, 16, 16, 16);
            CreateRegion("armor_icon", 112, 16, 16, 16);
            /*CreateRegion("attack_icon", 547, 105, 10, 10);
            CreateRegion("health_icon", 666, 174, 10, 10);
            CreateRegion("armor_icon", 546, 19, 12, 12);*/

            CreateRegion("player_board_tile", 66, 2, 12, 12);

            NinePatch = new NinePatch(Get("menu_patch"), 4, 4, 4, 4);
            ButtonNinePatch = new NinePatch(Get("button_patch"), 4, 4, 4, 4);

            var chars = new []
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', '.', '/',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '#', '+', '-', '*', '_', '=', '[', ']', '€', '£'
            };

            // Load normal (16x16)
            int startX = 0;
            int startY = 416;
            int sz = 16;

            for (var i = 0; i < chars.Length; ++i)
            {
                var dfX = i % 13;
                var dfY = i / 13;
                CreateRegion(chars[i].ToString(), startX + dfX * sz, startY + dfY * sz , sz, sz);

            }

            // Load small font (8x8)
            startX = 0;
            startY = 480;
            sz = 8;

            for (var i = 0; i < chars.Length; ++i)
            {
                var dfX = i % 13;
                var dfY = i / 13;
                CreateRegion(chars[i] + "_small", startX + dfX * sz, startY + dfY * sz, sz, sz);
            }
        }

        private void CreateRegion(string key, int x, int y, int width, int height)
        {
            if (string.IsNullOrEmpty(key) || rects.ContainsKey(key))
                return;
            rects.Add(key, new Rectangle(x, y, width, height));
        }

        private void LoadAsset<T>(string key, string name) where T : class, new()
        {
            if (objects.ContainsKey(key))
                return;
            objects.Add(key, contentManager.Load<T>(name));
        }

        public T Get<T>(string t) where T: class, new()
        {
            if (objects.TryGetValue(t, out var value))
            {
                return (T) value;
            }

            return null;
        }

        public Rectangle Get(string t)
        {
            if (rects.TryGetValue(t, out var value))
            {
                return value;
            }
            return new Rectangle();
        }
    }
}
