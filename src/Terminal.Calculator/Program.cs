using System;

namespace Terminal.Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            while(input != "q")
            {
                Console.Write($"Pass in a math expression: ");
                input = Helper.RemoveWhiteSpace(Console.ReadLine());
                if (HasValidInput(input))
                {
                    IShuntingYard sy = new ShuntingYard(input);
                    sy.ConvertToPostfix();
                    Console.WriteLine(sy.Evaluate());
                }
                else if (input == "q")
                {
                    Console.WriteLine("Bye | (• ◡•)| (❍ᴥ❍ʋ)");
                    return;
                }
                else 
                {
                    Console.WriteLine("Invalid input (ノ﹏ヽ)");
                }

                Console.WriteLine("Press q to quit");
            }
        }

        private static bool HasValidInput(string input)
        {
            return !Helper.HasLetters(input) && !string.IsNullOrEmpty(input) && Helper.HasNumbers(input) && !Helper.HasInvalidSpecialCharacters(input);
        }
    }
}
