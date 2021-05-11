using System;

namespace RectangleHelper
{
    public class Rectangle
    {
        private int _x;
        private int _y;

        public Rectangle(int x, int y)
        {
            if (x < 0 | y < 0)
                throw new ArgumentException("Arguments must be positive!");

            _x = x;
            _y = y;
        }

        public int FindPerimeter()
        {
            return 2 * (_x + _y);
        }

        public int FindSquare()
        {
            return _x * _y;
        }
    }
}
