using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public static class Direction
    {
        public static readonly Coordinate Up = new Coordinate(0, -1);
        public static readonly Coordinate Right = new Coordinate(1, 0);
        public static readonly Coordinate Down = new Coordinate(0, 1);
        public static readonly Coordinate Left = new Coordinate(-1, 0);
        public static readonly Coordinate None = new Coordinate(0, 0);

        public static readonly Coordinate[] GhostPriorityList = new Coordinate[] { Up, Left, Down, Right };
    }
}
