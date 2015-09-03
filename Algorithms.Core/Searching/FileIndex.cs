using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>FileIndex</tt> class provides a client for indexing a set of files,
    /// specified as command-line arguments. It takes queries from standard input
    /// and prints each file that contains the given query.
    /// </summary>
    public class FileIndex
    {
        readonly ST<string, SET<string>> _st;
        public FileIndex()
        {
            _st = new ST<string, SET<string>>();
        }

        public void CreateIndex(string fileName, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (!_st.Contains(word)) _st.Put(word, new SET<string>());
                var set = _st.Get(word);
                set.Add(fileName);
            }

        }
        public void ShowFilesByQuery(string query)
        {
            if (!_st.Contains(query)) return;
            var set = _st.Get(query);
            foreach (var file in set)
            {
                Console.WriteLine($"  {file}");
            }
        }

    }
}
