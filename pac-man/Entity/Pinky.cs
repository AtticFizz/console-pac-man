using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public class Pinky : Entity
    {
        public override char Symbol => 'P';
        public override ConsoleColor Color => ConsoleColor.Magenta;

        public Pinky(Coordinate initialPosition)
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Mode = GhostMode.Scatter;
            Position = initialPosition;
        }
        public Pinky(int initialX, int initialY)
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Mode = GhostMode.Scatter;
            Position = new Coordinate(initialX, initialY);
        }

        public override void UpdatePosition(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            CheckGhostHouse();

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
                Coordinate targetPosition = pacman.Position + 4 * Direction.Up + 4 * Direction.Left;
                MoveGhostToTarget(targetPosition);
            }
            else
            {
                Coordinate targetPosition = pacman.Position + 4 * pacman.Facing;
                MoveGhostToTarget(targetPosition);
            }
        }

        private void ScatterMode(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
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
