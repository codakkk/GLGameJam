using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGameJam.Player
{
    public class PlayerResources
    {
        public const int StartExpLevel = 4;
        public const int ExpPerLevel = 2;
        public const int MaxLevel = 8;

        public int Gold { get; set; }

        private int exp;
        public int Exp
        {
            get => exp;
            set
            {
                exp = value;
                if (exp < NextExp) 
                    return;
                exp -= NextExp;
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
            }
        }

        public int NextExp => StartExpLevel + ExpPerLevel * (Level-1);

        public PlayerResources()
        {
            Gold = 4;
            Exp = 0;
            Level = 1;
        }
    }
}
