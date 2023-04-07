namespace Terminal.Calculator
{
    public interface IShuntingYard
    {
        void ConvertToPostfix();
        double Evaluate();
        string[] GetPostfix();
    }
}