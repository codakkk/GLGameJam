using GLGameJam.Game;
using GLGameJam.Gfx;
using GLGameJam.Input;
using GLGameJam.Screens;
using GLGameJam.UI.Widgets;
using GLGameJam.Utils;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI
{

    public class Shop : BaseContainer
    {

        #region Constants
        private const int ShopX = 0;
        private const int ShopY = CoreGame.GameSizeY - ShopHeight;
        private const int ShopWidth = CoreGame.GameSizeX;
        private const int ShopHeight = 45;
        public const int MaxShopCards = 5;
        #endregion

        private readonly InputManager inputManager;

        private readonly Player player;

        #region UI Widgets
        private readonly ShopCard[] currentShopCards;
        private FloatingText errorText;
        #endregion



        public Shop(Player player, InputManager inputManager) : base(new Point(ShopX, ShopY), new Point(ShopWidth, ShopHeight))
        {
            this.player = player;
            this.inputManager = inputManager;
            currentShopCards = new ShopCard[MaxShopCards];

            InitializeUI();

            RefreshShop();
        }

        private void InitializeUI()
        {
            // Create card shop
            var cardX = Size.X / 2 - 2 * ShopCard.CardSizeX;
            var cardY = (Size.Y / 2) - (ShopCard.CardSizeY / 2);
            const int spacing = 2;

            for (var i = 0; i < currentShopCards.Length; ++i)
            {
                var cardStartX = cardX + (ShopCard.CardSizeX + spacing) * i;
                var shopCard = new ShopCard(new Point(cardStartX, cardY))
                {
                    CardDefinition = CardDefinitions.Mage
                };

                AddWidget(shopCard);

                shopCard.OnPress += () =>
                {
                    if (shopCard.CardDefinition.Gold > player.Gold || player.IsBenchFull())
                        return;
                    player.GiveCard(new Card(shopCard.CardDefinition));

                    player.Gold -= shopCard.CardDefinition.Gold;

                    shopCard.CardDefinition = null;
                    shopCard.IsVisible = false;

                    ShowFloating("New card!");
                };

                currentShopCards[i] = shopCard;
            }

            // Static Widgets
            AddWidget(new TextWidget(new Point(Size.X - 100, -16), "R:Refresh [2 €]", FontSize.Small)
            {
                Color = Color.White
            });
            AddWidget(new TextWidget(new Point(Size.X - 100, -24), "F:Buy Exp [2 €]", FontSize.Small)
            {
                Color = Color.White
            });

            // Mutable widgets
            errorText = new FloatingText(new Point(160 - 16, -8), "Test");
            AddWidget(errorText);

            var levelText = new TextWidget(new Point(0, -8), "Level", FontSize.Small)
            {
                Color = Colors.ExpColor
            };

            AddWidget(levelText);

            var goldText = new TextWidget(new Point(3, 5), "€ 0", FontSize.Normal)
            {
                Color = Color.White
            };

            AddWidget(goldText);

            var expText = new TextWidget(new Point(3, 5 + 16), $"£ {player.Exp/player.NextExp}", FontSize.Normal)
            {
                Color = Color.White
            };

            AddWidget(expText);


            player.OnGoldChange += () =>
            {
                goldText.Text = $"€ {player.Gold}";

                foreach (var shopCard in currentShopCards)
                {
                    if (shopCard.CardDefinition == null)
                        continue;
                    shopCard.CanAfford = !(shopCard.CardDefinition.Gold > player.Gold);
                }
            };

            player.OnExpChange += () =>
            {
                expText.Text = $"£ {player.Exp}/{player.NextExp}";
            };

            player.OnLevelUp += () =>
            {
                levelText.Text = player.Level == Player.MaxLevel
                    ? "Level Max."
                    : $"Level {player.Level}";

                if (player.Level != 1)
                {
                    ShowFloating("£ Level Up £");
                }
            };
        }

        public void RefreshShop()
        {
            foreach(var shopCard in currentShopCards)
            {
                shopCard.CardDefinition = RandomUtils.RandomRange(CardDefinitions.CardDefinitionsList);
                shopCard.IsVisible = true;
            }
        }

        public override void LoadContent(AssetManager assetManager)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Input(InputManager inputManager)
        {
            base.Input(inputManager);
            if (inputManager.IsActionJustDown("shop_refresh") && player.Gold >= GameScreen.ShopRefreshPrice)
            {
                RefreshShop();
                player.Gold -= GameScreen.ShopRefreshPrice;
            }
            else if (inputManager.IsActionJustDown("shop_buyexp") && player.Gold >= GameScreen.ShopExpPrice)
            {
                player.Exp += 2;
                player.Gold -= GameScreen.ShopExpPrice;
            }
        }

        public override void Draw(CustomBatch customBatch)
        {
            base.Draw(customBatch);

            customBatch.SetOrigin(Position.X, Position.Y);

            customBatch.DrawNineRect(0, 0, Size.X, Size.Y, Color.White);

            customBatch.SetOrigin(0, 0);
        }

        private void ShowFloating(string text)
        {
            errorText.Text = text;
            errorText.IsVisible = true;
        }
    }
}
