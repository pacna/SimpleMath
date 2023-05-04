namespace SimpleMath.MonoGame.MessageBus;

public class MessageBusFactory
{
    private Bus _bus = Bus.Instance;
    private IEnumerable<IHandler> _handlers;

    private MessageBusFactory()
    {
    }

    public void AddHandlers(IEnumerable<IHandler> _handlers)
    {
        this._handlers = _handlers;
    }

    public void Publish<TKey, TValue>(TKey key, TValue value)
    {
        this._bus.Add(new EventMsg<TKey, TValue>()
        {
            Key = key,
            Value = value
        });
    }

    public void Run()
    {
        if (this._bus.IsEmpty())
        {
            return;
        }

        foreach(IHandler handler in this._handlers)
        {
            handler.HandleMsg(this._bus.Peek());
        }

        this._bus.Remove();
    }

    public static MessageBusFactory Instance => new();
}