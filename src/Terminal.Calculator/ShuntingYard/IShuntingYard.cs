using System.Collections.Generic;

namespace Terminal.Calculator
{
    public interface IShuntingYard
    {
        double Evaluate();

        void ConvertToPostfix();

        string GetPostfix();
    }
}