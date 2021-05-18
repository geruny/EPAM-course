using System;

namespace ConsoleApp
{
    internal class RowMinSort : ISort
    {
        public void Sort(int[,] matrix, Matrix.Operation sortBy, Func<int[,], int, int, int[,]> swapRows)
        {
            var rowsMinEl = GetRowsMinEl(matrix);

            for (var i = 0; i < rowsMinEl.Length; i++)
                for (var j = 0; j < rowsMinEl.Length - 1 - i; j++)
                    if (rowsMinEl[j].CompareTo(rowsMinEl[j + 1]) == (int)sortBy)
                    {
                        (rowsMinEl[j], rowsMinEl[j + 1]) = (rowsMinEl[j + 1], rowsMinEl[j]);
                        matrix = swapRows(matrix, j, j + 1);
                    }
        }

        private static int[] GetRowsMinEl(int[,] array)
        {
            var rowsMinEl = new int[array.GetLength(0)];

            for (var i = 0; i < array.GetLength(0); i++)
            {
                var minEl = array[i, 0];
                for (var j = 0; j < array.GetLength(1) - 1; j++)
                {
                    var num = array[i, j] < array[i, j + 1] ? array[i, j] : array[i, j + 1];
                    minEl = minEl < num ? minEl : num;
                }
                rowsMinEl[i] = minEl;
            }

            return rowsMinEl;
        }
    }
}