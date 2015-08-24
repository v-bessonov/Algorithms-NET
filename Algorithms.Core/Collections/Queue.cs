using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Collections
{
    /// <summary>
    /// The <tt>Queue</tt> class represents a first-in-first-out (FIFO)
    /// queue of generic items.
    /// It supports the usual <em>enqueue</em> and <em>dequeue</em>
    /// operations, along with methods for peeking at the first item,
    /// testing if the queue is empty, and iterating through
    /// the items in FIFO order.
    /// <p>
    /// This implementation uses a singly-linked list with a static nested class for
    /// linked-list nodes. See {@link LinkedQueue} for the version from the
    /// textbook that uses a non-static nested class.
    /// The <em>enqueue</em>, <em>dequeue</em>, <em>peek</em>, <em>size</em>, and <em>is-empty</em>
    /// operations all take constant time in the worst case.
    /// </p>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T> : IEnumerable<T> where T : class
    {

        private int _n;            // number of elements on queue
        private Node<T> _first;    // beginning of queue
        private Node<T> _last;    // end of queue

        /// <summary>
        /// Initializes an empty queue.
        /// </summary>
        public Queue()
        {
            _first = null;
            _last = null;
            _n = 0;
        }


        /// <summary>
        /// Returns true if this queue is empty.
        /// </summary>
        /// <returns><tt>true</tt> if this queue is empty; <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return _first == null;
        }

        /// <summary>
        /// Returns the number of items in this queue.
        /// </summary>
        /// <returns>he number of items in this queue</returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Returns the item least recently added to this queue.
        /// </summary>
        /// <returns>the item least recently added to this queue</returns>
        /// throws InvalidOperationException if this queue is empty
        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            return _first.Item;
        }

        /// <summary>
        /// Adds the item to this queue
        /// </summary>
        /// <param name="item">item the item to add</param>
        public void Enqueue(T item)
        {
            var oldlast = _last;
            _last = new Node<T>
            {
                Item = item,
                Next = null
            };
            if (IsEmpty()) _first = _last;
            else oldlast.Next = _last;
            _n++;
        }

        /// <summary>
        /// Removes and returns the item on this queue that was least recently added.
        /// </summary>
        /// <returns>the item on this queue that was least recently added</returns>
        /// throws InvalidOperationException if this queue is empty
        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue underflow");
            var item = _first.Item;
            _first = _first.Next;
            _n--;
            if (IsEmpty()) _last = null;   // to avoid loitering
            return item;
        }



        /// <summary>
        /// Returns a string representation of this queue.
        /// </summary>
        /// <returns>the sequence of items in FIFO order, separated by spaces</returns>
        public string ToStringQueue()
        {
            var s = new StringBuilder();
            foreach (var item in this)
            {
                s.AppendFormat("{0} ", item);
            }
            return s.ToString();
        }



        /// <summary>
        /// Returns an iterator that iterates over the items in this queue in FIFO order.
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
