using System;

namespace Geometric_Shapes
{
    internal abstract class Shape
    {
        public double Perimeter { get; protected set; }
        public double Area { get; protected set; }

        protected readonly double X;
        protected readonly double Y;

        protected Shape() { }

        protected Shape(double x) => X = x;

        protected Shape(double x, double y)
        {
            X = x;
            Y = y;
        }

        public abstract void CalculatePerimeter();

        public abstract void CalculateArea();

        public virtual void Print()
        {
            Console.WriteLine("Area: " + Area);
            Console.WriteLine("Perimeter: " + Perimeter);
        }
    }
}
