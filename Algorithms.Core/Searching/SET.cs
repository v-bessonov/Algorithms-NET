using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Searching
{
    public class SET<TKey> : IEnumerable<TKey> where TKey : class, IComparable<TKey>
    {

        private readonly RedBlackBST<TKey, string> set;


        /**
    * Initializes an empty set.
    */
        public SET()
        {
            set = new RedBlackBST<TKey, string>();
        }


        /**
    * Adds the key to this set (if it is not already present).
    *
    * @param  key the key to add
    * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
    */
        public void Add(TKey key)
        {
            if (key == null) throw new NullReferenceException("called add() with a null key");
            set.Put(key, string.Empty);
        }





        /**
     * Returns true if this set contains the given key.
     *
     * @param  key the key
     * @return <tt>true</tt> if this set contains <tt>key</tt> and
     *         <tt>false</tt> otherwise
     * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
     */
        public bool Contains(TKey key)
        {
            if (key == null) throw new NullReferenceException("called contains() with a null key");
            return set.Contains(key);
        }

        /**
         * Removes the key from this set (if the key is present).
         *
         * @param  key the key
         * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
         */
        public void Delete(TKey key)
        {
            if (key == null) throw new NullReferenceException("called delete() with a null key");
            set.Delete(key);
        }

        /**
         * Returns the number of keys in this set.
         *
         * @return the number of keys in this set
         */
        public int Size()
        {
            return set.Size();
        }

        /**
         * Returns true if this set is empty.
         *
         * @return <tt>true</tt> if this set is empty, and <tt>false</tt> otherwise
         */
        public bool IsEmpty()
        {
            return Size() == 0;
        }






        /**
    * Returns all of the keys in this set, as an iterator.
    * To iterate over all of the keys in a set named <tt>set</tt>, use the
    * foreach notation: <tt>for (Key key : set)</tt>.
    *
    * @return an iterator to all of the keys in this set
    */
        public IEnumerator<TKey> Iterator()
        {
            return set.Keys().GetEnumerator();
        }


        public IEnumerator<TKey> GetEnumerator()
        {
            return set.Keys().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /**
     * Returns the largest key in this set.
     *
     * @return the largest key in this set
     * @throws NoSuchElementException if this set is empty
     */
        public TKey Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("called max() with empty set");
            return set.Max();
        }

        /**
         * Returns the smallest key in this set.
         *
         * @return the smallest key in this set
         * @throws NoSuchElementException if this set is empty
         */
        public TKey Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("called min() with empty set");
            return set.Min();
        }


        /**
         * Returns the smallest key in this set greater than or equal to <tt>key</tt>.
         *
         * @param  key the key
         * @return the smallest key in this set greater than or equal to <tt>key</tt>
         * @throws NoSuchElementException if there is no such key
         * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
         */
        public TKey Ceiling(TKey key)
        {
            if (key == null) throw new NullReferenceException("called ceiling() with a null key");
            var k = set.Ceiling(key);
            if (k == null) throw new InvalidOperationException("all keys are less than " + key);
            return k;
        }

        /**
         * Returns the largest key in this set less than or equal to <tt>key</tt>.
         *
         * @param  key the key
         * @return the largest key in this set table less than or equal to <tt>key</tt>
         * @throws NoSuchElementException if there is no such key
         * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
         */
        public TKey Floor(TKey key)
        {
            if (key == null) throw new NullReferenceException("called floor() with a null key");
            var k = set.Floor(key);
            if (k == null) throw new InvalidOperationException("all keys are greater than " + key);
            return k;
        }

        /**
         * Returns the union of this set and that set.
         *
         * @param  that the other set
         * @return the union of this set and that set
         * @throws NullPointerException if <tt>that</tt> is <tt>null</tt>
         */
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

        /**
         * Returns the intersection of this set and that set.
         *
         * @param  that the other set
         * @return the intersection of this set and that set
         * @throws NullPointerException if <tt>that</tt> is <tt>null</tt>
         */
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


        public override int GetHashCode()
        {
            throw new NotSupportedException("hashCode() is not supported because sets are mutable");
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            foreach (var key in this)
                s.AppendFormat($"{key} ");
            return s.ToString();
        }
    }
}
