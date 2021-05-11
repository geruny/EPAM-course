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
            PrintL("--Array helper--\n");

            var rand = new Random();

            //Bubble sort

            var array = new int[100];

            PrintL("Initial array:");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-99, 99);
                Print(array[i] + " ");
            }

            var Asc = BubbleSort<int>.Operation.Asc;
            var Desc = BubbleSort<int>.Operation.Desc;

            BubbleSort<int>.Sort(array, Asc);
            PrintL("\n\nSorted by ASC:");
            foreach (var i in array)
                Print(i + " ");

            BubbleSort<int>.Sort(array, Desc);
            PrintL("\n\nSorted by DESC:");
            foreach (var i in array)
                Print(i + " ");

            //Calculator

            int rowCount = 10, columnCount = 5;
            var array2 = new int[rowCount, columnCount];

            PrintL("\n\nInitial two dimensional array:");
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    array2[i, j] = rand.Next(-99, 99);
                    Print(array2[i, j] + " ");
                }
                PrintL();
            }
            PrintL($"\nResult sum: {Calculator.FindSum2DimArray(array2)}");
        }

        static void RectangleHelperApp()
        {
            PrintL("\n--Rectangle helper--\n");

            var rectangle = new Rectangle(5, 6);

            PrintL("Perimeter: " + rectangle.FindPerimeter());
            PrintL("Square: " + rectangle.FindSquare());
        }

        static void PrintL(string str = "")
        {
            Console.WriteLine(str);
        }

        static void Print(string str)
        {
            Console.Write(str);
        }
    }
}
