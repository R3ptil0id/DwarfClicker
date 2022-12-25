using System;
using System.Collections.Generic;

namespace Utils.EnumExtensions
{
    public static class CollectionExtension
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            foreach (var s in source)
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));
                if (source == null)
                    throw new ArgumentNullException(nameof(source));

                collection.Add(s);
            }
        }
    }
}