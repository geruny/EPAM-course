using System;

namespace ConsoleApp
{
    internal class RowSumSort : ISort
    {
        public void Sort(int[,] matrix, Matrix.Operation sortBy, Func<int[,], int, int, int[,]> swapRows)
        {
            var rowsSums = GetRowsSums(matrix);

            for (var i = 0; i < rowsSums.Length; i++)
                for (var j = 0; j < rowsSums.Length - 1 - i; j++)
                    if (rowsSums[j].CompareTo(rowsSums[j + 1]) == (int)sortBy)
                    {
                        (rowsSums[j], rowsSums[j + 1]) = (rowsSums[j + 1], rowsSums[j]);
                        matrix = swapRows(matrix, j, j + 1);
                    }
        }

        private static int[] GetRowsSums(int[,] array)
        {
            var rowsSums = new int[array.GetLength(0)];

            for (var i = 0; i < array.GetLength(0); i++)
            {
                var sum = 0;
                for (var j = 0; j < array.GetLength(1); j++)
                    sum += array[i, j];

                rowsSums[i] = sum;
            }

            return rowsSums;
        }
    }
}
