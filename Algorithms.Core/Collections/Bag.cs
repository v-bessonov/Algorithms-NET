using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Collections
{
    /// <summary>
    ///  A generic bag or multiset, implemented using a singly-linked list.
    /// The <tt>Bag</tt> class represents a bag (or multiset) of 
    /// generic items. It supports insertion and iterating over the 
    /// items in arbitrary order.
    /// <p>
    /// This implementation uses a singly-linked list with a class Node.
    /// The <em>add</em>, <em>isEmpty</em>, and <em>size</em> operations
    /// take constant time. Iteration takes time proportional to the number of items.
    /// <p>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Bag<T> : IEnumerable<T> where T : class 
    {

        private int _n;            // number of elements in bag
        private Node<T> _first;    // beginning of bag

        /// <summary>
        /// Initializes an empty bag.
        /// </summary>
        public Bag()
        {
            _first = null;
            _n = 0;
        }


        /// <summary>
        /// Is this bag empty?
        /// </summary>
        /// <returns>true if this bag is empty; false otherwise</returns>
        public bool IsEmpty()
        {
            return _first == null;
        }

        /// <summary>
        /// Returns the number of items in this bag.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Adds the item to this bag.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            var oldfirst = _first;
            _first = new Node<T> {Item = item, Next = oldfirst};
            _n++;
        }

        /// <summary>
        /// Returns an iterator that iterates over the items in the bag in arbitrary order.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(_first);  
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
