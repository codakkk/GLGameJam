using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Gfx;
using GLGameJam.UI.Widgets;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.Game
{
    public enum CardRarity : uint
    {
        Common,
        Rare,
        VeryRare,
    }

    public class Card
    {
        public CardDefinition CardDefinition { get; private set; }

        private int level;

        public int Level
        {
            get => level;
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 3)
                    value = 3;
                level = value;
            }
        }

        public int Attack => CardDefinition.Attack * Level;
        public int Armor => CardDefinition.Armor * Level;
        public int Health => CardDefinition.Health * Level;

        public Color Color
        {
            get
            {
                var color = Colors.LevelOneCard;
                switch (Level)
                {
                    case 1:
                        color = Colors.LevelOneCard;
                        break;
                    case 2:
                        color = Colors.LevelTwoCard;
                        break;
                    case 3:
                        color = Colors.LevelThreeCard;
                        break;
                }

                return color;
            }
        }


        public Card(CardDefinition cardDefinition)
        {
            this.Level = 1;
            this.CardDefinition = cardDefinition;
        }
    }
}
