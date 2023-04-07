using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Terminal.Calculator
{
    public static class Helper
    {
        public static string RemoveWhiteSpace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static bool IsArithmeticOperator(char input)
        {
            return input == '+' || input == '-' || input == '*' || input == '/' || input == '^';
        }

        public static bool HasLetters(string input)
        {
            foreach (char x in input)
            {
                if (Char.IsLetter(x))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasNumbers(string input)
        {
            foreach (char x in input)
            {
                if (Char.IsDigit(x))
                {
                    return true;
                }
            }

            return false;
        }

        public static string ConvertToString(string[] input)
        {
            return String.Concat(input);
        }

        public static bool IsNumber(string input)
        {
            return input.All(char.IsNumber);
        }

        public static string[] SplitOperatorsAndNumbers(string input)
        {
            return Regex.Split(input, @"\s*([()+\*/^-])\s*")
                    .Where(result => !string.IsNullOrEmpty(result))
                    .ToArray();
        }

        public static bool HasInvalidSpecialCharacters(string input)
        {
            return Regex.IsMatch(input, @"(?<=[.,;=!@#$~`{<%&~>}])");
        }
    }
}