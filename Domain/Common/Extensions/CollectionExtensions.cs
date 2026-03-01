namespace Domain.Common.Extensions;

public static class CollectionExtensions
{
    public static void AddOrRemove<T>(
    this ICollection<T> collection,
    T? item, 
    Func<T, bool> predicate,
    bool isAdding) where T : class
    {
        var existingItem = collection.FirstOrDefault(predicate);

        if (isAdding)
        {
            if (existingItem == null && item != null)
                collection.Add(item);
        }
        else
        {
            if (existingItem != null)
                collection.Remove(existingItem);
        }
    }
}
