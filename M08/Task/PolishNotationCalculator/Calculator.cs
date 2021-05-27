using System;
using System.Collections.Generic;
using System.Text;

namespace PolishNotationCalculator
{
    internal class Calculator
    {
        public static double Calculate(string input)
        {
            var stack = new Stack<double>();

            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    var digit = new StringBuilder();
                    while (!IsDelimiter(input[i]) & !IsOperator(input[i]))
                    {
                        digit.Append(input[i]);
                        i++;
                        if (i.Equals(input.Length)) break;
                    }
                    stack.Push(double.Parse(digit.ToString()));
                }
                else if (IsOperator(input[i]))
                {
                    var a = stack.Pop();
                    var b = stack.Pop();

                    var result = input[i] switch
                    {
                        '+' => b + a,
                        '-' => b - a,
                        '*' => b * a,
                        '/' => b / (a == 0 ? throw new DivideByZeroException() : a),
                        '^' => Math.Pow(b, a),
                    };

                    stack.Push(result);
                }
            }

            return stack.Peek();
        }

        private static bool IsDelimiter(char ch)
        {
            return " ".IndexOf(ch) != -1;
        }

        private static bool IsOperator(char ch)
        {
            return "+-/*^".IndexOf(ch) != -1;
        }
    }
}
