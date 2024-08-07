using HsManCommonLibrary.Locks;
using HsManCommonLibrary.NestedValues;
using HsManCommonLibrary.NestedValues.NestedValueConverters;
using HsManCommonLibrary.NestedValues.NestedValueDeserializer;
using HsManCommonLibrary.ValueHolders;

namespace osuToolsV2.GameInfo.Config;

public class OsuConfig : INestedValueStore
{
    private readonly object _locker = new object();
    private readonly INestedValueStore _root = new CommonNestedValueStore(new Dictionary<string, object>());

    public OsuConfig(string configFile)
    {
        lock (_locker)
        {
            string[] lines = File.ReadAllLines(configFile);
            int commentIdx = 0;
            var rootConfig = _root.GetValueAs<Dictionary<string, object>>() ??
                             throw new InvalidOperationException();
            foreach (var line in lines)
            {
                if (line.StartsWith("//") || line.StartsWith("#") || string.IsNullOrEmpty(line))
                {
                    rootConfig.Add($"#Comment{commentIdx++}", line);
                    continue;
                }

                int colonIndex = line.IndexOf('=');
                if (colonIndex == -1)
                {
                    continue;
                }

                var propertyName = line.Substring(0, colonIndex).Trim();
                var valueStr = line.Substring(colonIndex + 1).Trim();
                rootConfig.Add(propertyName, valueStr);
            }
        }
    }

    public object? GetValue()
    {
        return _root.GetValue();
    }

    public T? GetValueAs<T>()
    {
        if (typeof(T) != typeof(Dictionary<string, object>))
        {
            throw new InvalidCastException();
        }

        return (T?)(object?)_root.GetValueAs<Dictionary<string, object>>();
    }

    public void SetValue(string key, INestedValueStore? val)
    {
        _root.SetValue(key, val);
    }

    private INestedValueStore? GetValue(string key)
    {
        return _root[key];
    }

    public INestedValueStore? this[string key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }

    public object? Convert(Type type)
    {
        if (type != typeof(Dictionary<string, object>))
        {
            throw new InvalidCastException();
        }

        return _root.GetValueAs<Dictionary<string, object>>();
    }

    public T? Convert<T>()
    {
        return (T?)Convert(typeof(T));
    }

    public object? ConvertWith(INestedValueStoreConverter converter)
    {
        return converter.Convert(this);
    }

    public T? ConvertWith<T>(INestedValueStoreConverter<T> converter)
    {
        return converter.Convert(this);
    }

    public bool IsNull(string key)
    {
        return _root.IsNull(key);
    }

    public T Deserialize<T>(INestedValueStoreDeserializer<T> storeDeserializer)
    {
        return storeDeserializer.Deserialize(this);
    }

    public ValueHolder<T> GetAsValueHolder<T>()
    {
        return new ValueHolder<T>((T?)_root.GetValue());
    }

    public ValueHolder<INestedValueStore> GetMemberAsValueHolder(string memberName)
    {
        return new ValueHolder<INestedValueStore>(_root[memberName]);
    }

    public ValueHolder<T> GetMemberValueAsValueHolder<T>(string memberName)
    {
        return new ValueHolder<T>((T?)_root[memberName]?.GetValue());
    }

    public bool TryGetValue<T>(out T? value)
    {
        value = (T)_root;
        return true;
    }

    public bool TryGetMember(string name, out INestedValueStore? value)
    {
        var val = GetValue(name);
        if (val != null)
        {
            value = val;
            return true;
        }

        value = null;
        return false;
    }

    public bool TryGetMemberValue<T>(string name, out T? value)
    {
        var val = GetValue(name)?.GetValue();
        if (val == null)
        {
            value = default;
            return false;
        }

        value = (T)val;
        return true;
    }
}