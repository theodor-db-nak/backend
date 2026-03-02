namespace Infrastructure.Persistence.Extensions;

public static class CollectionExtensions
{
    public static void Sync<TEntity, TKey>(
        this ICollection<TEntity> existingItems,
        IEnumerable<TEntity> updatedItems,
        Func<TEntity, TKey> keySelector) where TKey : notnull
    {
        var updatedKeys = updatedItems.Select(keySelector).ToHashSet();

        var toRemove = existingItems
            .Where(oldItem => !updatedKeys.Contains(keySelector(oldItem)))
            .ToList();

        foreach (var item in toRemove)
        {
            existingItems.Remove(item);
        }

        var existingKeys = existingItems.Select(keySelector).ToHashSet();
        var toAdd = updatedItems
            .Where(newItem => !existingKeys.Contains(keySelector(newItem)));

        foreach (var item in toAdd)
        {
            existingItems.Add(item);
        }
    }
}
