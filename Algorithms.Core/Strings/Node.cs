namespace Algorithms.Core.Strings
{
    /// <summary>
    /// R-way trie node
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Node<TValue> where TValue : class
    {
        public TValue Value { get; set; } // associated data
        public Node<TValue>[] Next { get; set; }

        public Node(int r)
        {
            Next = new Node<TValue>[r];
        }
    }
}
