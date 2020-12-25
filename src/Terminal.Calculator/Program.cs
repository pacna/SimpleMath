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
                if (IsFirstElementAnArithmeticOperator(input: input[0]))
                {
                    Console.WriteLine("Can't have an operator initialized in the beginning");
                }
                else if (HasValidInput(input: input))
                {
                    IShuntingYard sy = new ShuntingYard(input: input);
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

                Console.WriteLine("Press q and then ENTER to quit");
            }
        }

        private static bool HasValidInput(string input)
        {
            return !Helper.HasLetters(input: input) && !string.IsNullOrEmpty(input) && Helper.HasNumbers(input: input) && !Helper.HasInvalidSpecialCharacters(input: input);
        }

        private static bool IsFirstElementAnArithmeticOperator(char input)
        {
            return Helper.IsArithmeticOperator(input: input);
        }
    }
}
