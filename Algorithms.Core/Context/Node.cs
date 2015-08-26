namespace Algorithms.Core.Context
{
    /// <summary>
    /// helper B-tree node data type
    /// </summary>
    public class Node<TKey, TValue> where TKey : class where TValue : class
    {
        public int M { get; set; } // number of children
        public Entry<TKey, TValue>[] Children { get; } // the array of children

        // create a node with k children
        public Node(int k, int maxChilren)
        {
            M = k;
            Children = new Entry<TKey, TValue>[maxChilren];
        }
    }
}
