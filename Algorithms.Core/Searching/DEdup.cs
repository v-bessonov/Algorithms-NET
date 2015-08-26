using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>DeDup</tt> class provides a client for reading in a sequence of
    /// words from standard input and printing each word, removing any duplicates.
    /// It is useful as a test client for various symbol table implementations.
    /// </summary>
    public class DeDup
    {
        private readonly SET<string> _set;
        public DeDup(IEnumerable<string> a)
        {
            _set = new SET<string>(); ;
            InsertWords(a);
        }

        private void InsertWords(IEnumerable<string> list)
        {
            foreach (var line in list)
            {
                _set.Add(line);
            }

        }
        public void ShowWords()
        {

            foreach (var item in _set)
            {
                Console.WriteLine(item);
            }
        }
    }
}
