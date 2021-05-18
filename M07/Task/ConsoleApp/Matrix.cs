using System;

namespace MatrixSortApp
{
    internal class Matrix
    {
        public enum Operation
        {
            Asc = 1,
            Desc = -1
        }

        public static void Sort(int[,] matrix, Operation sortBy, Func<int[,], int[]> getRows)
        {
            if (matrix == null)
                throw new ArgumentNullException();

            var rows = getRows(matrix);

            for (var i = 0; i < rows.Length; i++)
                for (var j = 0; j < rows.Length - 1 - i; j++)
                    if (rows[j].CompareTo(rows[j + 1]) == (int)sortBy)
                    {
                        (rows[j], rows[j + 1]) = (rows[j + 1], rows[j]);
                        matrix = SwapRows(matrix, j, j + 1);
                    }
        }

        private static int[,] SwapRows(int[,] array, int row1, int row2)
        {
            for (var i = 0; i < array.GetLength(1); i++)
                (array[row1, i], array[row2, i]) = (array[row2, i], array[row1, i]);

            return array;
        }
    }
}
