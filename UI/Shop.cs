using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLGameJam.Gfx;
using GLGameJam.Input;
using GLGameJam.Player;
using GLGameJam.Screens;
using GLGameJam.UI.Widgets;
using GLGameJam.Utils;
using GLJamGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GLGameJam.UI
{

    public class Shop
    {

        #region Constants
        private const int ShopX = 0;
        private const int ShopY = 135;
        private const int ShopWidth = 320;
        private const int ShopHeight = 45;
        public const int MaxShopCards = 5;
        #endregion

        private readonly InputManager inputManager;

        private readonly CardDefinition[] currentShopCards;

        private readonly PlayerResources playerResources;

        public Shop(PlayerResources playerResources, InputManager inputManager)
        {
            this.playerResources = playerResources;
            this.inputManager = inputManager;
            currentShopCards = new CardDefinition[MaxShopCards];
            RefreshShop();
        }

        public void RefreshShop()
        {
            for (var i = 0; i < currentShopCards.Length; ++i)
            {
                currentShopCards[i] = RandomUtils.RandomRange(CardDefinitions.CardDefinitionsList);
            }
        }

        public void LoadContent(AssetManager assetManager)
        {
        }

        public void Input(InputManager inputManager)
        {
            if (inputManager.IsActionJustDown("shop_refresh") && playerResources.Gold >= GameScreen.ShopRefreshPrice)
            {
                RefreshShop();
                playerResources.Gold -= GameScreen.ShopRefreshPrice;
            }
            else if (inputManager.IsActionJustDown("shop_buyexp") && playerResources.Gold >= GameScreen.ShopExpPrice)
            {
                playerResources.Exp += 2;
                playerResources.Gold -= GameScreen.ShopExpPrice;
            }
        }

        private void DrawCards(CustomBatch customBatch)
        {
            const int cardX = ShopWidth / 2 - 2 * ShopCard.CardSizeX;
            const int cardY = (ShopHeight / 2) - (ShopCard.CardSizeY / 2);
            const int spacing = 2;

            var shopOrigin = customBatch.GetOrigin();

            for (var i = 0; i < currentShopCards.Length; ++i)
            {
                var card = currentShopCards[i];

                if (card == null)
                    continue;

                var cardStartX = shopOrigin.X + cardX + (ShopCard.CardSizeX + spacing) * i;
                var cardStartY = shopOrigin.Y + cardY;

                customBatch.SetOrigin(cardStartX, cardStartY);

                bool hovering = false;

                var (mx, my) = inputManager.MousePosition;

                if (mx > cardStartX && mx < cardStartX + ShopCard.CardSizeX && my > cardStartY &&
                    my < cardStartY + ShopCard.CardSizeY)
                    hovering = true;

                // var hovering = new Rectangle(cardStartX, cardStartY, cardStartX + ShopCard.CardSize, cardStartY + ShopCard.CardSize).Contains(inputManager.MousePosition);

                if (hovering)
                {
                    const int offsetY = 2;
                    const int offsetX = 2;
                    customBatch.DrawNineRect(0, -offsetY, ShopCard.CardSizeX, ShopCard.CardSizeY, Color.White);
                    customBatch.DrawPixelString(new Vector2(offsetX, 4 - offsetY), "ATK " + card.Attack, Color.White, FontSize.Small);
                    customBatch.DrawPixelString(new Vector2(offsetX, 12 - offsetY), "DEF " + card.Armor, Color.White, FontSize.Small);
                    customBatch.DrawPixelString(new Vector2(offsetX, 20 - offsetY), "HP " + card.Health, Color.White, FontSize.Small);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && playerResources.Gold >= card.Gold && playerResources.HasSpaceForCard())
                    {
                        playerResources.GiveCard(new Card(card));
                        currentShopCards[i] = null;
                        playerResources.Gold -= card.Gold;
                    }
                }
                else
                {
                    customBatch.DrawNineRect(0, 0, ShopCard.CardSizeX, ShopCard.CardSizeY, Color.White);
                    customBatch.Draw(card.SourceName, new Rectangle(ShopCard.CardSizeX / 2 - 8, 12, 16, 16), Color.White);
                    var vec = new Point(ShopCard.CardSizeX - 12, -4);
                    //customBatch.Draw("melee_icon", ShopCard.CardSizeX - 12, -4);
                    var priceColor = Color.White;

                    if (playerResources.Gold < card.Gold)
                        priceColor = Color.DarkRed;

                    customBatch.Draw("coin_icon", new Rectangle(4, 4, 6, 6), priceColor);
                    customBatch.DrawPixelString(new Vector2(11, 4), card.Gold.ToString(), priceColor, FontSize.Small);
                }

                //card.Draw(customBatch, hovering);
            }
        }

        public void Draw(CustomBatch customBatch)
        {
            customBatch.SetOrigin(ShopX, ShopY);

            customBatch.DrawNineRect(0, 0, ShopWidth, ShopHeight, Color.White);

            var levelStr = playerResources.Level == PlayerResources.MaxLevel
                ? "Level Max."
                : $"Level {playerResources.Level}";


            customBatch.DrawPixelString(new Vector2(0, -24), "R: Refresh", playerResources.Gold < GameScreen.ShopRefreshPrice ? Colors.ErrorColor : Colors.ExpColor, FontSize.Small);
            customBatch.DrawPixelString(new Vector2(0, -16), "F: Buy EXP", playerResources.Gold < GameScreen.ShopExpPrice ? Colors.ErrorColor : Colors.ExpColor, FontSize.Small);
            customBatch.DrawPixelString(new Vector2(0, -8), levelStr, Colors.ExpColor, FontSize.Small);

            const int goldX = 3;
            const int goldY = 5;
            customBatch.Draw("gold", goldX, goldY);
            customBatch.DrawPixelString(new Vector2(goldX + 13, goldY), playerResources.Gold.ToString(), Color.White);

            string expStr = playerResources.Level == PlayerResources.MaxLevel ? "Max." : $"{playerResources.Exp}/{playerResources.NextExp}";
            customBatch.Draw("exp", goldX, goldY + 16);
            customBatch.DrawPixelString(new Vector2(goldX + 13, goldY + 16), expStr, Color.White);


            // Each method will use its origin

            DrawCards(customBatch);

            customBatch.SetOrigin(0, 0);
        }

    }
}
