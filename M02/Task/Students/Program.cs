using System;
using System.Collections.Generic;
using System.Linq;

namespace Students
{
    internal class Program
    {
        private static readonly string[] Subjects = { "Maths", "Geometry", "Computer science", "English", "Physics", "Geography" };
        private static readonly List<Student> ListStudents = new List<Student>
        {
            new Student("vasya.pupkin@epam.com"),
            new Student("Vasya", "Pupkin"),

            new Student("german.petrov@epam.com"),
            new Student("German", "Petrov"),

            new Student("mikhail.fedorov@epam.com"),
            new Student("Mikhail", "Fedorov")
        };

        private static void Main(string[] args)
        {
            var studentSubjectDict = InitializeDict();
            PrintDict(studentSubjectDict);

            Console.WriteLine();
            Console.ReadKey();
        }

        private static Dictionary<Student, HashSet<string>> InitializeDict()
        {
            var studentSubjectDict = new Dictionary<Student, HashSet<string>>();
            var rnd = new Random();

            foreach (var student in ListStudents)
                studentSubjectDict[student] = new HashSet<string>
                    (Subjects.OrderBy(i => rnd.Next()).Take(3).ToArray());

            return studentSubjectDict;
        }

        private static void PrintDict(Dictionary<Student, HashSet<string>> studentSubjectDict)
        {
            foreach (var keyValue in studentSubjectDict)
            {
                Console.Write("\n" + keyValue.Key.ToString() + " - ");

                foreach (var subject in keyValue.Value)
                    Console.Write(subject + " ");
            }
        }
    }
}
