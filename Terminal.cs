using SimpleMath.PostfixMath;

namespace SimpleMath;

public class Terminal : IRun
{
    public void Run()
    {
        string input = string.Empty;
        while (input != "q")
        {
            Console.Write($"Pass in a math expression: ");
            input = Helper.RemoveWhiteSpace(Console.ReadLine());
            if (IsFirstElementAnArithmeticOperator(input: input[0]))
            {
                Console.WriteLine("Can't have an operator initialized in the beginning");
            }
            else if (HasValidInput(input: input))
            {
                ShuntingYard sy = new ShuntingYard(input: input);
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

    public void Dispose()
    {
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