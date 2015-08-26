using System;
using System.Text;

namespace Algorithms.Core.Context
{
    /// <summary>
    /// B-tree.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class BTree<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {
        private const int M = 4;    // max children per B-tree node = M-1

        private Node<TKey, TValue> _root;             // root of the B-tree
        private int _ht;                // height of the B-tree
        private int _n;                 // number of key-value pairs in the B-tree

        /// <summary>
        /// constructor
        /// </summary>
        public BTree()
        {
            _root = new Node<TKey, TValue>(0, M);
        }

        /// <summary>
        /// return number of key-value pairs in the B-tree
        /// </summary>
        /// <returns></returns>
        public int Size() { return _n; }

        /// <summary>
        /// return height of B-tree
        /// </summary>
        /// <returns></returns>
        public int Height() { return _ht; }

        /// <summary>
        /// search for given key, return associated value; return null if no such key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Get(TKey key) { return Search(_root, key, _ht); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        private TValue Search(Node<TKey, TValue> x, TKey key, int ht)
        {
            var children = x.Children;

            // external node
            if (ht == 0)
            {
                for (var j = 0; j < x.M; j++)
                {
                    if (Eq(key, children[j].Key)) return children[j].Value;
                }
            }

            // internal node
            else
            {
                for (var j = 0; j < x.M; j++)
                {
                    if (j + 1 == x.M || Less(key, children[j + 1].Key))
                        return Search(children[j].Next, key, ht - 1);
                }
            }
            return null;
        }


        /// <summary>
        /// insert key-value pair
        /// add code to check for duplicate keys
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put(TKey key, TValue value)
        {
            var u = Insert(_root, key, value, _ht);
            _n++;
            if (u == null) return;

            // need to split root
            var t = new Node<TKey, TValue>(2, M);
            t.Children[0] = new Entry<TKey, TValue>(_root.Children[0].Key, null, _root);
            t.Children[1] = new Entry<TKey, TValue>(u.Children[0].Key, null, u);
            _root = t;
            _ht++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Insert(Node<TKey, TValue> h, TKey key, TValue value, int ht)
        {
            int j;
            var t = new Entry<TKey, TValue>(key, value, null);

            // external node
            if (ht == 0)
            {
                for (j = 0; j < h.M; j++)
                {
                    if (Less(key, h.Children[j].Key)) break;
                }
            }

            // internal node
            else
            {
                for (j = 0; j < h.M; j++)
                {
                    if ((j + 1 == h.M) || Less(key, h.Children[j + 1].Key))
                    {
                        var u = Insert(h.Children[j++].Next, key, value, ht - 1);
                        if (u == null) return null;
                        t.Key = u.Children[0].Key;
                        t.Next = u;
                        break;
                    }
                }
            }

            for (var i = h.M; i > j; i--)
                h.Children[i] = h.Children[i - 1];
            h.Children[j] = t;
            h.M++;
            return h.M < M ? null : Split(h);
        }

        /// <summary>
        /// split node in half
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Split(Node<TKey, TValue> h)
        {
            var t = new Node<TKey, TValue>(M / 2, M);
            h.M = M / 2;
            for (var j = 0; j < M / 2; j++)
                t.Children[j] = h.Children[M / 2 + j];
            return t;
        }

        /// <summary>
        /// for debugging
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(_root, _ht, "") + "\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h"></param>
        /// <param name="ht"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        private string ToString(Node<TKey, TValue> h, int ht, string indent)
        {
            var s = new StringBuilder();
            var children = h.Children;

            if (ht == 0)
            {
                for (var j = 0; j < h.M; j++)
                {
                    s.Append(indent + children[j].Key + " " + children[j].Value + "\n");
                }
            }
            else
            {
                for (var j = 0; j < h.M; j++)
                {
                    if (j > 0) s.Append(indent + "(" + children[j].Key + ")\n");
                    s.Append(ToString(children[j].Next, ht - 1, indent + "     "));
                }
            }
            return s.ToString();
        }


        // comparison functions - make Comparable instead of Key to avoid casts
        private bool Less(TKey k1, TKey k2)
        {
            return k1.CompareTo(k2) < 0;
        }

        private bool Eq(TKey k1, TKey k2)
        {
            return k1.CompareTo(k2) == 0;
        }

    }
}
