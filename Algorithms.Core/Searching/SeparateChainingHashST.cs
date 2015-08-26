using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>SeparateChainingHashST</tt> class represents a symbol table of generic
    /// key-value pairs.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides a <em>keys</em> method for iterating over all of the keys.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p>
    /// This implementation uses a separate chaining hash table. It requires that
    /// the key type overrides the <tt>equals()</tt> and <tt>hashCode()</tt> methods.
    /// The expected time per <em>put</em>, <em>contains</em>, or <em>remove</em>
    /// operation is constant, subject to the uniform hashing assumption.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SeparateChainingHashST<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {
        private const int INIT_CAPACITY = 4;

        // largest prime <= 2^i for i = 3 to 31
        // not currently used for doubling and shrinking
        // private static final int[] PRIMES = {
        //    7, 13, 31, 61, 127, 251, 509, 1021, 2039, 4093, 8191, 16381,
        //    32749, 65521, 131071, 262139, 524287, 1048573, 2097143, 4194301,
        //    8388593, 16777213, 33554393, 67108859, 134217689, 268435399,
        //    536870909, 1073741789, 2147483647
        // };

        private int _n;                                // number of key-value pairs
        private int _m;                                // hash table size
        private SequentialSearchST<TKey, TValue>[] _st;  // array of linked-list symbol tables


        /// <summary>
        /// Initializes an empty symbol table.
        /// </summary>
        public SeparateChainingHashST() : this(INIT_CAPACITY)
        {
            
        }

        /// <summary>
        /// Initializes an empty symbol table with <tt>M</tt> chains.
        /// </summary>
        /// <param name="m">M the initial number of chains</param>
        public SeparateChainingHashST(int m)
        {
            _m = m;
            _st = new SequentialSearchST<TKey, TValue>[m];
            for (var i = 0; i < m; i++)
                _st[i] = new SequentialSearchST<TKey, TValue>();
        }


        /// <summary>
        /// resize the hash table to have the given number of chains b rehashing all of the keys
        /// </summary>
        /// <param name="chains"></param>
        private void Resize(int chains)
        {
            var temp = new SeparateChainingHashST<TKey, TValue>(chains);
            for (var i = 0; i < _m; i++)
            {
                foreach (var key in _st[i].Keys())
                {
                    temp.Put(key, _st[i].Get(key));
                }
            }
            _m = temp._m;
            _n = temp._n;
            _st = temp._st;
        }

        /// <summary>
        /// hash value between 0 and M-1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _m;
        }

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
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public bool Contains(TKey key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TValue Get(TKey key)
        {
            var i = Hash(key);
            return _st[i].Get(key);
        }

        /// <summary>
        /// Inserts the key-value pair into the symbol table, overwriting the old value
        /// with the new value if the key is already in the symbol table.
        /// If the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <param name="val">val the value</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Put(TKey key, TValue val)
        {
            if (val == null)
            {
                Delete(key);
                return;
            }

            // double table size if average length of list >= 10
            if (_n >= 10 * _m) Resize(2 * _m);

            var i = Hash(key);
            if (!_st[i].Contains(key)) _n++;
            _st[i].Put(key, val);
        }

        /// <summary>
        /// Removes the key and associated value from the symbol table
        /// (if the key is in the symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            var i = Hash(key);
            if (_st[i].Contains(key)) _n--;
            _st[i].Delete(key);

            // halve table size if average length of list <= 2
            if (_m > INIT_CAPACITY && _n <= 2 * _m) Resize(_m / 2);
        }
        /// <summary>
        /// return keys in symbol table as an IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TKey> Keys()
        {
            var queue = new Collections.Queue<TKey>();
            for (var i = 0; i < _m; i++)
            {
                foreach (var key in _st[i].Keys())
                    queue.Enqueue(key);
            }
            return queue;
        }
    }
}
