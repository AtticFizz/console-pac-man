using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public static class JoyStick
    {
        private static Coordinate _PressedDirection = Direction.None;
        
        public static Coordinate DirectionPressed
        {
            get
            {
                return _PressedDirection;
            }
        }

        public static bool IsPressed()
        {
            return ! _PressedDirection.Equals(Direction.None);
        }

        public static void SetDirection(Coordinate direction)
        {
            _PressedDirection = direction;
        }
        public static void SetDirection(int x, int y)
        {
            _PressedDirection = new Coordinate(x, y);
        }

        public static void Reset()
        {
            _PressedDirection = Direction.None;
        }

    }
}
