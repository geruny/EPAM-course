using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp
{
    internal class StudentSorter
    {
        public static List<string> Flags = new()
        {
            "-name",
            "-minmark",
            "-maxmark",
            "-datefrom",
            "-dateto",
            "-test"
        };

        private static string[] _words;

        public static IEnumerable<Student> GetStudents(string path, string input)
        {
            var students = GetStudentsFromJson(path);

            _words = GetWordsFromString(input);

            var resQuery = SearchQuery(students);
            resQuery = SortQuery(resQuery);

            return resQuery;
        }

        private static IEnumerable<Student> SearchQuery(List<Student> students)
        {
            var searchDict =
                Flags.ToDictionary(flag => flag, GetInputValues);

            var dateFrom = GetDateTime(searchDict["-datefrom"]);
            var dateTo = GetDateTime(searchDict["-dateto"]);

            var resQuery = students
                .Select(n => n)
                .Where(n =>
                    n.Name.Equals(searchDict["-name"])
                    & n.Test.Subject.Equals(searchDict["-test"])
                    & n.Test.DateTime.CompareTo(dateFrom) >= 0
                    & n.Test.DateTime.CompareTo(dateTo) <= 0
                    & n.Test.Mark.CompareTo(Convert.ToInt32(searchDict["-minmark"])) >= 0
                    & n.Test.Mark.CompareTo(Convert.ToInt32(searchDict["-maxmark"])) <= 0
                );

            if (!resQuery.Any())
                throw new ArgumentNullException("Students not found");

            return resQuery;
        }

        private static IEnumerable<Student> SortQuery(IEnumerable<Student> resQuery)
        {
            var sortValue = GetInputValues("-sort");
            var sortMethod = GetSortMethod(sortValue);

            return sortMethod switch
            {
                "asc" => sortValue switch
                {
                    "name" => resQuery.OrderBy(n => n.LastName),
                    "mark" => resQuery.OrderBy(n => n.Test.Mark),
                    "date" => resQuery.OrderBy(n => n.Test.DateTime),
                    "test" => resQuery.OrderBy(n => n.Test.Subject),
                    _ => throw new ArgumentException("Unknown sort value")
                },
                "desc" => sortValue switch
                {
                    "name" => resQuery.OrderByDescending(n => n.LastName),
                    "mark" => resQuery.OrderByDescending(n => n.Test.Mark),
                    "date" => resQuery.OrderByDescending(n => n.Test.DateTime),
                    "test" => resQuery.OrderByDescending(n => n.Test.Subject),
                    _ => throw new ArgumentException("Unknown sort value")
                },
                _ => throw new ArgumentException("Unknown sort method")
            };
        }

        private static string GetInputValues(string flag)
        {
            var flagIndex = _words.Where(n => n.Equals(flag))
                .Select(n => Array.IndexOf(_words, n))
                .ToList();

            if (!flagIndex.Any())
                throw new ArgumentException($"{flag} not found");

            if (flagIndex.Count() > 1)
                throw new ArgumentException("Too many flags");

            var valueIndex = flagIndex[0] + 1;

            return _words[valueIndex];
        }

        private static string GetSortMethod(string sortValue)
        {
            var sortValueIndex = _words.Where(n => n.Equals(sortValue))
                .Select(n => Array.IndexOf(_words, n))
                .ToList();

            return _words[sortValueIndex[0] + 1];
        }

        private static DateTime GetDateTime(string dateStr)
        {
            var date = dateStr.Split('.')
                .Select(n => Convert.ToInt32(n))
                .ToList();

            return new DateTime(date[2], date[1], date[0]);
        }

        private static string[] GetWordsFromString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException("Empty string");

            return str.Split(" ");
        }

        public static List<Student> GetStudentsFromJson(string path)
        {
            string jsonString;
            using (var sr = new StreamReader(path))
                jsonString = sr.ReadToEnd();

            var studentsObj = JsonSerializer.Deserialize<JsonStudents>(jsonString);

            return studentsObj.Students ?? throw new ArgumentNullException();
        }
    }
}
