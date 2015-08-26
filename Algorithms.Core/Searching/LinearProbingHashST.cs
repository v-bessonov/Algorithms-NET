using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>LinearProbingHashST</tt> class represents a symbol table of generic
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
    /// This implementation uses a linear probing hash table. It requires that
    /// the key type overrides the <tt>equals()</tt> and <tt>hashCode()</tt> methods.
    /// The expected time per <em>put</em>, <em>contains</em>, or <em>remove</em>
    /// operation is constant, subject to the uniform hashing assumption.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class LinearProbingHashST<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {

        private const int INIT_CAPACITY = 4;

        private int _n;           // number of key-value pairs in the symbol table
        private int _m;           // size of linear probing table
        private TKey[] _keys;      // the keys
        private TValue[] _vals;    // the values

        /// <summary>
        /// Initializes an empty symbol table.
        /// </summary>
        public LinearProbingHashST():this(INIT_CAPACITY)
        {
            
        }

        /// <summary>
        /// Initializes an empty symbol table of given initial capacity.
        /// </summary>
        /// <param name="capacity">capacity the initial capacity</param>
        public LinearProbingHashST(int capacity)
        {
            _m = capacity;
            _keys = new TKey[_m];
            _vals = new TValue[_m];
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
        /// hash function for keys - returns value between 0 and M-1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _m;
        }

        /// <summary>
        /// resize the hash table to the given capacity by re-hashing all of the keys
        /// </summary>
        /// <param name="capacity"></param>
        private void Resize(int capacity)
        {
            var temp = new LinearProbingHashST<TKey, TValue>(capacity);
            for (var i = 0; i < _m; i++)
            {
                if (_keys[i] != null)
                {
                    temp.Put(_keys[i], _vals[i]);
                }
            }
            _keys = temp._keys;
            _vals = temp._vals;
            _m = temp._m;
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

            // double table size if 50% full
            if (_n >= _m / 2) Resize(2 * _m);

            int i;
            for (i = Hash(key); _keys[i] != null; i = (i + 1) % _m)
            {
                if (_keys[i].Equals(key))
                {
                    _vals[i] = val;
                    return;
                }
            }
            _keys[i] = key;
            _vals[i] = val;
            _n++;
        }

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TValue Get(TKey key)
        {
            for (int i = Hash(key); _keys[i] != null; i = (i + 1) % _m)
                if (_keys[i].Equals(key))
                    return _vals[i];
            return null;
        }

        /// <summary>
        /// Removes the key and associated value from the symbol table
        /// (if the key is in the symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            if (!Contains(key)) return;

            // find position i of key
            var i = Hash(key);
            while (!key.Equals(_keys[i]))
            {
                i = (i + 1) % _m;
            }

            // delete key and associated value
            _keys[i] = null;
            _vals[i] = null;

            // rehash all keys in same cluster
            i = (i + 1) % _m;
            while (_keys[i] != null)
            {
                // delete keys[i] an vals[i] and reinsert
                TKey keyToRehash = _keys[i];
                TValue valToRehash = _vals[i];
                _keys[i] = null;
                _vals[i] = null;
                _n--;
                Put(keyToRehash, valToRehash);
                i = (i + 1) % _m;
            }

            _n--;

            // halves size of array if it's 12.5% full or less
            if (_n > 0 && _n <= _m / 8) Resize(_m / 2);

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
            for (var i = 0; i < _m; i++)
                if (_keys[i] != null) queue.Enqueue(_keys[i]);
            return queue;
        }

        /// <summary>
        /// integrity check - don't check after each put() because
        /// integrity not maintained during a delete()
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {

            // check that hash table is at most 50% full
            if (_m < 2 * _n)
            {
                //System.err.println("Hash table size M = " + M + "; array size N = " + N);
                return false;
            }

            // check that each key in table can be found by get()
            for (var i = 0; i < _m; i++)
            {
                if (_keys[i] == null) continue;
                if (Get(_keys[i]) != _vals[i])
                {
                    //System.err.println("get[" + keys[i] + "] = " + get(keys[i]) + "; vals[i] = " + vals[i]);
                    return false;
                }
            }
            return true;
        }

    }
}
