using System;
using System.Diagnostics;

namespace Performance
{
    internal class Program
    {
        private delegate void ArraySort<T>(T[] array);

        private static void Main(string[] args)
        {
            using (var process = Process.GetCurrentProcess())
            {
                PrintL("Start time: " + process.StartTime);
                PrintL("Process Name: " + process.ProcessName);
            }
            PrintL();

            //classes tests

            PrintL("--Classes tests--");
            var classes = new C[100000];
            var classesDelta = MemoryChecker(classes);
            TimeChecker<C>(Array.Sort, classes);
            PrintL();

            //structs tests

            PrintL("--Structs tests--");
            var structs = new S[100000];
            var structsDelta = MemoryChecker(structs);
            TimeChecker<S>(Array.Sort, structs);
            PrintL();

            PrintL($"ClassesDelta - structsDelta: {classesDelta - structsDelta}");
            PrintL();
        }

        private static long MemoryChecker<T>(T[] array) where T : IMemoryChecker, new()
        {
            var rnd = new Random();
            using var process = Process.GetCurrentProcess();
                
            var memoryBefore = process.PagedMemorySize64;
            PrintL("Memory before array initialization: " + memoryBefore);

            for (int i = 0; i < 100000; i++)
                array[i] = new T() { I = rnd.Next() };

            process.Refresh();
            var memoryAfter = process.PagedMemorySize64;
            var delta = memoryAfter - memoryBefore;
            PrintL("Memory after array initialization: " + memoryAfter);
            PrintL("Delta: " + delta);

            return delta;
        }

        private static void TimeChecker<T>(ArraySort<T> arraySort, T[] array)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            arraySort(array);
            stopwatch.Stop();

            PrintL("Run time sorting: " + stopwatch.ElapsedMilliseconds);
        }

        private static void PrintL(string str = "") => Console.WriteLine(str);
    }
}
