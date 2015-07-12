using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// Generic min priority queue implementation with a binary heap.
    /// Can be used with a comparator instead of the natural order.
    /// We use a one-based array to simplify parent and child calculations.
    /// Can be optimized by replacing full exchanges with half exchanges
    /// (ala insertion sort).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinPQ<T> : IEnumerable<T> where T : class
    {


        private T[] _pq;                    // store items at indices 1 to N
        private int _n;                       // number of items on priority queue
        private readonly IComparer<T> _comparator;  // optional comparator

        /// <summary>
        /// Initializes an empty priority queue with the given initial capacity.
        /// </summary>
        /// <param name="initCapacity"></param>
        public MinPQ(int initCapacity)
        {
            _pq = new T[initCapacity + 1];
            _n = 0;
        }

        /// <summary>
        /// Initializes an empty priority queue.
        /// </summary>
        public MinPQ()
            : this(1)
        {
        }

        /// <summary>
        /// Initializes an empty priority queue with the given initial capacity,
        /// using the given comparator.
        /// </summary>
        /// <param name="initCapacity"></param>
        /// <param name="comparator"></param>
        public MinPQ(int initCapacity, IComparer<T> comparator)
        {
            _comparator = comparator;
            _pq = new T[initCapacity + 1];
            _n = 0;
        }

        /// <summary>
        /// Initializes an empty priority queue using the given comparator.
        /// </summary>
        /// <param name="comparator"></param>

        public MinPQ(IComparer<T> comparator)
            : this(1, comparator)
        {
        }

        /// <summary>
        /// Initializes a priority queue from the array of keys.
        /// Takes time proportional to the number of keys, using sink-based heap construction.
        /// </summary>
        /// <param name="keys"></param>
        public MinPQ(IList<T> keys)
        {
            _n = keys.Count;
            _pq = new T[keys.Count + 1];
            for (var i = 0; i < _n; i++)
                _pq[i + 1] = keys[i];
            for (var k = _n / 2; k >= 1; k--)
                Sink(k);
        }

        /// <summary>
        /// Is the priority queue empty?
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _n == 0;
        }

        /// <summary>
        /// Returns the number of keys on the priority queue.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Returns a smallest key on the priority queue.
        /// </summary>
        /// <returns></returns>
        public T Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            return _pq[1];
        }

        /// <summary>
        /// helper function to double the size of the heap array
        /// </summary>
        /// <param name="capacity"></param>
        private void Resize(int capacity)
        {
            var temp = new T[capacity];
            for (var i = 1; i <= _n; i++) temp[i] = _pq[i];
            _pq = temp;
        }

        /// <summary>
        /// Adds a new key to the priority queue.
        /// </summary>
        /// <param name="x"></param>
        public void Insert(T x)
        {
            // double size of array if necessary
            if (_n == _pq.Length - 1) Resize(2 * _pq.Length);

            // add x, and percolate it up to maintain heap invariant
            _pq[++_n] = x;
            Swim(_n);
        }

        /// <summary>
        /// Removes and returns a smallest key on the priority queue.
        /// throws InvalidOperationException if the priority queue is empty
        /// </summary>
        /// <returns>a smallest key on the priority queue</returns>
        public T DelMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            Exch(1, _n);
            var min = _pq[_n--];
            Sink(1);
            _pq[_n + 1] = null;         // avoid loitering and help with garbage collection
            if ((_n > 0) && (_n == (_pq.Length - 1) / 4)) Resize(_pq.Length / 2);
            return min;
        }


        #region Helper functions to restore the heap invariant.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
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

        /***********************************************************************
         * Helper functions for compares and swaps.
         **********************************************************************/
        #region Helper functions for compares and swaps.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool Greater(int i, int j)
        {
            return _comparator == null
                ? ((IComparable<T>) _pq[i]).CompareTo(_pq[j]) > 0
                : _comparator.Compare(_pq[i], _pq[j]) > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Exch(int i, int j)
        {
            var swap = _pq[i];
            _pq[i] = _pq[j];
            _pq[j] = swap;
        }

        /// <summary>
        /// is pq[1..N] a min heap?
        /// </summary>
        /// <returns></returns>
        private bool IsMinHeap()
        {
            return IsMinHeap(1);
        }

        /// <summary>
        /// is subtree of pq[1..N] rooted at k a min heap?
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        private bool IsMinHeap(int k)
        {
            if (k > _n) return true;
            int left = 2 * k, right = 2 * k + 1;
            if (left <= _n && Greater(k, left)) return false;
            if (right <= _n && Greater(k, right)) return false;
            return IsMinHeap(left) && IsMinHeap(right);
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
        public IEnumerator<T> GetEnumerator()
        {
            return new HeapEnumerator<T>(_comparator, Size(), _n, _pq);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
