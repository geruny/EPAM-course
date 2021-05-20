using System;

namespace PolishNotationCalculator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var calc = Calculator.Calculate("5 1 2 + 4 * + 3 -");
            Console.WriteLine("Result: " + calc);
        }
    }
}
