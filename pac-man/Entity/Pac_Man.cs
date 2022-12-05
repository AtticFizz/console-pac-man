using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public class Pac_Man : Entity
    {
        public override char Symbol => 'O';
        public override ConsoleColor Color => ConsoleColor.Yellow;

        public Pac_Man(Coordinate initialPosition)
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Position = initialPosition;
        }
        public Pac_Man(int initialX, int initialY) 
        {
            Facing = Direction.None;
            MovingDirection = Direction.None;
            NextMovingDirection = Direction.None;
            Position = new Coordinate(initialX, initialY);
        }

        public override void UpdatePosition(Coordinate anchor, Entity pacman, Entity blinky, Entity inky, Entity pinky, Entity clyde)
        {
            int fruit = Map.RemovePellet(Map.FixOutOfBounds(Position));
            Scoreboard.Points += fruit;

            if (fruit == 50)
            {
                Scoreboard.Streak = 0;
                Game.ChangeGlobalGhostMode(GhostMode.Frightened, true);

                //PowerPelletEaten(anchor, pacman, blinky, inky, pinky, clyde);
            }

            if (JoyStick.IsPressed())
            {
                Coordinate turnedPosition = Map.FixOutOfBounds(Position + JoyStick.DirectionPressed);
                char turnedPositionObject = Map.Get(turnedPosition);

                if (MapObject.CanPacmanEnter(turnedPositionObject))
                {
                    Facing = JoyStick.DirectionPressed;
                    MovingDirection = JoyStick.DirectionPressed;
                    JoyStick.Reset();
                    NextMovingDirection = Direction.None;
                    MoveForward();
                    return;
                }

                NextMovingDirection = JoyStick.DirectionPressed;
            }

            Coordinate nextPosition = Map.FixOutOfBounds(Position + MovingDirection);
            char nextPositionObject = Map.Get(nextPosition);

            if (MapObject.CanPacmanEnter(nextPositionObject))
            {
                MoveForward();
                return;
            }

            Stop();
            MovingDirection = NextMovingDirection;
            NextMovingDirection = Direction.None;
        }

        private void PowerPelletEaten(Coordinate anchor, Entity pacman, params Entity[] ghosts)
        {
            Scoreboard.Streak = 0;

            foreach (Entity ghost in ghosts)
            {
                if (ghost.Mode != GhostMode.Eaten)
                {
                    ghost.Mode = GhostMode.Frightened;
                    ghost.Facing = ghost.Facing.Inverse();
                }
            }
        }
    }
}
