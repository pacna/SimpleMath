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

        public static bool HasLetters(string input)
        {
            foreach(var x in input)
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
            foreach(var x in input)
            {
                if (Char.IsDigit(x))
                {
                    return true;
                }
            }
            
            return false;
        }

        public static bool HasInvalidSpecialCharacters(string input)
        {
            return Regex.IsMatch(input, @"(?<=[.,;=!@#$~`{<%&~>}])");
        }
    }
}