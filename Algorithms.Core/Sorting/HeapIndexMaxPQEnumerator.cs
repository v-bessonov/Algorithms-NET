using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    public class HeapIndexMaxPQEnumerator<T> : IEnumerator<int> where T : class, IComparable<T>
    {

        // create a new pq
        private readonly IndexMaxPQ<T> _copy;

        // add all items to copy of heap
        // takes linear time since already in heap order so no keys move
        public HeapIndexMaxPQEnumerator(int n, int[] pq, T[] keys)
        {
            _copy = new IndexMaxPQ<T>(pq.Length - 1);
            for (var i = 1; i <= n; i++)
                _copy.Insert(pq[i], keys[pq[i]]);
        }

        public bool HasNext() { return !_copy.IsEmpty(); }
        public void Remove() { throw new NotSupportedException(); }

        //public T Next()
        //{
        //    if (!HasNext()) throw new InvalidOperationException();
        //    return _copy.DelMin();
        //}


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!HasNext()) return false;
            Current = _copy.DelMax();
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public int Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
