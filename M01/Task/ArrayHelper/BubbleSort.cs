using System;

namespace ArrayHelper
{
    public class BubbleSort<T> where T : IComparable
    {

        public static void SortAsc(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length - 1 - i; j++)
                    if (array[j].CompareTo(array[j + 1]) == 1)
                    {
                        var temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
        }

        public static void SortDesc(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length - 1 - i; j++)
                    if (array[j].CompareTo(array[j + 1]) == -1)
                    {
                        var temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
        }

    }
}
