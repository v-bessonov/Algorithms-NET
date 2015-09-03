using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>SET</tt> class represents an ordered set of comparable keys.
    /// It supports the usual <em>add</em>, <em>contains</em>, and <em>delete</em>
    /// methods. It also provides ordered methods for finding the <em>minimum</em>,
    /// <em>maximum</em>, <em>floor</em>, and <em>ceiling</em> and set methods
    /// for <em>union</em>, <em>intersection</em>, and <em>equality</em>.
    /// <p>
    /// Even though this implementation include the method <tt>equals()</tt>, it
    /// does not support the method <tt>hashCode()</tt> because sets are mutable.
    /// </p>
    /// This implementation uses a balanced binary search tree. It requires that
    /// the key type implements the <tt>Comparable</tt> interface and calls the
    /// <tt>compareTo()</tt> and method to compare two keys. It does not call either
    /// <tt>equals()</tt> or <tt>hashCode()</tt>.
    /// The <em>add</em>, <em>contains</em>, <em>delete</em>, <em>minimum</em>,
    /// <em>maximum</em>, <em>ceiling</em>, and <em>floor</em> methods take
    /// logarithmic time in the worst case.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// <p>
    /// This implementation uses a balanced binary search tree.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey">the generic type of a key in this set</typeparam>
    public class SET<TKey> : IEnumerable<TKey>, IComparable<SET<TKey>> where TKey : class, IComparable<TKey>
    {


        private readonly RedBlackBST<TKey, string> _set;


        /// <summary>
        /// Initializes an empty set.
        /// </summary>
        public SET()
        {
            _set = new RedBlackBST<TKey, string>();
        }


        /// <summary>
        /// Adds the key to this set (if it is not already present).
        /// </summary>
        /// <param name="key">key the key to add</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Add(TKey key)
        {
            if (key == null) throw new NullReferenceException("called add() with a null key");
            _set.Put(key, string.Empty);
        }



        /// <summary>
        /// Returns true if this set contains the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this set contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public bool Contains(TKey key)
        {
            if (key == null) throw new NullReferenceException("called contains() with a null key");
            return _set.Contains(key);
        }

        /// <summary>
        /// Removes the key from this set (if the key is present).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            if (key == null) throw new NullReferenceException("called delete() with a null key");
            _set.Delete(key);
        }

        /// <summary>
        /// Returns the number of keys in this set.
        /// </summary>
        /// <returns>the number of keys in this set</returns>
        public int Size()
        {
            return _set.Size();
        }

        /// <summary>
        /// Returns true if this set is empty.
        /// </summary>
        /// <returns><tt>true</tt> if this set is empty, and <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }



        /// <summary>
        /// Returns all of the keys in this set, as an iterator.
        /// To iterate over all of the keys in a set named <tt>set</tt>, use the
        /// foreach notation
        /// </summary>
        /// <returns>an iterator to all of the keys in this set</returns>
        public IEnumerator<TKey> Iterator()
        {
            return _set.Keys().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TKey> GetEnumerator()
        {
            return _set.Keys().GetEnumerator();
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
        /// Returns the largest key in this set.
        /// </summary>
        /// <returns>the largest key in this set</returns>
        /// <exception cref="InvalidOperationException">if this set is empty</exception>
        public TKey Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("called max() with empty set");
            return _set.Max();
        }

        /// <summary>
        /// Returns the smallest key in this set.
        /// </summary>
        /// <returns>the smallest key in this set</returns>
        /// <exception cref="InvalidOperationException">if this set is empty</exception>
        public TKey Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("called min() with empty set");
            return _set.Min();
        }


        /// <summary>
        /// Returns the smallest key in this set greater than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the smallest key in this set greater than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Ceiling(TKey key)
        {
            if (key == null) throw new NullReferenceException("called ceiling() with a null key");
            var k = _set.Ceiling(key);
            if (k == null) throw new InvalidOperationException("all keys are less than " + key);
            return k;
        }

        /// <summary>
        /// Returns the largest key in this set less than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the largest key in this set table less than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Floor(TKey key)
        {
            if (key == null) throw new NullReferenceException("called floor() with a null key");
            var k = _set.Floor(key);
            if (k == null) throw new InvalidOperationException("all keys are greater than " + key);
            return k;
        }

        /// <summary>
        /// Returns the union of this set and that set.
        /// </summary>
        /// <param name="that">that the other set</param>
        /// <returns>the union of this set and that set</returns>
        /// <exception cref="NullReferenceException">if <tt>that</tt> is <tt>null</tt></exception>
        public SET<TKey> Union(SET<TKey> that)
        {
            if (that == null) throw new NullReferenceException("called union() with a null argument");
            var c = new SET<TKey>();
            foreach (var x in this)
            {
                c.Add(x);
            }
            foreach (var x in that)
            {
                c.Add(x);
            }
            return c;
        }

        /// <summary>
        /// Returns the intersection of this set and that set.
        /// </summary>
        /// <param name="that">that the other set</param>
        /// <returns>the intersection of this set and that set</returns>
        /// <exception cref="NullReferenceException">if <tt>that</tt> is <tt>null</tt></exception>
        public SET<TKey> Intersects(SET<TKey> that)
        {
            if (that == null) throw new NullReferenceException("called intersects() with a null argument");
            var c = new SET<TKey>();
            if (Size() < that.Size())
            {
                foreach (var x in this)
                {
                    if (that.Contains(x)) c.Add(x);
                }
            }
            else
            {
                foreach (var x in that)
                {
                    if (this.Contains(x)) c.Add(x);
                }
            }
            return c;
        }

        /// <summary>
        /// Compares this set to the specified set.
        /// </summary>
        /// <param name="other">other the other set</param>
        /// <returns><tt>true</tt> if this set equals <tt>other</tt>; <tt>false</tt> otherwise</returns>
        public override bool Equals(object other)
        {
            if (other == this) return true;
            if (other == null) return false;
            if (other.GetType() != GetType()) return false;
            var that = (SET<TKey>)other;
            if (Size() != that.Size()) return false;
            try
            {
                foreach (var k in this)
                    if (!that.Contains(k)) return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This operation is not supported because sets are mutable.
        /// </summary>
        /// <returns>does not return a value</returns>
        /// <exception cref="NotSupportedException">if called</exception>
        public override int GetHashCode()
        {
            throw new NotSupportedException("hashCode() is not supported because sets are mutable");
        }

        public int CompareTo(SET<TKey> that)
        {
            var union = Union(that);
            if (_set.Size() == that.Size() && _set.Size() == union.Size() && that.Size() == union.Size())
            {
                return 0;
            }
            if (_set.Size() < that.Size()) return -1;
            return 1;
        }

        /// <summary>
        /// Returns a string representation of this set.
        /// </summary>
        /// <returns>a string representation of this set, with the keys separated by single spaces</returns>
        public override string ToString()
        {
            var s = new StringBuilder();
            foreach (var key in this)
                s.AppendFormat($"{key} ");
            return s.ToString();
        }


       
    }
}
