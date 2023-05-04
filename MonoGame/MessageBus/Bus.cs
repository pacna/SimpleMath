namespace SimpleMath.MonoGame.MessageBus;

public class Bus
{
    private readonly Queue<IMessageDoc> _queue = new();

    private Bus()
    {
    }

    public void Add(IMessageDoc msg)
    {
        this._queue.Enqueue(item: msg);
    }

    public void Remove()
    {
        this._queue.Dequeue();
    }

    public IMessageDoc Peek()
    {
        return this._queue.Peek();
    }

    public bool IsEmpty()
    {
        return this._queue.IsNullOrEmpty();
    }

    public static Bus Instance => new();
}