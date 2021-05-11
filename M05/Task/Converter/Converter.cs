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
            _logger.LogInformation("Checking string for valid arguments");
            if (string.IsNullOrWhiteSpace(str))
            {
                var ex = new ArgumentException();
                _logger.LogError(ex, "Can not convert to int this string");
                throw ex;
            }

            str = str.Trim();

            bool negative = false;
            if (str[0] == '-')
            {
                negative = true;
                str = str.Trim('-');
            }

            _logger.LogInformation("Checking string characters");
            if (!str.All(char.IsDigit))
            {
                var ex = new ArgumentException();
                _logger.LogError(ex, "Not all values are numbers");
                throw ex;
            }


            _logger.LogInformation("Trying to parse");
            uint result = 0;
            foreach (var ch in str)
            {
                result *= 10;
                result += (uint)(ch - '0');

                if (negative == false & result > int.MaxValue | negative & -result < int.MinValue) {
                    var ex = new ArgumentOutOfRangeException();
                    _logger.LogError(ex, "Values in string out of range Int32");
                    throw ex;
                }
            }

            return negative ? -(int)result : (int)result;
        }
    }
}
