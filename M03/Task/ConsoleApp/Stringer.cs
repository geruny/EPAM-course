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
            if (str == "" | defChars == "")
                throw new ArgumentException("Some argument is empty");

            var chars = new HashSet<char>(defChars.Replace(" ", "").ToCharArray());
            var sb = new StringBuilder(str);

            for (var i = 0; i < sb.Length; i++)
                foreach (var ch in chars)
                    if (sb[i].Equals(ch))
                    {
                        sb.Insert(i, ch);
                        ++i;
                    }

            return sb.ToString();
        }

        public static string GetSumOfNums(string str1, string str2)
        {
            if (str1 == "" | str2 == "")
                throw new ArgumentException("Some argument is empty");

            if (str1.Length > str2.Length)
            {
                var temp = str1;
                str1 = str2;
                str2 = temp;
            }

            int n1 = str1.Length, n2 = str2.Length;
            int diff = n2 - n1;
            int carry = 0;
            var str = "";

            for (int i = n1 - 1; i >= 0; i--)
            {
                int sum = ((int)(str1[i] - '0') + (int)(str2[i + diff] - '0') + carry);
                str += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            for (int i = n2 - n1 - 1; i >= 0; i--)
            {
                int sum = ((int)(str2[i] - '0') + carry);
                str += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            if (carry > 0)
                str += (char)(carry + '0');

            var chars = str.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        public static string ReverseString(string str)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '(', ')' };
            var wordsArr = GetWordsFromString(str, delimiterChars);
            Array.Reverse(wordsArr);

            return string.Join(' ', wordsArr);
        }

        public static List<string> GetPhoneNumbers(string str)
        {
            var regex = new Regex(@"((\+\d{1,3})|\d{1})\s((\(\d{2,3}\))|(\d{2,3}))\s(\d{3})\-(\d{2,4})(\-(\d{2,4})|)", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(str);

            return matches.Select(match => match.Value).ToList();
        }

        private static string[] GetWordsFromString(string str, char[] delimiterChars)
        {
            if (str == "")
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
