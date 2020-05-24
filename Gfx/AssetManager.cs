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

namespace GLGameJam
{
    public class AssetManager
    {
        private readonly ContentManager contentManager;

        public Texture2D MainTexture { get; private set; }
        public NinePatch NinePatch { get; private set; }

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
            MainTexture = contentManager.Load<Texture2D>("colored_transparent");
            CreateRegion("menu_patch", 663, 238, 16, 16);
            CreateRegion("gold", 697, 68, 16, 16);
            CreateRegion("exp", 561, 170, 16, 16);
            CreateRegion("mage", 408, 0, 16, 16);
            NinePatch = new NinePatch(Get("menu_patch"), 4, 4, 4, 4);

            var chars = new []
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', '.', '%',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '#', '+', '-', '*', '/', '=', '@',
            };

            // Load all characters
            const int startX = 595;
            const int startY = 289;
            const int sz = 16;

            for (var i = 0; i < chars.Length; ++i)
            {
                var dfX = i % 13;
                var dfY = i / 13;
                CreateRegion(chars[i].ToString(), startX + dfX * sz + dfX, startY + dfY * sz + dfY, sz, sz);
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
