namespace SimpleMath;

class Program
{
    static void Main(string[] args)
    {
        bool isTerminal = args.Contains("-terminal");

        using IRun engine = isTerminal ? new Terminal() : new Engine();
        engine.Run();
    }
}
