using ArrayHelper;
using RectangleHelper;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayHelperApp();
            RectangleHelperApp();

            Console.ReadKey();
        }

        static void ArrayHelperApp()
        {
            Console.WriteLine("--Array helper--\n");

            var rand = new Random();

            //Bubble sort

            var array = new int[100];

            Console.WriteLine("Initial array:");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-99, 99);
                Console.Write(array[i] + " ");
            }

            BubbleSort<int>.SortAsc(array);
            Console.WriteLine("\n\nSorted by ASC:");
            foreach (var i in array)
                Console.Write(i + " ");

            BubbleSort<int>.SortDesc(array);
            Console.WriteLine("\n\nSorted by DESC:");
            foreach (var i in array)
                Console.Write(i + " ");

            //Calculator

            int rowCount = 10, columnCount = 5;
            var array2 = new int[rowCount, columnCount];

            Console.WriteLine("\n\nInitial two dimensional array:");
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    array2[i, j] = rand.Next(-99, 99);
                    Console.Write(array2[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nResult sum: {Calculator.FindSum2DimArray(array2)}");
        }

        static void RectangleHelperApp()
        {
            Console.WriteLine("\n--Rectangle helper--\n");

            var rectangle = new Rectangle(5, 6);

            Console.WriteLine("Perimeter: " + rectangle.FindPerimeter());
            Console.WriteLine("Square: " + rectangle.FindSquare());
        }

    }
}
