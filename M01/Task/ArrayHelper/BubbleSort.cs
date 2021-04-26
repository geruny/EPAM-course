using System;

namespace ArrayHelper
{
    public class BubbleSort<T> where T : IComparable
    {
        public enum Operation
        {
            Asc = 1,
            Desc = -1
        }

        public static void Sort(T[] array, Operation sortBy)
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length - 1 - i; j++)
                    if (array[j].CompareTo(array[j + 1]) == (int)sortBy)
                    {
                        var temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
        }
    }
}
