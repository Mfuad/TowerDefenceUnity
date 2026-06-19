using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.Scripts.Utilities
{
    public class RefCountDictionary<TKey, TValue> where TKey : notnull
    {
        private class RefCountedItem
        {
            public TValue Value { get; set; }
            public int RefCount { get; set; }

            public RefCountedItem(TValue value)
            {
                Value = value;
                RefCount = 1;
            }
        }

        private readonly Dictionary<TKey, RefCountedItem> _dictionary = new();

        public void Add(TKey key,TValue value)
        {
            _dictionary.Add(key, new RefCountedItem(value));
        }

        public bool TryGet(TKey key, out TValue outValue)
        {
            outValue = default;
            if (_dictionary.TryGetValue(key, out var item))
            {
                item.RefCount++;
                outValue = item.Value;
                return true;
            }
            return false;
        }

        public bool TryRelease(TKey key, out TValue outValue)
        {
            outValue = default;
            if (_dictionary.TryGetValue(key, out var item))
            {
                item.RefCount--;

                if (item.RefCount <= 0)
                {
                    _dictionary.Remove(key);
                }

                outValue = item.Value;
                return true;
            }

            return false;
        }

        public int GetRefCount(TKey key)
        {
            return _dictionary.TryGetValue(key, out var item) ? item.RefCount : 0;
        }
    }
}
