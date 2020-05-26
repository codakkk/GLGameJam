using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGameJam.Game
{

    public static class CardDefinitions
    {
        public static List<CardDefinition> CardDefinitionsList = new List<CardDefinition>();

        public static CardDefinition Mage = new CardDefinition()
        {
            Name = "Mage", 
            SourceName = "mage", 
            Attack = 20, 
            Armor = 2, 
            Health = 3, 
            Gold = 2,
            Rarity = CardRarity.Common
        };
        public static CardDefinition Warrior = new CardDefinition()
        {
            Name = "Warrior",
            SourceName = "warrior",
            Attack = 2,
            Armor = 5,
            Health = 10,
            Gold = 10,
            Rarity = CardRarity.Common
        };

        public static CardDefinition ZioDeeNo = new CardDefinition()
        {
            Name = "Zio DeeNo",
            SourceName = "chicken",
            Attack = 2,
            Armor = 5,
            Health = 10,
            Gold = 10,
            Rarity = CardRarity.Common
        };

    }

    public class CardDefinition
    {
        public string Name { get; internal set; }

        public string SourceName { get; internal set; }

        public int Attack { get; internal set; }
        public int Armor { get; internal set; }
        public int Health { get; internal set; }

        public int Gold { get; internal set; }

        public CardRarity Rarity { get; internal set; }

        public CardDefinition()
        {
            CardDefinitions.CardDefinitionsList.Add(this);
        }
    }
}
