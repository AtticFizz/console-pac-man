using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    // Ghost targeting system:
    // Get closest linear distance and go to the shortest one.
    // if two or more distances are equal use priority:
    // Up, Left, Down, Right
    // Ghosts can't turn around.
    // Ghosts can't turn to any direction at the top of the Ghost House and pack-man spawn.

    // https://www.youtube.com/watch?v=ataGotQ7ir8&ab_channel=RetroGameMechanicsExplained
    // https://gameinternals.com/understanding-pac-man-ghost-behavior

    public abstract class Entity
    {
        public abstract char Symbol { get; }
        public Coordinate Position { get; set; }
        public Coordinate Facing { get; set; }
        public Coordinate MovingDirection { get; set; }
        public Coordinate NextMovingDirection { get; set; }
        public GhostMode Mode { get; set; }
        public abstract ConsoleColor Color { get; }
        public abstract void UpdatePosition(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde);

        public static readonly ConsoleColor FrightenedColor = ConsoleColor.DarkBlue;
        public static readonly ConsoleColor EatenColor = ConsoleColor.Gray;


        public ConsoleColor GetColor()
        {
            if (Mode == GhostMode.Frightened)
                return FrightenedColor;
            if (Mode == GhostMode.Eaten)
                return EatenColor;

            return Color;
        }

        protected void MoveForward()
        {
            Position = Map.FixOutOfBounds(Position + MovingDirection);
        }

        protected void Stop()
        {
            MovingDirection = Direction.None;
        }

        protected void CheckGhostHouse()
        {
            if (Mode != GhostMode.Eaten)
            {
                if (Map.Get(Map.FixOutOfBounds(Position)).Equals(MapObject.GhostHouse))
                {
                    MoveGhostToTarget(Map.GhostGate);
                    return;
                }
            }
        }

        protected void MoveGhostToTarget(Coordinate target)
        {
            char currentTile = Map.Get(Map.FixOutOfBounds(Position));

            double shortestDistance = double.PositiveInfinity;
            Coordinate shortestDirection = Direction.None;

            for (int i = 0; i < Direction.GhostPriorityList.Length; i++)
            {
                Coordinate nextPosition = Map.FixOutOfBounds(Position + Direction.GhostPriorityList[i]);
                double distance = Coordinate.GetLinearDistance(nextPosition, target);

                char nextPositionSymbol = Map.Get(nextPosition);

                if (MapObject.CanGhostEnter(nextPositionSymbol))
                {
                    if (distance < shortestDistance)
                    {
                        if (!Direction.GhostPriorityList[i].IsInverse(Facing))
                        {
                            if (!(currentTile.Equals(MapObject.GhostGate) && nextPositionSymbol.Equals(MapObject.GhostGate)))
                            {
                                shortestDistance = distance;
                                shortestDirection = Direction.GhostPriorityList[i];
                            }
                        }
                    }
                }
            }

            if (Mode == GhostMode.Chase || Mode == GhostMode.Scatter)
            {
                if (!(currentTile.Equals(MapObject.GhostNoUpDownTile) && (shortestDirection.Equals(Direction.Up) || shortestDirection.Equals(Direction.Down))))
                {
                    Facing = shortestDirection;
                }
            }
            else
            {
                Facing = shortestDirection;
            }

            MovingDirection = Facing;
            MoveForward();
        }

        protected void PickRandom()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int randomNumber = rnd.Next(0, 3);

            Coordinate randomDirection = Direction.None;
            if (randomNumber == 0)
                randomDirection = Direction.Up;
            if (randomNumber == 1)
                randomDirection = Direction.Left;
            if (randomNumber == 2)
                randomDirection = Direction.Down;
            if (randomNumber == 3)
                randomDirection = Direction.Right;

            MoveGhostToTarget(Position + randomDirection);
        }

        protected void GoToGhostHouse()
        {
            if (!Map.FixOutOfBounds(Position).Equals(Map.GhostHouse))
            {
                MoveGhostToTarget(Map.GhostHouse);
                return;
            }

            Mode = Game.GlobalGhostMode;
        }

        public Entity IsColliding(params Entity[] ghosts)
        {
            foreach (Entity ghost in ghosts)
            {
                if (Position.Equals(ghost.Position))
                {
                    return ghost;
                }
            }

            return null;
        }
    }
}
