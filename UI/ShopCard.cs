using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLGameJam.UI.Widgets;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI
{
    public class ShopCard : Widget
    {
        public const int CardSizeX = 36;
        public const int CardSizeY = 32;

        public CardDefinition CardDefinition { get; set; }

        public bool CanAfford { get; set; }

        public ShopCard(Point position) : base(position, new Point(CardSizeX, CardSizeY))
        {
        }


        public override void Draw(CustomBatch customBatch)
        {
            if (CardDefinition == null)
                return;
            if (IsHovered)
            {
                const int offsetY = 2;
                const int offsetX = 2;
                customBatch.DrawNineRect(0, -offsetY, ShopCard.CardSizeX, ShopCard.CardSizeY, Color.White);
                customBatch.DrawPixelString(new Vector2(offsetX, 4 - offsetY), "ATK " + CardDefinition.Attack, Color.White, FontSize.Small);
                customBatch.DrawPixelString(new Vector2(offsetX, 12 - offsetY), "DEF " + CardDefinition.Armor, Color.White, FontSize.Small);
                customBatch.DrawPixelString(new Vector2(offsetX, 20 - offsetY), "HP " + CardDefinition.Health, Color.White, FontSize.Small);
            }
            else
            {
                customBatch.DrawNineRect(0, 0, ShopCard.CardSizeX, ShopCard.CardSizeY, Color.White);
                customBatch.Draw(CardDefinition.SourceName, new Rectangle(ShopCard.CardSizeX / 2 - 8, 12, 16, 16), Color.White);

                var priceColor = Color.White;

                if (!CanAfford)
                    priceColor = Color.DarkRed;
                
                //customBatch.Draw("coin_icon", new Rectangle(4, 4, 6, 6), priceColor);
                customBatch.DrawPixelString(new Vector2(2, 2), "€ " + CardDefinition.Gold, priceColor, FontSize.Small);
            }
            //customBatch.Draw("melee_icon", ShopCard.CardSizeX - 12, -4);

            //if (playerResources.Gold < CardDefinition.Gold)
                //priceColor = Color.DarkRed;

        }
    }
}
