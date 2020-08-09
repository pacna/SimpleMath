using System.Collections.Generic;

namespace Terminal.Calculator
{
    public interface IShuntingYard
    {
        void Output();

        void ConvertToPostfix();

        string GetPostfix();
    }
}