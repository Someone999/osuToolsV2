namespace osuToolsV2;

class GroupCollector<TKey, TValue> where TKey: notnull
{
    class GroupSelector
    {
        public GroupSelector(TKey key, Func<TValue, bool> predict)
        {
            Key = key;
            Predict = predict;
        }

        public TKey Key { get; set; }
        public Func<TValue, bool> Predict { get; set; }
    }
    private readonly TValue[] _array;
    private List<GroupSelector> _selectors = new List<GroupSelector>();
    public GroupCollector(IEnumerable<TValue> enumerable)
    {
        _array = enumerable.ToArray();
    }

    public void AddGroup(TKey key, Func<TValue, bool> predict)
    {
        _selectors.Add(new GroupSelector(key, predict));
    }
    
    public Dictionary<TKey, TValue[]> CollectGroups()
    {
        var dict = new Dictionary<TKey, List<TValue>>();
    
        foreach (var item in _array)
        {
            foreach (var selector in _selectors)
            {
                if (!selector.Predict(item))
                {
                    continue;
                }

                if (!dict.TryGetValue(selector.Key, out var list))
                {
                    list = new List<TValue>();
                    dict[selector.Key] = list;
                }
            
                list.Add(item);
            }
        }
        
        return dict.ToDictionary(pair => pair.Key, pair => pair.Value.ToArray());
    }
}