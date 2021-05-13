using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //1
            var averageWordLength = Stringer.GetAverageWordLength("Create a class that implements a method which allows to define an average word length in an input string (returns double). (Punctuation characters should not affect the word lenght.Don't use regex.)");
            PrintL("#1 Average word length: " + averageWordLength);

            //2
            var doubleCharsStr = Stringer.DoubleDefChars("omg i love shrek", "o kek");
            PrintL("#2 String with define double chars: " + doubleCharsStr);

            //3
            var sumOfNum = Stringer.GetSumOfNums("123", "123");
            PrintL("#3 Sum of numbers in strings: " + sumOfNum);

            //4
            var reversedStr = Stringer.ReverseString("The greatest victory is that which requires no battle");
            PrintL("#4 Reversed string: " + reversedStr);

            //5
            var phoneNumbers = Stringer.GetPhoneNumbers(ReadFile(@"..\..\..\Text.txt"));
            Print("#5 Find phone numbers in text: ");
            WriteFile(@"..\..\..\Numbers.txt", phoneNumbers);
        }

        private static void PrintL(string str) => Console.WriteLine(str);
        private static void Print(string str) => Console.Write(str);

        private static void WriteFile(string path, List<string> textList)
        {
            using StreamWriter writer = File.CreateText(path);

            foreach (var str in textList)
                writer.WriteLine(str);

            PrintL($"File is written in \"{path}\"");
        }

        private static string ReadFile(string path)
        {
            using StreamReader reader = File.OpenText(path);

            return reader.ReadToEnd();
        }

    }
}
