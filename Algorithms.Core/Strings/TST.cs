using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>TST</tt> class represents an symbol table of key-value
    /// pairs, with string keys and generic values.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides character-based methods for finding the string
    /// in the symbol table that is the <em>longest prefix</em> of a given prefix,
    /// finding all strings in the symbol table that <em>start with</em> a given prefix,
    /// and finding all strings in the symbol table that <em>match</em> a given pattern.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&amp;mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p/>
    /// This implementation uses a ternary search trie.
    /// <p/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class TST<TValue> where TValue : class
    {
        private int _n;              // size
        private Node<TValue> _root;   // root of TST



        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns>the number of key-value pairs in this symbol table</returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Does this symbol table contain the given key?
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this symbol table contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public bool Contains(string key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TValue Get(string key)
        {
            if (key == null) throw new NullReferenceException();
            if (key.Length == 0) throw new ArgumentException("key must have length >= 1");
            var x = Get(_root, key, 0);
            return x?.Value;
        }

        // return subtrie corresponding to given key
        private static Node<TValue> Get(Node<TValue> x, String key, int d)
        {
            if (key == null) throw new NullReferenceException();
            if (key.Length == 0) throw new ArgumentException("key must have length >= 1");
            if (x == null) return null;
            var c = key[d];
            if (c < x.Ch) return Get(x.Left, key, d);
            if (c > x.Ch) return Get(x.Right, key, d);
            if (d < key.Length - 1) return Get(x.Mid, key, d + 1);
            return x;
        }

        /// <summary>
        /// Inserts the key-value pair into the symbol table, overwriting the old value
        /// with the new value if the key is already in the symbol table.
        /// If the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <param name="val">val the value</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Put(string key, TValue val)
        {
            if (!Contains(key)) _n++;
            _root = Put(_root, key, val, 0);
        }

        private static Node<TValue> Put(Node<TValue> x, string key, TValue val, int d)
        {
            var c = key[d];
            if (x == null)
            {
                x = new Node<TValue> {Ch = c};
            }
            if (c < x.Ch) x.Left = Put(x.Left, key, val, d);
            else if (c > x.Ch) x.Right = Put(x.Right, key, val, d);
            else if (d < key.Length - 1) x.Mid = Put(x.Mid, key, val, d + 1);
            else x.Value = val;
            return x;
        }

        /// <summary>
        /// Returns the string in the symbol table that is the longest prefix of <tt>query</tt>,
        /// or <tt>null</tt>, if no such string.
        /// </summary>
        /// <param name="query">query the query string</param>
        /// <returns>the string in the symbol table that is the longest prefix of <tt>query</tt>, or <tt>null</tt> if no such string</returns>
        public string LongestPrefixOf(string query)
        {
            if (string.IsNullOrEmpty(query)) return null;
            var length = 0;
            var x = _root;
            var i = 0;
            while (x != null && i < query.Length)
            {
                var c = query[i];
                if (c < x.Ch) x = x.Left;
                else if (c > x.Ch) x = x.Right;
                else
                {
                    i++;
                    if (x.Value != null) length = i;
                    x = x.Mid;
                }
            }
            return query.Substring(0, length);
        }

        /// <summary>
        /// Returns all keys in the symbol table as an <tt>Iterable</tt>.
        /// To iterate over all of the keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation: <tt>for (Key key : st.keys())</tt>.
        /// </summary>
        /// <returns>all keys in the sybol table as an <tt>Iterable</tt></returns>
        public IEnumerable<string> Keys()
        {
            var queue = new Collections.Queue<string>();
            Collect(_root, new StringBuilder(), queue);
            return queue;
        }

        /// <summary>
        /// Returns all of the keys in the set that start with <tt>prefix</tt>.
        /// </summary>
        /// <param name="prefix">prefix the prefix</param>
        /// <returns>all of the keys in the set that start with <tt>prefix</tt>, as an iterable</returns>
        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            var queue = new Collections.Queue<string>();
            var x = Get(_root, prefix, 0);
            if (x == null) return queue;
            if (x.Value != null) queue.Enqueue(prefix);
            Collect(x.Mid, new StringBuilder(prefix), queue);
            return queue;
        }

        // all keys in subtrie rooted at x with given prefix
        private static void Collect(Node<TValue> x, StringBuilder prefix, Collections.Queue<string> queue)
        {
            if (x == null) return;
            Collect(x.Left, prefix, queue);
            if (x.Value != null) queue.Enqueue(prefix.ToString() + x.Ch);
            Collect(x.Mid, prefix.Append(x.Ch), queue);
            prefix.Remove(prefix.Length - 1, 1);
            Collect(x.Right, prefix, queue);
        }


        /// <summary>
        /// Returns all of the keys in the symbol table that match <tt>pattern</tt>,
        /// where . symbol is treated as a wildcard character.
        /// </summary>
        /// <param name="pattern">pattern the pattern</param>
        /// <returns>all of the keys in the symbol table that match <tt>pattern</tt>, as an iterable, where . is treated as a wildcard character.</returns>
        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            var queue = new Collections.Queue<string>();
            Collect(_root, new StringBuilder(), 0, pattern, queue);
            return queue;
        }

        private static void Collect(Node<TValue> x, StringBuilder prefix, int i, string pattern, Collections.Queue<string> queue)
        {
            if (x == null) return;
            var c = pattern[i];
            if (c == '.' || c < x.Ch) Collect(x.Left, prefix, i, pattern, queue);
            if (c == '.' || c == x.Ch)
            {
                if (i == pattern.Length - 1 && x.Value != null) queue.Enqueue(prefix.ToString() + x.Ch);
                if (i < pattern.Length - 1)
                {
                    Collect(x.Mid, prefix.Append(x.Ch), i + 1, pattern, queue);
                    prefix.Remove(prefix.Length - 1,1);
                }
            }
            if (c == '.' || c > x.Ch) Collect(x.Right, prefix, i, pattern, queue);
        }
    }
}
