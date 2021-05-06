namespace Geometric_Shapes
{
    internal class Square : Shape
    {
        public Square(double x) : base(x) { }

        public override void CalculatePerimeter()
        {
            Perimeter = 4 * X;
        }

        public override void CalculateArea()
        {
            Area = X * X;
        }
    }
}
