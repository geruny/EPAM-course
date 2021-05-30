namespace MatrixSortApp
{
    internal class SortMethods
    {
        private enum FindInRow
        {
            MaxEl = 1,
            MinEl = -1
        }

        public static int[] SumRowValues(int[,] array)
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

        public static int[] MaxValueInRow(int[,] array)
        {
            return GetRowsArray(array, FindInRow.MaxEl);
        }

        public static int[] MinValueInRow(int[,] array)
        {
            return GetRowsArray(array, FindInRow.MinEl);
        }

        private static int[] GetRowsArray(int[,] array, FindInRow sortBy)
        {
            var rows = new int[array.GetLength(0)];

            for (var i = 0; i < array.GetLength(0); i++)
            {
                var el = array[i, 0];
                for (var j = 0; j < array.GetLength(1) - 1; j++)
                {
                    var num = array[i, j].CompareTo(array[i, j + 1]) == (int)sortBy ? array[i, j] : array[i, j + 1];
                    el = el.CompareTo(num) == (int)sortBy ? el : num;
                }
                rows[i] = el;
            }

            return rows;
        }
    }
}
