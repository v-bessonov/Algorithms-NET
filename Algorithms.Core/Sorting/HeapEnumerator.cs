using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    public class HeapEnumerator<T> : IEnumerator<T> where T : class
    {

        // create a new pq
        private readonly MinPQ<T> _copy;

        // add all items to copy of heap
        // takes linear time since already in heap order so no keys move
        public HeapEnumerator(IComparer<T> comparator, int size, int n, IList<T> pq)
        {
            _copy = comparator == null ? new MinPQ<T>(size) : new MinPQ<T>(size, comparator);
            for (var i = 1; i <= n; i++)
                _copy.Insert(pq[i]);
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
            Current = _copy.DelMin();
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public T Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
