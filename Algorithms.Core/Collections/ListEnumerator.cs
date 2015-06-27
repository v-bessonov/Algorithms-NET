using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Collections
{
    public class ListEnumerator<T> : IEnumerator<T> where T : class
    {
        private Node<T> _current;
        private readonly Node<T> _first;

        public ListEnumerator(Node<T> first)
        {
            _first = first;
            _current = first;
        }


        public bool HasNext() { return _current != null; }
        public void Remove() { throw new NotImplementedException(); }

        //public T Next()
        //{
        //    if (!HasNext()) throw new Exception();
        //    var item = _current.Item;
        //    Current = _current.Item;
        //    _current = _current.Next;

        //    return item;
        //}
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!HasNext()) return false;
            Current = _current.Item;
            _current = _current.Next;
            return true;
        }

        public void Reset()
        {
            _current = _first;
            Current = _current.Item;
        }

        public T Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
