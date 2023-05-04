namespace SimpleMath.MonoGame.MessageBus;

public interface IHandler
{
    void HandleMsg(IMessageDoc msg);
}