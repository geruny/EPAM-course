using System;
using System.Threading;

namespace ObserverApp
{
    internal class Observer2
    {
        public static void PrintMessage(int count)
        {
            Thread.Sleep(900);
            Console.WriteLine($"{typeof(Observer2)} got notify {count}");
        }
    }
}
