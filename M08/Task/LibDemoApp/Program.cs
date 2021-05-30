using MyLibrary;
using System;

namespace LibDemoApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BinarySearchDemo();
            FibonacciDemo();
            StackIterationsDemo();
        }

        public static void BinarySearchDemo()
        {
            var array = new[] { 1, 2, 3 };
            var searchResult = ClassLib.BinarySearch<int>(array, 3);
            PrintL("Binary search result: " + searchResult);
            PrintL();
        }

        public static void FibonacciDemo()
        {
            var fibonacciEnumerable = ClassLib.Fibonacci(4);
            PrintL("Fibonacci enumerable:");
            foreach (var num in fibonacciEnumerable)
                PrintL(num.ToString());
            PrintL();
        }

        public static void StackIterationsDemo()
        {
            PrintL("Stack iterations:");
            var stack = new MyLibrary.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            foreach (var i in stack)
                PrintL(i.ToString());
        }

        public static void PrintL(string str = "") => Console.WriteLine(str);
    }
}
