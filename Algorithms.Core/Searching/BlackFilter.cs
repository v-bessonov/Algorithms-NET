using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>BlackFilter</tt> class provides a client for reading in a <em>blacklist</em>
    /// of words from a file; then, reading in a sequence of words from standard input, 
    /// printing out each word that <em>does not</em> appear in the file. 
    /// It is useful as a test client for various symbol table implementations.
    /// </summary>
    public class BlackFilter
    {
        private readonly SET<string> _filter;
        public BlackFilter(IEnumerable<string> a)
        {
            _filter = new SET<string>(); ;
            InsertWords(a);
        }

        private void InsertWords(IEnumerable<string> list)
        {
            foreach (var line in list)
            {
                _filter.Add(line);
            }

        }
        public void ShowFilter()
        {

            foreach (var item in _filter)
            {
                Console.WriteLine(item);
            }
        }

        public void Filter(string word)
        {
            if (!_filter.Contains(word))
                Console.WriteLine(word);
        }
    }
}
