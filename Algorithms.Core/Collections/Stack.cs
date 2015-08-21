using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Collections
{
    /// <summary>
    /// The <tt>Stack</tt> class represents a last-in-first-out (LIFO) stack of generic items.
    /// It supports the usual <em>push</em> and <em>pop</em> operations, along with methods
    /// for peeking at the top item, testing if the stack is empty, and iterating through
    /// the items in LIFO order.
    /// <p>
    /// This implementation uses a singly-linked list with a static nested class for
    /// linked-list nodes. See {@link LinkedStack} for the version from the
    /// textbook that uses a non-static nested class.
    /// The <em>push</em>, <em>pop</em>, <em>peek</em>, <em>size</em>, and <em>is-empty</em>
    /// operations all take constant time in the worst case.
    /// </p>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T> : IEnumerable<T> where T : class
    {

        private int _n;            // size of the stack
        private Node<T> _first;    // top of stack

        /// <summary>
        /// Initializes an empty stack.
        /// </summary>
        public Stack()
        {
            _first = null;
            _n = 0;
        }


        /// <summary>
        /// Returns true if this stack is empty.
        /// </summary>
        /// <returns>true if this stack is empty; false otherwise</returns>
        public bool IsEmpty()
        {
            return _first == null;
        }

        /// <summary>
        /// Returns the number of items in this stack.
        /// </summary>
        /// <returns>the number of items in this stack</returns>
        public int Size()
        {
            return _n;
        }


        /// <summary>
        /// Adds the item to this stack.
        /// </summary>
        /// <param name="item">item the item to add</param>
        public void Push(T item)
        {
            var oldfirst = _first;
            _first = new Node<T>
            {
                Item = item,
                Next = oldfirst
            };
            _n++;
        }

        /// <summary>
        /// Removes and returns the item most recently added to this stack.
        /// </summary>
        /// <returns>the item most recently added</returns>
        /// throws InvalidOperationException if this stack is empty
        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            var item = _first.Item;        // save item to return
            _first = _first.Next;            // delete first node
            _n--;
            return item;                   // return the saved item
        }


        /// <summary>
        /// Returns (but does not remove) the item most recently added to this stack.
        /// </summary>
        /// <returns>the item most recently added to this stack</returns>
        /// throws InvalidOperationException if this stack is empty
        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            return _first.Item;
        }
        /// <summary>
        /// Returns a string representation of this stack.
        /// </summary>
        /// <returns>the sequence of items in this stack in LIFO order, separated by spaces</returns>
        public string ToStringStack()
        {
            var s = new StringBuilder();
            foreach (var item in this)
            {
                s.AppendFormat("{0} ", item);
            }


            return s.ToString();
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
