using System;
using System.Diagnostics;

namespace Performance
{
    class Program
    {
        struct S : IComparable<S>
        {
            public int I;

            public int CompareTo(S s) => this.I.CompareTo(s.I);
        }

        static void Main(string[] args)
        {
            var process = Process.GetCurrentProcess();
            var rnd = new Random();

            PrintL("Start time: " + process.StartTime);
            PrintL("Process Name: " + process.ProcessName);
            PrintL();

            //check classes memory

            var classes = new C[100000];
            var memoryBefore = process.PagedMemorySize64;
            PrintL("Memory before classes array initialization: " + memoryBefore);

            for (int i = 0; i < 100000; i++)
                classes[i] = new C() { I = rnd.Next() };

            process.Refresh();
            var memoryAfter = process.PagedMemorySize64;
            PrintL("Memory after classes array initialization: " + memoryAfter);
            var deltaClasses = memoryAfter - memoryBefore;
            PrintL("Delta: " + deltaClasses);
            PrintL();

            //check structs memory

            var structs = new S[100000];
            memoryBefore = process.PagedMemorySize64;
            PrintL("Memory before structs array initialization: " + memoryBefore);

            for (int i = 0; i < 100000; i++)
                structs[i] = new S() { I = rnd.Next() };

            process.Refresh();
            memoryAfter = process.PagedMemorySize64;
            var deltaStructs = memoryAfter - memoryBefore;
            PrintL("Memory after structs array initialization: " + memoryAfter);
            PrintL("Delta: " + deltaStructs);

            PrintL($"\nСlasses use more memory than structs by : {deltaClasses - deltaStructs}");

            // MemoryChecker(classes);
            // MemoryChecker(structs);

            //check classes sorting time

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Array.Sort(classes);
            stopwatch.Stop();

            PrintL(stopwatch.ElapsedMilliseconds.ToString());
        }

        public void TimeChecker<T>(Func<T[], T[]> func, T[] array)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            func(array);
            stopwatch.Stop();

            PrintL();
        }

        static void MemoryChecker<T>(T[] array) where T : new()
        {
            var process = Process.GetCurrentProcess();
            var rnd = new Random();

            var memoryBefore = process.PagedMemorySize64;
            PrintL("Memory before array initialization: " + memoryBefore);

            for (int i = 0; i < 100000; i++)
                // array[i] = new T() { I = rnd.Next() };

                process.Refresh();
            var memoryAfter = process.PagedMemorySize64;
            PrintL("Memory after array initialization: " + memoryAfter);
            PrintL($"Delta: {memoryAfter - memoryBefore}");
        }

        static void PrintL(string str = "") => Console.WriteLine(str);
        static void Print(string str = "") => Console.Write(str);
    }
}
