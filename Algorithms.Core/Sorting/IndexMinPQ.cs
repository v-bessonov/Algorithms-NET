using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>IndexMinPQ</tt> class represents an indexed priority queue of generic keys.
    /// It supports the usual <em>insert</em> and <em>delete-the-minimum</em>
    /// operations, along with <em>delete</em> and <em>change-the-key</em> 
    /// methods. In order to let the client refer to keys on the priority queue,
    /// an integer between 0 and maxN-1 is associated with each key&mdash;the client
    /// uses this integer to specify which key to delete or change.
    /// It also supports methods for peeking at the minimum key,
    /// testing if the priority queue is empty, and iterating through
    /// the keys.
    /// <p>
    /// This implementation uses a binary heap along with an array to associate
    /// keys with integers in the given range.
    /// The <em>insert</em>, <em>delete-the-minimum</em>, <em>delete</em>,
    /// <em>change-key</em>, <em>decrease-key</em>, and <em>increase-key</em>
    /// operations take logarithmic time.
    /// The <em>is-empty</em>, <em>size</em>, <em>min-index</em>, <em>min-key</em>, and <em>key-of</em>
    /// operations take constant time.
    /// Construction takes time proportional to the specified capacity.
    /// </p>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexMinPQ<T> : IEnumerable<int> where T : class, IComparable<T>
    {


        private readonly int _maxN;        // maximum number of elements on PQ
        private int _n;           // number of elements on PQ
        private readonly int[] _pq;        // binary heap using 1-based indexing
        private readonly int[] _qp;        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
        private readonly T[] _keys;      // keys[i] = priority of i

        /// <summary>
        /// Initializes an empty indexed priority queue with indices between <tt>0</tt>
        /// and <tt>maxN - 1</tt>.
        /// </summary>
        /// <param name="maxN">maxN the keys on this priority queue are index from <tt>0</tt> <tt>maxN - 1</tt></param>
        /// throws ArgumentException if <tt>maxN</tt> &lt; <tt>0</tt>
        public IndexMinPQ(int maxN)
        {
            if (maxN < 0) throw new ArgumentException();
            this._maxN = maxN;
            _keys = (T[])new T[maxN + 1];    // make this of length maxN??
            _pq = new int[maxN + 1];
            _qp = new int[maxN + 1];                   // make this of length maxN??
            for (int i = 0; i <= maxN; i++)
                _qp[i] = -1;
        }

        #region Methods
        /// <summary>
        /// Returns true if this priority queue is empty.
        /// </summary>
        /// <returns><tt>true</tt> if this priority queue is empty;</returns>
        /// <tt>false</tt> otherwise
        public bool IsEmpty()
        {
            return _n == 0;
        }

        /// <summary>
        /// Is <tt>i</tt> an index on this priority queue?
        /// </summary>
        /// <param name="i">i an index</param>
        /// <returns><tt>true</tt> if <tt>i</tt> is an index on this priority queue; <tt>false</tt> otherwise</returns>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        public bool Contains(int i)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            return _qp[i] != -1;
        }

        /// <summary>
        /// Returns the number of keys on this priority queue.
        /// </summary>
        /// <returns>the number of keys on this priority queue</returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Associates key with index <tt>i</tt>.
        /// </summary>
        /// <param name="i">i an index</param>
        /// <param name="key">key the key to associate with index <tt>i</tt></param>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        /// throws ArgumentException if there already is an item associated
        /// with index <tt>i</tt>
        public void Insert(int i, T key)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            if (Contains(i)) throw new ArgumentException("index is already in the priority queue");
            _n++;
            _qp[i] = _n;
            _pq[_n] = i;
            _keys[i] = key;
            Swim(_n);
        }

        /// <summary>
        /// Returns an index associated with a minimum key.
        /// </summary>
        /// <returns>an index associated with a minimum key</returns>
        /// throws InvalidOperationException if this priority queue is empty
        public int MinIndex()
        {
            if (_n == 0) throw new InvalidOperationException("Priority queue underflow");
            return _pq[1];
        }

        /// <summary>
        /// Returns a minimum key.
        /// </summary>
        /// <returns>a minimum key</returns>
        /// throws InvalidOperationException if this priority queue is empty
        public T MinKey()
        {
            if (_n == 0) throw new InvalidOperationException("Priority queue underflow");
            return _keys[_pq[1]];
        }

        /// <summary>
        /// Removes a minimum key and returns its associated index.
        /// </summary>
        /// <returns>an index associated with a minimum key</returns>
        /// throws InvalidOperationException if this priority queue is empty
        public int DelMin()
        {
            if (_n == 0) throw new InvalidOperationException("Priority queue underflow");
            var min = _pq[1];
            Exch(1, _n--);
            Sink(1);
            _qp[min] = -1;            // delete
            _keys[_pq[_n + 1]] = null;    // to help with garbage collection
            _pq[_n + 1] = -1;            // not needed
            return min;
        }

        /// <summary>
        /// Returns the key associated with index <tt>i</tt>.
        /// </summary>
        /// <param name="i">i the index of the key to return</param>
        /// <returns>the key associated with index <tt>i</tt></returns>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        /// throws InvalidOperationException no key is associated with index <tt>i</tt>
        public T KeyOf(int i)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new InvalidOperationException("index is not in the priority queue");
            return _keys[i];
        }

        /// <summary>
        /// Change the key associated with index <tt>i</tt> to the specified value.
        /// </summary>
        /// <param name="i">i the index of the key to change</param>
        /// <param name="key">key change the key assocated with index <tt>i</tt> to this key</param>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        [Obsolete("Replaced by changeKey()")]
        public void Change(int i, T key)
        {
            ChangeKey(i, key);
        }

        /// <summary>
        /// Change the key associated with index <tt>i</tt> to the specified value.
        /// </summary>
        /// <param name="i">i the index of the key to change</param>
        /// <param name="key">key change the key assocated with index <tt>i</tt> to this key</param>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        /// throws InvalidOperationException no key is associated with index <tt>i</tt>
        public void ChangeKey(int i, T key)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new InvalidOperationException("index is not in the priority queue");
            _keys[i] = key;
            Swim(_qp[i]);
            Sink(_qp[i]);
        }

        /// <summary>
        /// Decrease the key associated with index <tt>i</tt> to the specified value.
        /// </summary>
        /// <param name="i">i the index of the key to decrease</param>
        /// <param name="key">key decrease the key assocated with index <tt>i</tt> to this key</param>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        /// throws ArgumentException if key &ge; key associated with index <tt>i</tt>
        /// throws InvalidOperationException no key is associated with index <tt>i</tt>
        public void DecreaseKey(int i, T key)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new InvalidOperationException("index is not in the priority queue");
            if (_keys[i].CompareTo(key) <= 0)
                throw new ArgumentException("Calling decreaseKey() with given argument would not strictly decrease the key");
            _keys[i] = key;
            Swim(_qp[i]);
        }

        /// <summary>
        /// Increase the key associated with index <tt>i</tt> to the specified value.
        /// </summary>
        /// <param name="i">i the index of the key to increase</param>
        /// <param name="key">key increase the key assocated with index <tt>i</tt> to this key</param>
        /// throws IndexOutOfRangeException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        /// throws ArgumentException if key &le; key associated with index <tt>i</tt>
        /// throws InvalidOperationException no key is associated with index <tt>i</tt>
        public void IncreaseKey(int i, T key)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new InvalidOperationException("index is not in the priority queue");
            if (_keys[i].CompareTo(key) >= 0)
                throw new ArgumentException("Calling increaseKey() with given argument would not strictly increase the key");
            _keys[i] = key;
            Sink(_qp[i]);
        }

        /// <summary>
        /// Remove the key associated with index <tt>i</tt>.
        /// </summary>
        /// <param name="i">i the index of the key to remove</param>
        /// throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
        /// throws NoSuchElementException no key is associated with index <t>i</tt>
        public void Delete(int i)
        {
            if (i < 0 || i >= _maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new InvalidOperationException("index is not in the priority queue");
            int index = _qp[i];
            Exch(index, _n--);
            Swim(index);
            Sink(index);
            _keys[i] = null;
            _qp[i] = -1;
        }
        #endregion
        #region General helper functions

        private bool Greater(int i, int j)
        {
            return _keys[_pq[i]].CompareTo(_keys[_pq[j]]) > 0;
        }

        private void Exch(int i, int j)
        {
            var swap = _pq[i];
            _pq[i] = _pq[j];
            _pq[j] = swap;
            _qp[_pq[i]] = i;
            _qp[_pq[j]] = j;
        }
        #endregion
        #region Heap helper functions
        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= _n)
            {
                var j = 2 * k;
                if (j < _n && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exch(k, j);
                k = j;
            }
        }
        #endregion

        #region Iterators
        //
        /// <summary>
        /// Returns an iterator that iterates over the keys on the priority queue
        /// in ascending order.
        /// The iterator doesn't implement <tt>remove()</tt> since it's optional.
        /// </summary>
        /// <returns></returns>
        /// 
        /// 
        public IEnumerator<int> GetEnumerator()
        {
            return new HeapIndexMinPQEnumerator<T>(_n, _pq, _keys);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        
    }
}
