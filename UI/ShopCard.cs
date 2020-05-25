using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGameJam.Game;
using GLJamGame;
using Microsoft.Xna.Framework;

namespace GLGameJam.UI
{
    public class ShopCard
    {
        public const int CardSizeX = 36;
        public const int CardSizeY = 32;

        public CardDefinition CardDefinition { get; set; }



        public void Draw(CustomBatch customBatch/*, int posX, int posY*/, bool hovering = false)
        {
            //customBatch.SetOrigin(posX, posY);
            
        }

        public void OnClick()
        {

        }
    }
}
