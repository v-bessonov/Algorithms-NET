using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>BST</tt> class represents an ordered symbol table of generic
    /// key-value pairs.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides ordered methods for finding the <em>minimum</em>,
    /// <em>maximum</em>, <em>floor</em>, <em>select</em>, and <em>ceiling</em>.
    /// It also provides a <em>keys</em> method for iterating over all of the keys.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&amp;mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p>
    /// This implementation uses a sorted array. It requires that
    /// the key type implements the <tt>Comparable</tt> interface and calls the
    /// <tt>compareTo()</tt> and method to compare two keys. It does not call either
    /// <tt>equals()</tt> or <tt>hashCode()</tt>.
    /// The <em>put</em> and <em>remove</em> operations each take linear time in
    /// the worst case; the <em>contains</em>, <em>ceiling</em>, <em>floor</em>,
    /// and <em>rank</em> operations take logarithmic time; the <em>size</em>,
    /// <em>is-empty</em>, <em>minimum</em>, <em>maximum</em>, and <em>select</em>
    /// operations take constant time. Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class BinarySearchST<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {

        private const int INIT_CAPACITY = 2;
        private TKey[] _keys;
        private TValue[] _vals;
        private int _n;

        /// <summary>
        /// Initializes an empty symbol table.
        /// </summary>
        public BinarySearchST() : this(INIT_CAPACITY)
        {

        }

        /// <summary>
        /// Initializes an empty symbol table with the given initial capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public BinarySearchST(int capacity)
        {
            _keys = new TKey[capacity];
            _vals = new TValue[capacity];
        }

        /// <summary>
        /// resize the underlying arrays
        /// </summary>
        /// <param name="capacity"></param>
        private void Resize(int capacity)
        {
            var tempk = new TKey[capacity];
            var tempv = new TValue[capacity];
            for (var i = 0; i < _n; i++)
            {
                tempk[i] = _keys[i];
                tempv[i] = _vals[i];
            }
            _vals = tempv;
            _keys = tempk;
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
        /// Returns true if this symbol table is empty.
        /// </summary>
        /// <returns><tt>true</tt> if this symbol table is empty; <tt>false</tt> otherwise</returns>
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
            if (IsEmpty()) return null;
            var i = Rank(key);
            if (i < _n && _keys[i].CompareTo(key) == 0) return _vals[i];
            return null;
        }

        /// <summary>
        /// Return the number of keys in the symbol table strictly less than <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the number of keys in the symbol table strictly less than <tt>key</tt></returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public int Rank(TKey key)
        {
            int lo = 0, hi = _n - 1;
            while (lo <= hi)
            {
                var m = lo + (hi - lo) / 2;
                var cmp = key.CompareTo(_keys[m]);
                if (cmp < 0) hi = m - 1;
                else if (cmp > 0) lo = m + 1;
                else return m;
            }
            return lo;
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

            var i = Rank(key);

            // key is already in table
            if (i < _n && _keys[i].CompareTo(key) == 0)
            {
                _vals[i] = val;
                return;
            }

            // insert new key-value pair
            if (_n == _keys.Length) Resize(2 * _keys.Length);

            for (int j = _n; j > i; j--)
            {
                _keys[j] = _keys[j - 1];
                _vals[j] = _vals[j - 1];
            }
            _keys[i] = key;
            _vals[i] = val;
            _n++;

        }

        /// <summary>
        /// Removes the key and associated value from the symbol table
        /// (if the key is in the symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            if (IsEmpty()) return;

            // compute rank
            var i = Rank(key);

            // key not in table
            if (i == _n || _keys[i].CompareTo(key) != 0)
            {
                return;
            }

            for (int j = i; j < _n - 1; j++)
            {
                _keys[j] = _keys[j + 1];
                _vals[j] = _vals[j + 1];
            }

            _n--;
            _keys[_n] = null;  // to avoid loitering
            _vals[_n] = null;

            // resize if 1/4 full
            if (_n > 0 && _n == _keys.Length / 4) Resize(_keys.Length / 2);

        }


        /// <summary>
        /// Removes the smallest key and associated value from the symbol table.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow error");
            Delete(Min());
        }

        /// <summary>
        /// Removes the largest key and associated value from the symbol table.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public void DeleteMax()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow error");
            Delete(Max());
        }

        #region Ordered symbol table methods.


        /// <summary>
        /// Returns the smallest key in the symbol table.
        /// </summary>
        /// <returns>the smallest key in the symbol table</returns>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public TKey Min()
        {
            return IsEmpty() ? null : _keys[0];
        }

        /// <summary>
        /// Returns the largest key in the symbol table.
        /// </summary>
        /// <returns>the largest key in the symbol table</returns>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public TKey Max()
        {
            return IsEmpty() ? null : _keys[_n - 1];
        }

        /// <summary>
        /// Return the kth smallest key in the symbol table.
        /// </summary>
        /// <param name="k">k the order statistic</param>
        /// <returns>the kth smallest key in the symbol table</returns>
        /// <exception cref="ArgumentException">unless <tt>k</tt> is between 0 and <em>N</em> &minus; 1</exception>
        public TKey Select(int k)
        {
            return k < 0 || k >= _n ? null : _keys[k];
        }

        /// <summary>
        /// Returns the largest key in the symbol table less than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the largest key in the symbol table less than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Floor(TKey key)
        {
            int i = Rank(key);
            if (i < _n && key.CompareTo(_keys[i]) == 0) return _keys[i];
            if (i == 0) return null;
            return _keys[i - 1];
        }

        /// <summary>
        /// Returns the smallest key in the symbol table greater than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the smallest key in the symbol table greater than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Ceiling(TKey key)
        {
            int i = Rank(key);
            if (i == _n) return null;
            return _keys[i];
        }

        /// <summary>
        /// Returns the number of keys in the symbol table in the given range.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>the number of keys in the sybol table between <tt>lo</tt> (inclusive) and <tt>hi</tt> (exclusive)</returns>
        /// <exception cref="NullReferenceException">if either <tt>lo</tt> or <tt>hi</tt> is <tt>null</tt></exception>
        public int Size(TKey lo, TKey hi)
        {
            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            return Rank(hi) - Rank(lo);
        }

        /// <summary>
        /// Returns all keys in the symbol table as an <tt>Iterable</tt>.
        /// To iterate over all of the keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation
        /// </summary>
        /// <returns>all keys in the symbol table</returns>
        public IEnumerable<TKey> Keys()
        {
            return Keys(Min(), Max());
        }

        /// <summary>
        /// Returns all keys in the symbol table in the given range,
        /// as an <tt>Iterable</tt>.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>all keys in the sybol table between <tt>lo</tt> (inclusive) and <tt>hi</tt> (exclusive)</returns>
        /// <exception cref="NullReferenceException">if either <tt>lo</tt> or <tt>hi</tt> is <tt>null</tt></exception>
        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            var queue = new Collections.Queue<TKey>();
            // if (lo == null && hi == null) return queue;
            if (lo == null) throw new NullReferenceException("lo is null in keys()");
            if (hi == null) throw new NullReferenceException("hi is null in keys()");
            if (lo.CompareTo(hi) > 0) return queue;
            for (var i = Rank(lo); i < Rank(hi); i++)
                queue.Enqueue(_keys[i]);
            if (Contains(hi)) queue.Enqueue(_keys[Rank(hi)]);
            return queue;
        }

        #endregion

        #region Check internal invariants.

        private bool Check()
        {
            return IsSorted() && RankCheck();
        }

        /// <summary>
        /// are the items in the array in ascending order?
        /// </summary>
        /// <returns></returns>
        private bool IsSorted()
        {
            for (var i = 1; i < Size(); i++)
                if (_keys[i].CompareTo(_keys[i - 1]) < 0) return false;
            return true;
        }

        /// <summary>
        ///  check that rank(select(i)) = i
        /// </summary>
        /// <returns></returns>
        private bool RankCheck()
        {
            for (var i = 0; i < Size(); i++)
                if (i != Rank(Select(i))) return false;
            for (var i = 0; i < Size(); i++)
                if (_keys[i].CompareTo(Select(Rank(_keys[i]))) != 0) return false;
            return true;
        }

        #endregion
    }
}
