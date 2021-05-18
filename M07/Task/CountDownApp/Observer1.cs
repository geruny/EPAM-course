using System;
using System.Threading;

namespace ObserverApp
{
    internal class Observer1
    {
        public static void PrintMessage(int count)
        {
            Thread.Sleep(900);
            Console.WriteLine($"{typeof(Observer1)} got notify {count}");
        }
    }
}
