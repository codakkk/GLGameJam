using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLGameJam.Screens;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.Player
{
    public class PlayerResources
    {
        public const int StartExpLevel = 4;
        public const int ExpPerLevel = 2;
        public const int MaxLevel = 8;

        private int gold;
        public int Gold
        {
            get => gold;
            set
            {
                if (value < 0)
                    value = 0;
                gold = value;
                OnGoldChange?.Invoke();
            }
        }

        private int exp;
        public int Exp
        {
            get => exp;
            set
            {
                exp = value;
                OnExpChange?.Invoke();

                if (exp < NextExp) 
                    return;
                Exp -= NextExp;
                

                Level++;
            }
        }

        private int level;

        public int Level
        {
            get => level;
            set
            {
                level = value;
                if (level < 1)
                    level = 1;
                else if (level > MaxLevel)
                    level = MaxLevel;
                OnLevelUp?.Invoke();
            }
        }

        public int NextExp => StartExpLevel + ExpPerLevel * (Level-1);

        public Card[] PlayerCards { get; }

        public Action OnLevelUp;
        public Action OnExpChange;
        public Action OnGoldChange;

        public PlayerResources()
        {
            this.PlayerCards = new Card[GameScreen.MaxPlayerCards];
            Level = 1;
        }

        private void CheckCardLevels()
        {
            for (int i = 0; i < GameScreen.MaxPlayerCards; ++i)
            {
                var card = PlayerCards[i];

                if (card == null || card.Level == 3)
                    continue;

                var count = 0;

                var temp = new Card[3];
                temp[count++] = card;

                for (var k = 0; k < GameScreen.MaxPlayerCards; ++k)
                {
                    var other = PlayerCards[k];
                    if (other == card || other == null)
                        continue;

                    if (card.CardDefinition != other.CardDefinition || card.Level != other.Level) 
                        continue;

                    temp[count++] = other;

                    if (count != 3) 
                        continue;
                    RemoveCards(temp);

                    card.Level++;
                    GiveCard(card);

                    break;
                }
            }
        }

        /*public void DrawSidebar(CustomBatch customBatch)
        {
            const int CardX = 2;
            const int CardY = 4;
            for (int x = 0; x < CardX; ++x)
            for (int y = 0; y < CardY; ++y)
            {
                var card = PlayerCards[x + y * CardX];
                customBatch.SetOrigin(CardX + x * 24 + x, CardY + y * 24 + y);
                if (card == null)
                {
                    customBatch.DrawNineRect(0, 0, 24, 24, Color.White);
                }
                else
                {
                    card.DrawInList(customBatch);
                }
            }
            customBatch.SetOrigin(0, 0);
        }*/

        public bool GiveCard(Card card)
        {
            if (card == null || !HasSpaceForCard())
                return false;
            for (int i = 0; i < GameScreen.MaxPlayerCards; ++i)
            {
                if (PlayerCards[i] != null)
                    continue;
                PlayerCards[i] = card;
                break;
            }
            
            CheckCardLevels();
            return true;
        }

        public bool RemoveCard(Card card)
        {
            if (card == null)
                return false;
            bool res = false;
            for (int i = 0; i < GameScreen.MaxPlayerCards; ++i)
            {
                if (PlayerCards[i] != card)
                    continue;
                PlayerCards[i] = null;
                res = true;
                break;
            }

            return res;
        }

        public void RemoveCards(params Card[] cards)
        {
            foreach (var t in cards)
            {
                RemoveCard(t);
            }
        }

        public bool HasSpaceForCard()
        {
            int count = 0;
            for (int i = 0; i < GameScreen.MaxPlayerCards; ++i)
            {
                if (PlayerCards[i] != null)
                    count++;
            }

            return count != GameScreen.MaxPlayerCards;
        }
    }
}
