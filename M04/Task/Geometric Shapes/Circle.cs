using System;

namespace Geometric_Shapes
{
    internal class Circle : Shape
    {
        private readonly double _radius;

        public Circle(double x) : base(x) => _radius = x;

        public override void CalculatePerimeter()
        {
            Perimeter = 2 * Math.PI * _radius;
        }

        public override void CalculateArea()
        {
            Area = Math.PI * Math.Pow(_radius, 2);
        }
    }
}
