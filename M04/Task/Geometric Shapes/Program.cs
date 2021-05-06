using System;

namespace Geometric_Shapes
{
    internal class Program
    {
        private static readonly Rectangle Rec = new(5, 6);
        private static readonly Circle Circle = new(4);
        private static readonly Square Square = new(2);
        private static readonly Triangle Triangle = new(2, 5, 6);

        private static void Main(string[] args)
        {
            TestShape(Rec);
            TestShape(Circle);
            TestShape(Square);
            TestShape(Triangle);
        }

        private static void TestShape(Shape shape)
        {
            PrintL(shape.ToString());
            shape.CalculatePerimeter();
            shape.CalculateArea();
            shape.Print();
            PrintL();
        }

        private static void PrintL(string str="") => Console.WriteLine(str);
    }
}
