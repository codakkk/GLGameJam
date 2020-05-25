using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGameJam.Utils
{
    public static class RandomUtils
    {

        private static readonly Random random = new Random();

        // Gets a random T from a specific list
        // Returns null i list.Count == 0
        public static T RandomRange<T>(List<T> list) where T : class
        {
            return list.Count == 0 ? null : list[random.Next(0, list.Count)];
        }
    }
}
