using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Multiway</tt> class provides a client for reading in several
    /// sorted text files and merging them together into a single sorted
    /// text stream.
    /// This implementation uses a {@link IndexMinPQ} to perform the multiway
    /// merge.
    /// </summary>
    public class Multiway
    {
        private readonly IndexMinPQ<string> _pq;
        private readonly int _n;
        public Multiway(List<List<string>> lists)
        {
            var n = lists.Sum(l => l.Count);
            _n = n;
            _pq = new IndexMinPQ<string>(n);

            foreach (var list in lists)
            {
               InsertWords(list);

            }
            
        }

        private void InsertWords(IEnumerable<string> list)
        {
            foreach (var word in list)
            {
                var indexEmpty = 0;

                for (var i = 0; i < _n; i++)
                {
                    if (!_pq.Contains(i))
                    {
                        indexEmpty = i;
                        break;
                    }
                }

                _pq.Insert(indexEmpty, word);
                if (_pq.Size() == _n)
                {
                    _pq.DelMin();
                }
                
            }
        }

        public void ShowWords()
        {
            // print results 
            foreach (var item in _pq)
            {
                Console.WriteLine(_pq.KeyOf(item));
            }
        }
    }
}
