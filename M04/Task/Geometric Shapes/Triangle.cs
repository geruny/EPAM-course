using System;

namespace Geometric_Shapes
{
    internal class Triangle : Shape
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        public Triangle(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public override void CalculatePerimeter()
        {
            Perimeter = _a + _b + _c;
        }

        public override void CalculateArea()
        {
            double p = Perimeter / 2;
            Area = Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
        }
    }
}
