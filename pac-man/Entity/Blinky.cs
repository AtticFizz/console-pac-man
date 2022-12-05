using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public class Blinky : Entity
    {
        public override char Symbol => 'B';
        public override ConsoleColor Color => ConsoleColor.Red;

        public Blinky(Coordinate initialPosition)
        {
            Facing = Direction.Left; // !!!
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Mode = GhostMode.Scatter;
            Position = initialPosition;
        }
        public Blinky(int initialX, int initialY)
        {
            Facing = Direction.Left; // !!!
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
            MoveGhostToTarget(pacman.Position);
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
