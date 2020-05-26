using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLGameJam.Screens;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam
{
    public class Player
    {
        public const int MaxBenchCards = 8;

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

        public HashSet<Card> TotalCards { get; private set; }

        private readonly Card[] bench;
        

        public Action OnLevelUp;
        public Action OnExpChange;
        public Action OnGoldChange;

        public Action<int, Card> OnAddCard;
        public Action<Card> OnRemoveCard;

        public Player()
        {
            this.bench = new Card[MaxBenchCards];
            this.TotalCards = new HashSet<Card>();
            Level = 1;
        }

        private void CheckCardLevels()
        {
            var toRemove = new List<Card>();
            foreach(var card in TotalCards.Where(card => card.Level < 3))
            {
                if (toRemove.Contains(card))
                    continue;

                var count = 0;

                var temp = new Card[2];

                foreach (var other in TotalCards)
                {
                    if (other == card || toRemove.Contains(other) || card.CardDefinition != other.CardDefinition || card.Level != other.Level)
                        continue;

                    temp[count++] = other;

                    if (count != 2) 
                        continue;

                    toRemove.AddRange(temp);

                    card.Level++;
                    break;
                }
            }

            RemoveCards(toRemove.ToArray());
        }

        public bool GiveCard(Card card)
        {
            if (card == null || IsBenchFull())
                return false;
            
            for (var i = 0; i < MaxBenchCards; ++i)
            {
                if (bench[i] != null)
                    continue;
                bench[i] = card;
                OnAddCard?.Invoke(i, card);
                break;
            }

            TotalCards.Add(card);

            CheckCardLevels();
            return true;
        }

        public bool RemoveCard(Card card)
        {
            if (card == null || !TotalCards.Remove(card))
                return false;

            var benchIndex = Array.FindIndex(bench, c => c == card);

            if (benchIndex != -1)
            {
                bench[benchIndex] = null;
            }

            OnRemoveCard?.Invoke(card);
            return true;
        }

        public void RemoveCards(params Card[] cards)
        {
            foreach (var t in cards)
            {
                RemoveCard(t);
            }
        }

        public bool IsBenchFull()
        {
            var count = 0;
            for (var i = 0; i < MaxBenchCards; ++i)
            {
                if (bench[i] != null)
                    count++;
            }
            return count == MaxBenchCards;
        }
    }
}
