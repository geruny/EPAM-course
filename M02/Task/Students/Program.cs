using System;
using System.Collections.Generic;
using System.Linq;

namespace Students
{
    class Program
    {
        static void Main(string[] args)
        {
            var subjects = new[] { "Maths", "Geometry", "Computer science", "English", "Physics", "Geography" };
            var listStudents = new List<Student>
            {
                new Student("vasya.pupkin@epam.com"),
                new Student("Vasya", "Pupkin"),

                new Student("german.petrov@epam.com"),
                new Student("German", "Petrov"),

                new Student("mikhail.fedorov@epam.com"),
                new Student("Mikhail", "Fedorov")
            };
            var studentSubjectDict = new Dictionary<Student, HashSet<string>>();
            var rnd = new Random();

            foreach (var student in listStudents)
                studentSubjectDict[student] = new HashSet<string>
                    (subjects.OrderBy(i => rnd.Next()).Take(3).ToArray());

            foreach (KeyValuePair<Student, HashSet<string>> keyValue in studentSubjectDict)
            {
                Console.Write("\n" + keyValue.Key.ToString() + " - ");

                foreach (var subject in keyValue.Value)
                    Console.Write(subject + " ");
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
