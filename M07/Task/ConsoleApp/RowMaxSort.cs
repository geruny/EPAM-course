using System;

namespace ConsoleApp
{
    internal class RowMaxSort : ISort
    {
        public void Sort(int[,] matrix, Matrix.Operation sortBy, Func<int[,], int, int, int[,]> swapRows)
        {
            var rowsMaxEl = GetRowsMaxEl(matrix);

            for (var i = 0; i < rowsMaxEl.Length; i++)
                for (var j = 0; j < rowsMaxEl.Length - 1 - i; j++)
                    if (rowsMaxEl[j].CompareTo(rowsMaxEl[j + 1]) == (int)sortBy)
                    {
                        (rowsMaxEl[j], rowsMaxEl[j + 1]) = (rowsMaxEl[j + 1], rowsMaxEl[j]);
                        matrix = swapRows(matrix, j, j + 1);
                    }
        }

        private static int[] GetRowsMaxEl(int[,] array)
        {
            var rowsMaxEl = new int[array.GetLength(0)];

            for (var i = 0; i < array.GetLength(0); i++)
            {
                var maxEl = array[i, 0];

                for (var j = 0; j < array.GetLength(1) - 1; j++)
                {
                    var num = array[i, j] > array[i, j + 1] ? array[i, j] : array[i, j + 1];
                    maxEl = maxEl > num ? maxEl : num;
                }
                rowsMaxEl[i] = maxEl;
            }

            return rowsMaxEl;
        }
    }
}
