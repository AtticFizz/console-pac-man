using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public static class MapObject 
    {
        public static readonly char Wall = '#';
        public static readonly char Empty = ' ';
        public static readonly char GhostGate = '-';
        public static readonly char GhostNoUpDownTile = '_';
        public static readonly char GhostHouse = 'G';
        public static readonly char Pellet = '∙';
        public static readonly char PowerPellet = 'o';

        public static readonly Dictionary<char, ConsoleColor> Color = new Dictionary<char, ConsoleColor>()
        {
            { Wall, ConsoleColor.Blue },
            { Empty, ConsoleColor.Black},
            { GhostGate, ConsoleColor.DarkMagenta },
            { GhostNoUpDownTile, ConsoleColor.Black },
            { GhostHouse, ConsoleColor.Black },
            { Pellet, ConsoleColor.Yellow },
            { PowerPellet, ConsoleColor.Yellow }
        };

        public static bool CanPacmanEnter(char symbol)
        {
            if (symbol.Equals(Wall))
                return false;
            else if (symbol.Equals(GhostGate))
                return false;
            else
                return true;
        }
        public static bool CanGhostEnter(char symbol)
        {
            if (symbol.Equals(Wall))
                return false;
            else
                return true;
        }

    }
}
