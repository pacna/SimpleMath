using SimpleMath.MonoGame.MessageBus;

namespace SimpleMath.MonoGame;

public static class Singleton
{
    public static GUICalculator Calculator = GUICalculator.Instance;
    public static MessageBusFactory MessageBus = MessageBusFactory.Instance;
}