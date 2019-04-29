using System.Collections.Generic;
using DLToolkit.Forms.Controls;

namespace Photobook.Helpers
{
    public class Grouping<K, T> : FlowObservableCollection<T>
    {
        public Grouping(K key)
        {
            Key = key;
        }

        public Grouping(K key, IEnumerable<T> items)
            : this(key)
        {
            AddRange(items);
        }

        public Grouping(K key, IEnumerable<T> items, int columnCount)
            : this(key, items)
        {
            ColumnCount = columnCount;
        }

        public K Key { get; }
        public int ColumnCount { get; }
    }
}