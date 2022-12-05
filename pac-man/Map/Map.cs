using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public static class Map
    {
        private static char[][] Matrix;
        private static char[][] Pellets;
        public static Coordinate Size;
        public static int PelletsCount;

        public static char Get(int x, int y)
        {
            return Matrix[y][x];
        }

        public static int RemovePellet(Coordinate coordinate)
        {
            return RemovePellet(coordinate.X, coordinate.Y);
        }
        public static int RemovePellet(int x, int y)
        {
            if (Pellets[y][x].Equals(MapObject.Pellet))
            {
                PelletsCount--;
                Pellets[y][x] = Matrix[y][x];
                return 10; // <-- Fruit?
            }
            else if (Pellets[y][x].Equals(MapObject.PowerPellet))
            {
                PelletsCount--;
                Pellets[y][x] = Matrix[y][x];
                return 50; // <-- Fruit?
            }

            return 0;
        }

        public static Coordinate OutOfBounds(Coordinate position)
        {
            if (position.Y < 0)
                return Direction.Up;
            if (position.X < 0)
                return Direction.Left;
            if (position.Y >= Size.Y)
                return Direction.Down;
            if (position.X >= Size.X)
                return Direction.Right;

            return Direction.None;
        }

        public static Coordinate Teleport(Coordinate OutOfBoundsDirection, Coordinate position)
        {
            if (OutOfBoundsDirection.Equals(Direction.Up))
                return new Coordinate(position.X, Size.Y - 1);
            if (OutOfBoundsDirection.Equals(Direction.Left))
                return new Coordinate(Size.X - 1, position.Y);
            if (OutOfBoundsDirection.Equals(Direction.Down))
                return new Coordinate(position.X, 0);
            if (OutOfBoundsDirection.Equals(Direction.Right))
                return new Coordinate(0, position.Y);

            return position;
        }

        public static Coordinate FixOutOfBounds(Coordinate position)
        {
            Coordinate OutOfBoundsDirection = OutOfBounds(position);
            if (!OutOfBoundsDirection.Equals(Direction.None))
                return Teleport(OutOfBoundsDirection, position);

            return position;
        }

        public static char Get(Coordinate coordinate)
        {
            coordinate = FixOutOfBounds(coordinate);
            return Matrix[coordinate.Y][coordinate.X];
        }

        public static void Load(string map, string pellets)
        {
            LoadMap(map);
            LoadPellets(pellets);
        }

        public static void LoadMap(string map)
        {
            string[] lines = map.Split('\n');

            Size = new Coordinate();
            Size.X = lines[0].Length;
            Size.Y = lines.Length;

            Matrix = new char[Size.Y][];
            for (int y = 0; y < Size.Y; y++)
            {
                Matrix[y] = lines[y].ToCharArray();
            }
        }

        public static void LoadPellets(string pellets)
        {
            PelletsCount = 0;
            string[] lines = pellets.Split('\n');

            Pellets = new char[Size.Y][];
            for (int y = 0; y < Size.Y; y++)
            {
                Pellets[y] = lines[y].ToCharArray();
                for (int x = 0; x < Size.X; x++)
                {
                    if (Pellets[y][x].Equals(MapObject.Pellet) || Pellets[y][x].Equals(MapObject.PowerPellet))
                        PelletsCount++;
                }
            }
        }

        public static string ToString(Entity pac_man, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            string matrix = "";

            for (int y = 0; y < Matrix.Length; y++)
            {
                for (int x = 0; x < Matrix[y].Length; x++)
                {
                    if (blinky.Position.Equals(x, y))
                        matrix += blinky.Symbol;
                    else if (inky.Position.Equals(x, y))
                        matrix += inky.Symbol;
                    else if (pinky.Position.Equals(x, y))
                        matrix += pinky.Symbol;
                    else if (clyde.Position.Equals(x, y))
                        matrix += clyde.Symbol;
                    else if (pac_man.Position.Equals(x, y))
                        matrix += pac_man.Symbol;
                    else if (Pellets[y][x].Equals(MapObject.Pellet))
                        matrix += MapObject.Pellet;
                    else if (Pellets[y][x].Equals(MapObject.PowerPellet))
                        matrix += MapObject.PowerPellet;
                    else if (Matrix[y][x].Equals(MapObject.GhostNoUpDownTile))
                        matrix += MapObject.Empty;
                    else
                        matrix += Matrix[y][x];
                }

                matrix += "\n";
            }

            return matrix.TrimEnd('\n');
        }

        public static readonly string LevelMain = new string
        (
            "############################\n" +
            "#            ##            #\n" +
            "# #### ##### ## ##### #### #\n" +
            "# #### ##### ## ##### #### #\n" +
            "# #### ##### ## ##### #### #\n" +
            "#                          #\n" +
            "# #### ## ######## ## #### #\n" +
            "# #### ## ######## ## #### #\n" +
            "#      ##    ##    ##      #\n" +
            "###### ##### ## ##### ######\n" +
            "     # ##### ## ##### #     \n" +
            "     # ## ________ ## #     \n" +
            "     # ## ###--### ## #     \n" +
            "###### ## #GGGGGG# ## ######\n" +
            "          #GGGGGG#          \n" +
            "###### ## #GGGGGG# ## ######\n" +
            "     # ## ######## ## #     \n" +
            "     # ##          ## #     \n" +
            "     # ## ######## ## #     \n" +
            "###### ## ######## ## ######\n" +
            "#            ##            #\n" +
            "# #### ##### ## ##### #### #\n" +
            "# #### ##### ## ##### #### #\n" +
            "#   ##    ________    ##   #\n" +
            "### ## ## ######## ## ## ###\n" +
            "### ## ## ######## ## ## ###\n" +
            "#      ##    ##    ##      #\n" +
            "# ########## ## ########## #\n" +
            "# ########## ## ########## #\n" +
            "#                          #\n" +
            "############################"
        );
        
        public static readonly string LevelMainPellets = new string
        (
            "############################\n" +
            "#∙∙∙∙∙∙∙∙∙∙∙∙##∙∙∙∙∙∙∙∙∙∙∙∙#\n" +
            "#∙####∙#####∙##∙#####∙####∙#\n" +
            "#o####∙#####∙##∙#####∙####o#\n" +
            "#∙####∙#####∙##∙#####∙####∙#\n" +
            "#∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙#\n" +
            "#∙####∙##∙########∙##∙####∙#\n" +
            "#∙####∙##∙########∙##∙####∙#\n" +
            "#∙∙∙∙∙∙##∙∙∙∙##∙∙∙∙##∙∙∙∙∙∙#\n" +
            "######∙##### ## #####∙######\n" +
            "     #∙##### ## #####∙#     \n" +
            "     #∙## ________ ##∙#     \n" +
            "     #∙## ###--### ##∙#     \n" +
            "######∙## #GGGGGG# ##∙######\n" +
            "      ∙   #GGGGGG#   ∙      \n" +
            "######∙## #GGGGGG# ##∙######\n" +
            "     #∙## ######## ##∙#     \n" +
            "     #∙##          ##∙#     \n" +
            "     #∙## ######## ##∙#     \n" +
            "######∙## ######## ##∙######\n" +
            "#∙∙∙∙∙∙∙∙∙∙∙∙##∙∙∙∙∙∙∙∙∙∙∙∙#\n" +
            "#∙####∙#####∙##∙#####∙####∙#\n" +
            "#∙####∙#####∙##∙#####∙####∙#\n" +
            "#o∙∙##∙∙∙∙∙∙∙  ∙∙∙∙∙∙∙##∙∙o#\n" +
            "###∙##∙##∙########∙##∙##∙###\n" +
            "###∙##∙##∙########∙##∙##∙###\n" +
            "#∙∙∙∙∙∙##∙∙∙∙##∙∙∙∙##∙∙∙∙∙∙#\n" +
            "#∙##########∙##∙##########∙#\n" +
            "#∙##########∙##∙##########∙#\n" +
            "#∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙#\n" +
            "############################"
        );

        public static readonly Coordinate PacManInitialPosition = new Coordinate(13, 23);

        public static readonly Coordinate BlinkyInitialPosition = new Coordinate(13, 11);
        public static readonly Coordinate InkyInitialPosition = new Coordinate(11, 14);
        public static readonly Coordinate PinkyInitialPosition = new Coordinate(13, 14);
        public static readonly Coordinate ClydeInitialPosition = new Coordinate(15, 14);

        public static readonly Coordinate BlinkyAnchor = new Coordinate(25, -3);
        public static readonly Coordinate InkyAnchor = new Coordinate(27, 32);
        public static readonly Coordinate PinkyAnchor = new Coordinate(3, -3);
        public static readonly Coordinate ClydeAnchor = new Coordinate(0, 32);

        public static readonly Coordinate GhostGate = new Coordinate(13, 12);
        public static readonly Coordinate GhostHouse = new Coordinate(13, 14);
    }
}
