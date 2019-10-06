using System.Collections.Generic;

public class DictionaryWithDefault<TKey, TValue> : Dictionary<TKey, TValue>
{
    private readonly TValue _default;

    public DictionaryWithDefault(TValue defaultValue) : base()
    {
        _default = defaultValue;
    }

    public new TValue this[TKey key]
    {
        get
        {
            TValue t;
            return base.TryGetValue(key, out t) ? t : _default;
        }
        set => base[key] = value;
    }
}
