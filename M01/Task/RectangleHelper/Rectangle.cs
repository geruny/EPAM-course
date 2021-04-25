using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleHelper
{
    public class Rectangle
    {
        private int _x;
        private int _y;

        public Rectangle(int x, int y)
        {
            if (x < 0 | y < 0)
                throw new Exception("Arguments must be positive!");

            _x = x;
            _y = y;
        }

        public int FindPerimeter() => 2 * (_x + _y);

        public int FindSquare() => _x * _y;

    }
}
