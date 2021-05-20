using System;
using System.Collections;
using System.Linq;

namespace MyLibrary
{
    public class ClassLib
    {
        public static int BinarySearch<T>(T[] array, T search) where T : IComparable<T>
        {
            var high = array.Length - 1;
            var low = 0;

            if (array.First().Equals(search))
                return low;

            if (array.Last().Equals(search))
                return high;

            while (low <= high)
            {
                var mid = (high + low) / 2;
                switch (array[mid].CompareTo(search))
                {
                    case 0:
                        return mid;
                    case > 0:
                        high = mid - 1;
                        break;
                    case < 0:
                        low = mid + 1;
                        break;
                }
            }

            return -1;
        }

        public static IEnumerable Fibonacci(int countNums)
        {
            var a = 0;
            var b = 1;

            for (var i = 0; i < countNums; i++)
            {
                var temp = a;
                a = b;
                yield return a;
                b += temp;
            }
        }
    }
}
