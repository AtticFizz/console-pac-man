using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    // Strawberry = 300 Pts
    // Orange = 500 Pts
    // Apple = 700 Pts
    // Melon = 1000 Pts
    // Galaxian = 2000 Pts
    // Bell = 3000 Pts
    // Key = 5000 Pts

    public static class Scoreboard
    {
        public static readonly int DEFAULT_POINTS = 0;
        public static readonly int DEFAULT_LIVES = 3;

        public static int Streak = 0;
        public static readonly int[] GhostPoints = new int[] { 200, 400, 800, 1600 };

        public static int Points = DEFAULT_POINTS;
        public static int Lives = DEFAULT_LIVES;

    }
}
