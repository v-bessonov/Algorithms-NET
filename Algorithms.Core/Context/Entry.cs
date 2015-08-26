namespace Algorithms.Core.Context
{
    /// <summary>
    /// internal nodes: only use key and next
    /// external nodes: only use key and value
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Entry<TKey, TValue> where TKey : class where TValue : class
    {
        public TKey Key { get; set; }
        public TValue Value { get; }
        public Node<TKey, TValue> Next { get; set; }  // helper field to iterate over array entries

        public Entry(TKey key, TValue value, Node<TKey, TValue> next)
        {
            Key = key;
            Value = value;
            Next = next;
        }

    }
}
