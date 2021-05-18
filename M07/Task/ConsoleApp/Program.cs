using System;
using System.Runtime.InteropServices;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var matrix = new[,]
            {
                {7, 2, 6},
                {10, 7, 6},
                {8, 2, -3}
            };

            PrintL("Initial matrix");
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Print(matrix[i, j] + " ");
                }
                PrintL();
            }

            Matrix.Sort(matrix, Matrix.Operation.Asc,new RowMaxSort());

            PrintL("Sorted matrix");
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Print(matrix[i, j] + " ");
                }
                PrintL();
            }
        }

        private static void PrintL(string str = "") => Console.WriteLine(str);
        private static void Print(string str = "") => Console.Write(str);
    }
}
