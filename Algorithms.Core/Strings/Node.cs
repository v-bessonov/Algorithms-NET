﻿namespace Algorithms.Core.Strings
{
    /// <summary>
    /// R-way trie node
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Node<TValue> where TValue : class
    {
        public TValue Value { get; set; } // associated data
        public Node<TValue>[] Next { get; set; }

        public bool IsString { get; set; }

        public Node<TValue> Left { get; set; }
        public Node<TValue> Mid { get; set; }
        public Node<TValue> Right { get; set; }
        // left, middle, and right subtries

        public char Ch { get; set; }
        public Node()
        {
        }
        public Node(int r)
        {
            Next = new Node<TValue>[r];
        }
    }
}
