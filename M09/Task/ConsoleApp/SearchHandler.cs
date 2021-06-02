using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class SearchHandler : ISearchHandler<Student>
    {
        private static readonly List<string> Flags = new()
        {
            "-name",
            "-minmark",
            "-maxmark",
            "-datefrom",
            "-dateto",
            "-test"
        };
        private readonly string[] _words;

        public SearchHandler(string input)
        {
            _words = GetWordsFromString(input);
        }

        public IEnumerable<Student> SearchQuery(List<Student> collection)
        {
            var searchDict =
                Flags.ToDictionary(flag => flag, GetInputValues);

            var dateFrom = GetDateTime(searchDict["-datefrom"]);
            var dateTo = GetDateTime(searchDict["-dateto"]);

            var resQuery = collection
                .Where(n =>
                    n.Name.Equals(searchDict["-name"])
                    && n.Test.Subject.Equals(searchDict["-test"])
                    && n.Test.DateTime.CompareTo(dateFrom) >= 0
                    && n.Test.DateTime.CompareTo(dateTo) <= 0
                    && n.Test.Mark.CompareTo(Convert.ToInt32(searchDict["-minmark"])) >= 0
                    && n.Test.Mark.CompareTo(Convert.ToInt32(searchDict["-maxmark"])) <= 0
                );

            if (!resQuery.Any())
                throw new KeyNotFoundException("Students not found");

            return resQuery;
        }

        public string GetInputValues(string flag)
        {
            var flagIndex = _words.Where(n => n.Equals(flag))
                .Select(n => Array.IndexOf(_words, n))
                .ToList();

            if (!flagIndex.Any())
                throw new ArgumentException($"{flag} not found");

            if (flagIndex.Count > 1)
                throw new ArgumentException("Too many flags");

            var valueIndex = flagIndex[0] + 1;

            if (valueIndex >= _words.Length || _words[valueIndex].ToCharArray()[0] == '-')
                throw new ArgumentException($"Missing {_words[flagIndex[0]]} value");

            return _words[valueIndex];
        }

        public string GetSortMethod(string sortValue)
        {
            var sortValueIndex = _words.Where(n => n.Equals(sortValue))
                .Select(n => Array.IndexOf(_words, n))
                .ToList();

            var sortMethodIndex = sortValueIndex[0] + 1;

            if (sortMethodIndex >= _words.Length || _words[sortMethodIndex].ToCharArray()[0] == '-')
                throw new ArgumentException($"Missing sort method for {_words[sortValueIndex[0]]} ");

            return _words[sortMethodIndex];
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

            return str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
