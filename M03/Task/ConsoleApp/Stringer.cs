using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    internal class Stringer
    {
        public static double GetAverageWordLength(string str)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '(', ')' };
            var wordsArr = GetWordsFromString(str, delimiterChars);

            double totaLength = 0;
            foreach (var word in wordsArr)
                totaLength += word.Length;

            return totaLength / wordsArr.Count();
        }

        public static string DoubleDefChars(string str, string defChars)
        {
            if (string.IsNullOrWhiteSpace(str) | string.IsNullOrWhiteSpace(defChars))
                throw new ArgumentException("Some argument is empty");

            var chars = new HashSet<char>(defChars);
            var sb = new StringBuilder();

            foreach (var ch in str)
            {
                sb.Append(ch);
                if (chars.Contains(ch) & !char.IsWhiteSpace(ch))
                    sb.Append(ch);
            }

            return sb.ToString();
        }

        public static string GetSumOfNums(string str1, string str2)
        {
            if (string.IsNullOrWhiteSpace(str1) | string.IsNullOrWhiteSpace(str2))
                throw new ArgumentException("Some argument is empty");

            if (str1.Length > str2.Length)
                (str1, str2) = (str2, str1);

            int str1Length = str1.Length, str2Length = str2.Length;
            int diff = str2Length - str1Length;
            int carry = 0;
            var str = new StringBuilder();

            for (int i = str1Length - 1; i >= 0; i--)
            {
                int sum = ToInt(str1[i]) + ToInt(str2[i + diff]) + carry;
                str.Append(ToChar(sum));
                carry = sum / 10;
            }

            for (int i = str2Length - str1Length - 1; i >= 0; i--)
            {
                int sum = ToInt(str2[i]) + carry;
                str.Append(ToChar(sum));
                carry = sum / 10;
            }

            if (carry > 0)
                str.Append(ToChar(carry));

            var chars = str.ToString().ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        private static int ToInt(char ch) => (int)(ch - '0');
        private static char ToChar(int sum) => (char)(sum % 10 + '0');

        public static string ReverseString(string str)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '(', ')' };
            var wordsArr = GetWordsFromString(str, delimiterChars);
            Array.Reverse(wordsArr);

            return string.Join(' ', wordsArr);
        }

        public static List<string> GetPhoneNumbers(string str)
        {
            var regex = new Regex(@"^((\+(\d|\d{3}))|(\s\d))\s((\(\d{2,3}\))|(\d{3}))\s(\d{3})\-((\d{2}\-\d{2})|\d{4})$", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(str);

            return matches.Select(match => match.Value).ToList();
        }

        private static string[] GetWordsFromString(string str, char[] delimiterChars)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException("Empty string");

            var words =
                from word in str.Split(delimiterChars)
                where word != ""
                select word;

            var wordsArr = words.ToArray();

            if (!wordsArr.Any())
                throw new ArgumentException("There are no words in string");

            return wordsArr;
        }
    }
}
