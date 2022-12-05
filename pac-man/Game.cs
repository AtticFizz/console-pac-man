using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pac_man
{
    public static class Game
    {
        public static bool Exit;
        private static bool GameOver;
        private static bool Pause;
        private static bool Win;

        public static int StaticFrameTime;
        public static int LogicFrameTime;
        public static Stopwatch GlobalStopWatch;
        public static int Timer;

        private static Entity blinky = new Blinky(Map.BlinkyInitialPosition);
        private static Entity inky = new Inky(Map.InkyInitialPosition);
        private static Entity pinky = new Pinky(Map.PinkyInitialPosition);
        private static Entity clyde = new Clyde(Map.ClydeInitialPosition);
        private static Entity pac_man = new Pac_Man(Map.PacManInitialPosition);

        public static GhostMode GlobalGhostMode;

        public static void Start()
        {
            Exit = false;
            GameOver = false;
            Pause = false;
            Win = false;

            StaticFrameTime = 2;
            LogicFrameTime = 60; // 80
            GlobalStopWatch = new Stopwatch();
            Timer = GhostModeTimer.Time[GhostMode.Scatter];

            blinky = new Blinky(Map.BlinkyInitialPosition);
            inky = new Inky(Map.InkyInitialPosition);
            pinky = new Pinky(Map.PinkyInitialPosition);
            clyde = new Clyde(Map.ClydeInitialPosition);
            pac_man = new Pac_Man(Map.PacManInitialPosition);

            blinky.Mode = GhostMode.Scatter;
            inky.Mode = GhostMode.Scatter;
            pinky.Mode = GhostMode.Scatter;
            clyde.Mode = GhostMode.Scatter;

            GlobalGhostMode = GhostMode.Scatter;

            GlobalStopWatch.Reset();
            GlobalStopWatch.Start();
        }

        public static void Loop()
        {
            Start();
            Map.Load(Map.LevelMain, Map.LevelMainPellets);

            while (!GameOver && !Exit)
            {
                if (!Pause)
                {
                    Logic();
                    Print();
                    Thread.Sleep(LogicFrameTime);
                }
            }
        }

        public static void Print()
        {
            string map = Map.ToString(pac_man, blinky, inky, pinky, clyde);

            Console.Clear();

            foreach (char symbol in map)
            {
                if (symbol.Equals(blinky.Symbol))
                    Console.BackgroundColor = blinky.GetColor();
                else if (symbol.Equals(inky.Symbol))
                    Console.BackgroundColor = inky.GetColor();
                else if (symbol.Equals(pinky.Symbol))
                    Console.BackgroundColor = pinky.GetColor();
                else if (symbol.Equals(clyde.Symbol))
                    Console.BackgroundColor = clyde.GetColor();

                else if (symbol.Equals(pac_man.Symbol))
                    Console.BackgroundColor = pac_man.Color;

                else if (MapObject.Color.ContainsKey(symbol))
                    Console.BackgroundColor = MapObject.Color[symbol];
                else
                    Console.ResetColor();

                if (symbol.Equals(MapObject.Pellet))
                {
                    Console.BackgroundColor = MapObject.Color[MapObject.Empty];
                    Console.ForegroundColor = MapObject.Color[symbol];
                    Console.Write(MapObject.Pellet);
                }
                else if (symbol.Equals(MapObject.PowerPellet))
                {
                    Console.BackgroundColor = MapObject.Color[MapObject.Empty];
                    Console.ForegroundColor = MapObject.Color[symbol];
                    Console.Write(MapObject.PowerPellet);
                }
                else if (symbol.Equals('\n'))
                    Console.Write('\n');
                else
                    Console.Write(' ');

                Console.ResetColor();
            }

            //Console.WriteLine(map);

            Console.WriteLine();
            Console.WriteLine("Points: " + Scoreboard.Points);
            Console.WriteLine("Lives: " + Scoreboard.Lives);
            //Console.WriteLine("Mode: " + GlobalGhostMode);
            //Console.WriteLine("Mode time: "+ GlobalStopWatch.ElapsedMilliseconds);
            //Console.WriteLine(Map.Size);

            if (Win)
                Console.WriteLine("You Win!");
            else if (GameOver)
                Console.WriteLine("Game Over!");
            else if (Exit)
                Console.WriteLine("Exit!");
            else if (Pause)
                Console.WriteLine("Game Paused!");
        }

        public static Coordinate Input(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                return Direction.Up;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                return Direction.Right;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                return Direction.Down;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                return Direction.Left;
            }
            else if (key == ConsoleKey.P)
            {
                Pause = !Pause;
                return Direction.None;
            }

            return Direction.None;
        }

        public static void Logic()
        {
            if (GlobalStopWatch.ElapsedMilliseconds >= Timer)
            {
                TransitionGlobalGhostMode();
            }

            if (Map.PelletsCount == 0)
            {
                Win = true;
                Exit = true;
            }

            CheckCollision();

            pac_man.UpdatePosition(null, null, blinky, inky, pinky, clyde);

            CheckCollision();

            blinky.UpdatePosition(Map.BlinkyAnchor, pac_man, blinky, inky, pinky, clyde);
            inky.UpdatePosition(Map.InkyAnchor, pac_man, blinky, inky, pinky, clyde);
            pinky.UpdatePosition(Map.PinkyAnchor, pac_man, blinky, inky, pinky, clyde);
            clyde.UpdatePosition(Map.ClydeAnchor, pac_man, blinky, inky, pinky, clyde);

            CheckCollision();
        }

        public static void TransitionGlobalGhostMode()
        {
            if (GlobalGhostMode == GhostMode.Scatter)
                ChangeGlobalGhostMode(GhostMode.Chase, true);
            else if (GlobalGhostMode == GhostMode.Chase)
                ChangeGlobalGhostMode(GhostMode.Scatter, false);
            else if (GlobalGhostMode == GhostMode.Frightened)
                ChangeGlobalGhostMode(GhostMode.Chase, false);
        }

        public static void ChangeGlobalGhostMode(GhostMode mode, bool turnAround = false)
        {
            GlobalGhostMode = mode;

            if (blinky.Mode != GhostMode.Eaten)
                blinky.Mode = mode;
            if (inky.Mode != GhostMode.Eaten)
                inky.Mode = mode;
            if (pinky.Mode != GhostMode.Eaten)
                pinky.Mode = mode;
            if (clyde.Mode != GhostMode.Eaten)
                clyde.Mode = mode;

            if (turnAround)
            {
                if (blinky.Mode != GhostMode.Eaten)
                    blinky.Facing = blinky.Facing.Inverse();
                if (inky.Mode != GhostMode.Eaten)
                    inky.Facing = inky.Facing.Inverse();
                if (pinky.Mode != GhostMode.Eaten)
                    pinky.Facing = pinky.Facing.Inverse();
                if (clyde.Mode != GhostMode.Eaten)
                    clyde.Facing = clyde.Facing.Inverse();
            }

            GlobalStopWatch.Reset();
            Timer = GhostModeTimer.Time[mode];
            GlobalStopWatch.Start();
        }

        private static void CheckCollision()
        {
            Entity collidingGhost = pac_man.IsColliding(blinky, inky, pinky, clyde);

            if (collidingGhost != null)
            {
                if (collidingGhost.Mode == GhostMode.Chase || collidingGhost.Mode == GhostMode.Scatter)
                {
                    if (Scoreboard.Lives > 1)
                    {
                        Scoreboard.Lives--;
                        Start();
                        Print();
                        Thread.Sleep(3000);
                        Start();
                        return;
                    }
                    else
                    {
                        GameOver = true;
                        return;
                    }
                }
                else if (collidingGhost.Mode == GhostMode.Frightened)
                {
                    Scoreboard.Points += Scoreboard.GhostPoints[Scoreboard.Streak];
                    Scoreboard.Streak++;
                    collidingGhost.Mode = GhostMode.Eaten;
                    return;
                }
            }
        }

    }
}
