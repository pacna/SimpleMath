namespace SimpleMath;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>(this IList<T> item)
    {
        return item == null || item.Count == 0;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> item)
    {
        return item == null || !item.Any();
    }
}