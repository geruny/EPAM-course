using System;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string input = "-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc";
            PrintL("Search query:");
            PrintL(input);
            PrintL();

            var studentSorter = new StudentSorter(new JsonStudents("TestResults.json"), new SearchHandler(input));
            var students = studentSorter.GetStudents();

            PrintL("Result:");
            foreach (var student in students)
                PrintL(student.ToString());
        }

        public static void PrintL(string str = "") => Console.WriteLine(str);
    }
}
