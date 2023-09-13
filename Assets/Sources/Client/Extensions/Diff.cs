using System;
using System.Collections.Generic;
using System.Linq;

namespace Sources.Client.Extensions
{
    public static partial class Extensions
    {
        public static (IEnumerable<T> Added, IEnumerable<T> Removed) Diff<T>(this IEnumerable<T> sourceCollection,
            IEnumerable<T> changedCollection, Func<T, T, bool> comparer)
        {
            List<T> removed = new List<T>();
            List<T> targets = changedCollection.ToList();

            foreach (T item in sourceCollection)
            {
                T sameItem = targets.FirstOrDefault(targetItem => comparer.Invoke(targetItem, item));

                if (sameItem == null)
                    removed.Add(item);
                else
                    targets.Remove(sameItem);
            }

            return (targets, removed);
        }
    }
}