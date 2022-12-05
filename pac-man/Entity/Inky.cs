using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public class Inky : Entity
    {
        public override char Symbol => 'I';
        public override ConsoleColor Color => ConsoleColor.Cyan;

        public Inky(Coordinate initialPosition)
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Mode = GhostMode.Scatter;
            Position = initialPosition;
        }
        public Inky(int initialX, int initialY)
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Mode = GhostMode.Scatter;
            Position = new Coordinate(initialX, initialY);
        }

        public override void UpdatePosition(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            if (Mode != GhostMode.Eaten)
            {
                if (Map.Get(Map.FixOutOfBounds(Position)).Equals(MapObject.GhostHouse))
                {
                    MoveGhostToTarget(Map.GhostGate);
                    return;
                }
            }

            // CheckGhostHouse(); WTF

            if (Mode == GhostMode.Chase)
                ChaseMode(anchor, pacman, blinky, inky, pinky, clyde);
            else if (Mode == GhostMode.Scatter)
                ScatterMode(anchor, pacman, blinky, inky, pinky, clyde);
            else if (Mode == GhostMode.Frightened)
                FrightenedMode(anchor, pacman, blinky, inky, pinky, clyde);
            else if (Mode == GhostMode.Eaten)
                EatenMode(anchor, pacman, blinky, inky, pinky, clyde);
            else
                ScatterMode(anchor, pacman, blinky, inky, pinky, clyde);
        }

        private void ChaseMode(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            if (pacman.Facing.Equals(Direction.Up)) {
                Coordinate pacmanAnchor = pacman.Position + 2 * Direction.Up + 2 * Direction.Left;
                Coordinate vector = pacmanAnchor - blinky.Position;
                Coordinate targetPosition = pacmanAnchor + vector;

                MoveGhostToTarget(targetPosition);
            }
            else
            {
                Coordinate pacmanAnchor = pacman.Position + 2 * pacman.Facing;
                Coordinate vector = pacmanAnchor - blinky.Position;
                Coordinate targetPosition = pacmanAnchor + vector;

                MoveGhostToTarget(targetPosition);
            }
        }

        private void ScatterMode(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clydet)
        {
            MoveGhostToTarget(anchor);
        }

        private void FrightenedMode(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            PickRandom();
        }

        private void EatenMode(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            GoToGhostHouse();
        }

       

    }
}
