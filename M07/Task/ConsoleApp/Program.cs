using System;

namespace MatrixSortApp
{
    internal class Program
    {
        private static int[,] _matrix =
        {
            {7, 2, 6},
            {10, 7, 6},
            {8, 2, -3}
        };

        private static void Main(string[] args)
        {
            PrintL("Initial matrix");
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    Print(_matrix[i, j] + " ");
                }
                PrintL();
            }

            Matrix.Sort(_matrix, Matrix.Operation.Asc, SortMethods.MinValueInRow);

            PrintL();
            PrintL("Sorted matrix");
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    Print(_matrix[i, j] + " ");
                }
                PrintL();
            }
        }

        private static void PrintL(string str = "") => Console.WriteLine(str);
        private static void Print(string str = "") => Console.Write(str);
    }
}
