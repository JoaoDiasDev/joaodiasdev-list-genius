using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Extensions.Cache
{
    public static class MemoryCacheExtensions
    {
        public static IEnumerable<object> GetKeys<T>(this IMemoryCache memoryCache)
        {
            if (memoryCache is MemoryCache cache)
            {
                if (cache
                        .GetType()
                        .GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                        .GetValue(cache) is ICollection<KeyValuePair<object, object>> entries)
                {
                    foreach (var entry in entries)
                    {
                        yield return entry.Key;
                    }
                }
            }
        }
    }
}
