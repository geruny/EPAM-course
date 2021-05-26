using System;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string input = "-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc";
            PrintL("Search query:");
            PrintL(input);
            PrintL();

            var students = StudentSorter.GetStudents("TestResults.json", input);

            PrintL("Result:");
            foreach (var student in students)
                PrintL(student.ToString());
        }

        public static void PrintL(string str = "") => Console.WriteLine(str);
    }
}
