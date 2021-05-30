using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MyLib
{
    public class Converter
    {
        private static ILogger _logger;

        public Converter(ILogger<Converter> logger)
        {
            _logger = logger;
        }

        public static int StringToInt(string str)
        {
            str = str.Trim();

            CheckStringNotNull(str);

            var negative = CheckStringIsNegative(str);
            if(negative)
                str = str.Trim('-');

            CheckStringIsDigit(str);

            _logger.LogInformation("Trying to parse");
            var result = 0;
            foreach (var ch in str)
            {
                try
                {
                    checked
                    {
                        result *= 10;
                        result += ch - '0';
                    }
                }
                catch (OverflowException ex)
                {
                    _logger.LogError(ex, "Values in string out of range Int32");
                    throw;
                }
            }

            return negative ? -result : result;
        }

        internal static void CheckStringNotNull(string str)
        {
            _logger.LogInformation("Checking string for valid arguments");
            if (!string.IsNullOrEmpty(str)) return;

            var ex = new ArgumentException();
            _logger.LogError(ex, "Can not convert to int this string");
            throw ex;
        }

        internal static void CheckStringIsDigit(string str)
        {
            _logger.LogInformation("Checking string characters");
            if (str.All(char.IsDigit)) return;

            var ex = new ArgumentException();
            _logger.LogError(ex, "Not all values are numbers");
            throw ex;
        }

        internal static bool CheckStringIsNegative(string str) => str[0] == '-';
    }
}
