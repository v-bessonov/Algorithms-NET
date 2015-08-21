using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>TopM</tt> class provides a client that reads a sequence of
    /// transactions from standard input and prints the <em>M</em> largest ones
    /// to standard output. This implementation uses a {@link MinPQ} of size
    /// at most <em>M</em> + 1 to identify the <em>M</em> largest transactions
    /// and a {@link Stack} to output them in the proper order.
    /// </summary>
    public class TopM
    {
        private readonly MinPQ<Transaction> _pq;
        private readonly int _m;
        public TopM(ICollection<string> a)
        {
            _m = a.Count;
            _pq = new MinPQ<Transaction>(_m + 1);
            InsertTransactions(a);
        }

        private void InsertTransactions(IEnumerable<string> list)
        {
            foreach (var line in list)
            {
                var transaction = new Transaction(line);
                _pq.Insert(transaction);
            }

            // remove minimum if M+1 entries on the PQ
            if (_pq.Size() > _m)
                _pq.DelMin();
        }

        public void ShowTransactions()
        {
            var stack = new Stack<Transaction>();
            foreach (var transaction in _pq)
            {
                stack.Push(transaction);
            }

            foreach (var transaction in stack)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
