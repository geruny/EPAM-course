using System;

namespace ConsoleApp
{
    internal class Matrix
    {
        public enum Operation
        {
            Asc = 1,
            Desc = -1
        }

        public static void Sort(int[,] matrix, Operation sortBy, ISort makeSort)
        {
            if (matrix == null)
                throw new ArgumentNullException();

            makeSort.Sort(matrix, sortBy, SwapRows);
        }

        private static int[,] SwapRows(int[,] array, int row1, int row2)
        {
            for (var i = 0; i < array.GetLength(1); i++)
            {
                (array[row1, i], array[row2, i]) = (array[row2, i], array[row1, i]);
            }

            return array;
        }
    }
}
