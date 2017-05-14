using System;
using System.Collections;
using System.Collections.Generic;

namespace HexMex.Game.Buildings
{
    // TODO Optimize
    public class FixedSizeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public int Count => InternalDictionary.Count;
        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => InternalDictionary[key];
            set => InternalDictionary[key] = value;
        }

        public ICollection<TKey> Keys => InternalDictionary.Keys;
        public ICollection<TValue> Values => InternalDictionary.Values;
        private Dictionary<TKey, TValue> InternalDictionary { get; } = new Dictionary<TKey, TValue>();

        public FixedSizeDictionary(IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                InternalDictionary.Add(key, default(TValue));
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key) => InternalDictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var value in InternalDictionary)
            {
                array[arrayIndex++] = value;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return InternalDictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}