namespace Geometric_Shapes
{
    internal class Rectangle : Shape
    {
        public Rectangle(double x, double y) : base(x, y) { }

        public override void CalculatePerimeter() => Perimeter = 2 * (Y + X);

        public override void CalculateArea() => Area = X * Y;
    }
}
