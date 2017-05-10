using System;
using System.Collections.Generic;
using System.Linq;

namespace HexMex.Helper
{
    public static class LinqExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> source, T element)
        {
            var s = source.ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (Equals(s[i], element))
                    return i;
            }
            return -1;
        }

        public static T GetElementAfter<T>(this T[] source, T element)
        {
            for (int i = 0; i < source.Length - 1; i++)
            {
                if (Equals(source[i], element))
                    return source[i + 1];
            }
            throw new ArgumentException("No element found/element is last entry", nameof(element));
        }

        public static IEnumerable<TKey> Unique<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> selector)
        {
            var counter = new Dictionary<TKey, int>();
            foreach (var element in source)
            {
                var key = selector(element);
                if (counter.ContainsKey(key))
                    counter[key]++;
                else
                    counter.Add(key, 1);
            }
            foreach (var kvp in counter)
            {
                if (kvp.Value == 1)
                    yield return kvp.Key;
            }
        }
    }
}