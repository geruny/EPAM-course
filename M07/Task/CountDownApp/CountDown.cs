using System;

namespace ObserverApp
{
    internal class CountDown
    {
        public delegate void CountHandler(int count);
        public static event CountHandler Notify;

        public static void CountDownMethod()
        {
            for (var i = 10; i > 0; i--)
            {
                Console.WriteLine();
                Console.WriteLine(i);
                Notify?.Invoke(i);
            }
        }
    }
}
