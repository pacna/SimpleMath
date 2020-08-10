using System;
using System.Linq;

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
    }
}