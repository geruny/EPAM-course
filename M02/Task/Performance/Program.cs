using System;
using System.Diagnostics;

namespace Performance
{
    class Program
    {
        struct S : IComparable<S>, IMemoryChecker
        {
            public int I { get; set; }
            public int CompareTo(S s) => this.I.CompareTo(s.I);
        }

        delegate void ArraySort<T>(T[] array);

        static void Main(string[] args)
        {
            var process = Process.GetCurrentProcess();

            PrintL("Start time: " + process.StartTime);
            PrintL("Process Name: " + process.ProcessName);
            PrintL();

            //classes tests

            PrintL("--Classes tests--");
            var classes = new C[100000];
            var classesDelta = MemoryChecker(classes);
            PrintL();

            //structs tests

            PrintL("--Structs tests--");
            var structs = new S[100000];
            var structsDelta = MemoryChecker(structs);
            PrintL();

            PrintL($"ClassesDelta - structsDelta: {classesDelta - structsDelta}");
            PrintL();
        }

        static long MemoryChecker<T>(T[] array) where T : IMemoryChecker, new()
        {
            var process = Process.GetCurrentProcess();
            var rnd = new Random();

            var memoryBefore = process.PagedMemorySize64;
            PrintL("Memory before array initialization: " + memoryBefore);

            for (int i = 0; i < 100000; i++)
                array[i] = new T() { I = rnd.Next() };

            process.Refresh();
            var memoryAfter = process.PagedMemorySize64;
            var delta = memoryAfter - memoryBefore;
            PrintL("Memory after array initialization: " + memoryAfter);
            PrintL("Delta: " + delta);

            TimeChecker<T>(Array.Sort, array);

            return delta;
        }

        static void TimeChecker<T>(ArraySort<T> arraySort, T[] array)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            arraySort(array);
            stopwatch.Stop();

            PrintL("Run time sorting: " + stopwatch.ElapsedMilliseconds);
        }

        static void PrintL(string str = "") => Console.WriteLine(str);
    }
}
