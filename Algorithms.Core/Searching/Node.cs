namespace Algorithms.Core.Searching
{
    public class Node<TKey, TValue> where TKey : class where TValue : class
    {

        public int N { get; set; } // number of nodes in subtree

        public TKey Key { get; } // sorted by key

        public TValue Value { get; set; } // associated data

        public Node<TKey, TValue> Left { get; set; } //left subtree

        public Node<TKey, TValue> Right { get; set; } // right subtree

        public Node(TKey key, TValue val, int n)
        {
            Key = key;
            Value = val;
            N = n;
        }

    }
}
