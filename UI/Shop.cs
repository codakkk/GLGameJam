using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Input;
using GLGameJam.Player;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI
{
    public struct ShopCard
    {
        public string name;
    }

    public class Shop
    {

        private const int shopX = 0;
        private const int shopY = 135;
        private const int shopWidth = 320;
        private const int shopHeight = 45;

        private const int cardSize = 24;

        private ShopCard[] currentShopCards;

        private PlayerResources playerResources;

        public Shop(PlayerResources playerResources)
        {
            this.playerResources = playerResources;
            currentShopCards = new []
            {
                new ShopCard() { name = "mage"},
                new ShopCard() { name = "mage"},
                new ShopCard() { name = "mage"},
                new ShopCard() { name = "mage"},
                new ShopCard() { name = "mage"},
            };

        }

        public void LoadContent(AssetManager assetManager)
        {

        }

        public void Input(InputManager inputManager)
        {
            if (inputManager.IsActionJustDown("shop_buyexp"))
            {
                playerResources.Exp++;
            }
        }

        private void DrawCards(CustomBatch customBatch)
        {
            const int cardX = 16 + 16 + 8 * 5;
            const int cardY = 5+ (shopHeight / 2) - 16;
            for (var i = 0; i < 5; ++i)
            {
                customBatch.DrawNineRect(cardX + 26 * i, cardY, cardSize, cardSize);
                customBatch.Draw(currentShopCards[i].name, new Rectangle(cardX + 26 * i + 4, cardY + 4, 16, 16));
            }
        }

        public void Draw(CustomBatch customBatch)
        {
            customBatch.SetOrigin(shopX, shopY);

            customBatch.DrawNineRect(0, 0, shopWidth, shopHeight);

            const int goldX = 3;
            const int goldY = 5;
            customBatch.Draw("gold", goldX, goldY);
            customBatch.DrawPixelString(new Vector2(goldX + 13, goldY), playerResources.Gold.ToString(), Color.White);

            string expStr = null;
            if (playerResources.Level == PlayerResources.MaxLevel)
            {
                expStr = "Max.";
            }
            else
            {
                expStr = $"{playerResources.Exp}%{playerResources.NextExp}";
            }

            customBatch.Draw("exp", goldX, goldY + 16);
            customBatch.DrawPixelString(new Vector2(goldX + 13, goldY + 16), expStr, Color.White);

            DrawCards(customBatch);

            customBatch.SetOrigin(0, 0);
        }

    }
}
