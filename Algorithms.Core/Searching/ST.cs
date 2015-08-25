using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>ST</tt> class represents an ordered symbol table of generic
    /// key-value pairs.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides ordered methods for finding the <em>minimum</em>,
    /// <em>maximum</em>, <em>floor</em>, and <em>ceiling</em>.
    /// It also provides a <em>keys</em> method for iterating over all of the keys.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p>
    /// This implementation uses a balanced binary search tree. It requires that
    /// the key type implements the <tt>Comparable</tt> interface and calls the
    /// <tt>compareTo()</tt> and method to compare two keys. It does not call either
    /// <tt>equals()</tt> or <tt>hashCode()</tt>.
    /// The <em>put</em>, <em>contains</em>, <em>remove</em>, <em>minimum</em>,
    /// <em>maximum</em>, <em>ceiling</em>, and <em>floor</em> operations each take
    /// logarithmic time in the worst case.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey">the generic type of keys in this symbol table</typeparam>
    /// <typeparam name="TValue">the generic type of values in this symbol table</typeparam>
    public class ST<TKey, TValue> : IEnumerable<TKey> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {

        private readonly RedBlackBST<TKey, TValue> _st;

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        public ST()
        {
            _st = new RedBlackBST<TKey, TValue>();
        }


        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table; <tt>null</tt> if the key is not in the symbol table</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TValue Get(TKey key)
        {
            if (key == null) throw new NullReferenceException("called get() with null key");
            return _st.Get(key);
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
            if (key == null) throw new NullReferenceException("called put() with null key");
            if (val == null) _st.Delete(key);
            else _st.Put(key, val);
        }

        /// <summary>
        /// Removes the key and associated value from this symbol table.
        /// if the key is in this symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            if (key == null) throw new NullReferenceException("called delete() with null key");
            _st.Delete(key);
        }

        /// <summary>
        /// Returns true if this symbol table contain the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this symbol table contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public bool Contains(TKey key)
        {
            if (key == null) throw new NullReferenceException("called contains() with null key");
            return _st.Contains(key);
        }

        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns>the number of key-value pairs in this symbol table</returns>
        public int Size()
        {
            return _st.Size();
        }

        /// <summary>
        /// Returns true if this symbol table is empty.
        /// </summary>
        /// <returns><tt>true</tt> if this symbol table is empty and <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }


        /// <summary>
        /// Returns all keys in this symbol table.
        /// To iterate over all of the keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation: <tt>for (Key key : st.keys())</tt>.
        /// </summary>
        /// <returns>all keys in the symbol table</returns>
        public IEnumerable<TKey> Keys()
        {
            return _st.Keys();
        }

        /// <summary>
        /// Returns all of the keys in this symbol table.
        /// To iterate over all of the keys in a symbol table named <tt>st</tt>, use the
        /// foreach notation.
        /// </summary>
        /// <returns>an iterator to all of the keys in the symbol table</returns>
        [Obsolete("Use Keys instead")]
        public IEnumerator<TKey> Iterator()
        {
            return _st.Keys().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TKey> GetEnumerator()
        {
            return _st.Keys().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns the smallest key in this symbol table.
        /// </summary>
        /// <returns>the smallest key in this symbol table</returns>
        /// <exception cref="InvalidOperationException">if this symbol table is empty</exception>
        public TKey Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("called min() with empty symbol table");
            return _st.Min();
        }

        /// <summary>
        /// Returns the largest key in this symbol table.
        /// </summary>
        /// <returns>the largest key in this symbol table</returns>
        /// <exception cref="InvalidOperationException">if this symbol table is empty</exception>
        public TKey Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("called max() with empty symbol table");
            return _st.Max();
        }

        /// <summary>
        /// Returns the smallest key in this symbol table greater than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the smallest key in this symbol table greater than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Ceiling(TKey key)
        {
            if (key == null) throw new InvalidOperationException("called ceiling() with null key");
            var k = _st.Ceiling(key);
            if (k == null) throw new NullReferenceException("all keys are less than " + key);
            return k;
        }

        /// <summary>
        /// Returns the largest key in this symbol table less than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the largest key in this symbol table less than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Floor(TKey key)
        {
            if (key == null) throw new InvalidOperationException("called floor() with null key");
            var k = _st.Floor(key);
            if (k == null) throw new NullReferenceException("all keys are greater than " + key);
            return k;
        }

    }
}
