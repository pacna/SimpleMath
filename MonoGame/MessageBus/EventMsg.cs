namespace SimpleMath.MonoGame.MessageBus;

public class EventMsg<TKey, TValue> : IMessageDoc
{
    public TKey Key { get; init; }
    public TValue Value { get; init; }

    object IMessageDoc.Key => this.Key;
    object IMessageDoc.Value => this.Value;
}
