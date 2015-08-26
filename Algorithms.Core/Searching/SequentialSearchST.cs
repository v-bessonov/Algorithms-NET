using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>SequentialSearchST</tt> class represents an (unordered)
    /// symbol table of generic key-value pairs.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides a <em>keys</em> method for iterating over all of the keys.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// The class also uses the convention that values cannot be <tt>null</tt>. Setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table
    /// <p>
    /// This implementation uses a singly-linked list and sequential search.
    /// It relies on the <tt>equals()</tt> method to test whether two keys
    /// are equal. It does not call either the <tt>compareTo()</tt> or
    /// <tt>hashCode()</tt> method.
    /// The <em>put</em> and <em>delete</em> operations take linear time; the
    /// <em>get</em> and <em>contains</em> operations takes linear time in the worst case.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SequentialSearchST<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {
        private int _n;           // number of key-value pairs
        private Node<TKey, TValue> _first;      // the linked list of key-value pairs

        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns>the number of key-value pairs in this symbol table</returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Is this symbol table empty?
        /// </summary>
        /// <returns><tt>true</tt> if this symbol table is empty and <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// Does this symbol table contain the given key?
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this symbol table contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        public bool Contains(TKey key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        public TValue Get(TKey key)
        {
            for (var x = _first; x != null; x = x.Next)
            {
                if (key.Equals(x.Key))
                    return x.Value;
            }
            return null;
        }

        /// <summary>
        /// Inserts the key-value pair into the symbol table, overwriting the old value
        /// with the new value if the key is already in the symbol table.
        /// f the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <param name="val">val the value</param>
        public void Put(TKey key, TValue val)
        {
            if (val == null)
            {
                Delete(key);
                return;
            }

            for (var x = _first; x != null; x = x.Next)
            {
                if (key.Equals(x.Key))
                {
                    x.Value = val;
                    return;
                }
            }
            _first = new Node<TKey, TValue>(key, val, _first);
            _n++;
        }

        /// <summary>
        /// Removes the key and associated value from the symbol table
        /// (if the key is in the symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        public void Delete(TKey key)
        {
            _first = Delete(_first, key);
        }

        /// <summary>
        /// delete key in linked list beginning at Node x
        /// warning: function call stack too large if table is large
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Delete(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            if (key.Equals(x.Key))
            {
                _n--;
                return x.Next;
            }
            x.Next = Delete(x.Next, key);
            return x;
        }

        /// <summary>
        /// Returns all keys in the symbol table as an <tt>Iterable</tt>.
        /// To iterate over all of the keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation
        /// </summary>
        /// <returns>all keys in the sybol table as an <tt>IEnumerable</tt></returns>
        public IEnumerable<TKey> Keys()
        {
            var queue = new Collections.Queue<TKey>();
            for (var x = _first; x != null; x = x.Next)
                queue.Enqueue(x.Key);
            return queue;
        }
    }
}
