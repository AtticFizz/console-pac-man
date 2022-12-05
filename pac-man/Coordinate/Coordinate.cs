using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public class Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public void Add(Coordinate coordinate)
        {
            X += coordinate.X;
            Y += coordinate.Y;
        }
        public void Add(int x, int y)
        {
            X += x;
            Y += y;
        }

        public static double GetLinearDistance(Coordinate left, Coordinate right)
        {
            int distanceX = left.X - right.X;
            int distanceY = left.Y - right.Y;
            return Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));
        }

        public bool IsInverse(Coordinate coordinate)
        {
            return Equals(Inverse(coordinate));
        }

        public Coordinate Inverse()
        {
            int inverseX = X * (-1);
            int inverseY = Y * (-1);

            return new Coordinate(inverseX, inverseY);
        }
        public Coordinate Inverse(Coordinate coordinate)
        {
            int inverseX = coordinate.X * (-1);
            int inverseY = coordinate.Y * (-1);

            return new Coordinate(inverseX, inverseY);
        }

        public bool Equals(Coordinate coordinate)
        {
            return X == coordinate.X && Y == coordinate.Y;
        }
        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }

        public override string ToString()
        {
            return $"({ X }, { Y })";
        }

        public static Coordinate operator +(Coordinate left, Coordinate right)
        {
            return new Coordinate(left.X + right.X, left.Y + right.Y);
        }

        public static Coordinate operator -(Coordinate left, Coordinate right)
        {
            return new Coordinate(left.X - right.X, left.Y - right.Y);
        }

        public static Coordinate operator *(int left, Coordinate right)
        {
            return new Coordinate(left * right.X, left * right.Y);
        }
    }
}
