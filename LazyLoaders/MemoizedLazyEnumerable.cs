using System.Collections;

namespace osuToolsV2.LazyLoaders;

public class MemoizedLazyEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _source;
    private List<T>? _cache;

    public MemoizedLazyEnumerable(IEnumerable<T> source)
    {
        _source = source;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (_cache != null)
        {
            foreach (var item in _cache)
            {
                yield return item;
            }
        }

        _cache = new List<T>();
        foreach (var item in _source)
        {
            _cache.Add(item);
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}