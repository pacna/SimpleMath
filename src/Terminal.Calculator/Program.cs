using System;

namespace Terminal.Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write($"Pass in a math expression: ");
            string input = Console.ReadLine();
            ShuntingYard sy = new ShuntingYard(input);
            sy.ConvertToPostfix();
            sy.PrintQueue();
            sy.PrintStack();
        }
    }
}
