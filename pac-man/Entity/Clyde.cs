using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public class Clyde : Entity
    {
        public override char Symbol => 'C';
        public override ConsoleColor Color => ConsoleColor.DarkYellow;

        public Clyde(Coordinate initialPosition)
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Mode = GhostMode.Scatter;
            Position = initialPosition;
        }
        public Clyde(int initialX, int initialY)
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
            if (Coordinate.GetLinearDistance(pacman.Position, Position) >= 8.0)
            {
                MoveGhostToTarget(pacman.Position);
            }
            else
            {
                MoveGhostToTarget(anchor);
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
