using System;

namespace Game
{
    internal interface IGameConsole
    {
        protected static void Print(string str)
        {
            Console.WriteLine(str);
        }
    }
}
