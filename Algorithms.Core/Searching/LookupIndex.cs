using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>LookupIndex</tt> class provides a data-driven client for reading in a
    /// key-value pairs from a file; then, printing the values corresponding to the
    /// keys found on standard input. Keys are strings; values are lists of strings.
    /// The separating delimiter is taken as a command-line argument. This client
    /// is sometimes known as an <em>inverted index</em>.
    /// </summary>
    public class LookupIndex
    {
        readonly ST<string, Collections.Queue<string>> _st;
        readonly ST<string, Collections.Queue<string>> _ts;

        public LookupIndex()
        {
            _st = new ST<string, Collections.Queue<string>>();
            _ts = new ST<string, Collections.Queue<string>>();
        }

        public void CreateLookup(string key, IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                if (!_st.Contains(key)) _st.Put(key, new Collections.Queue<string>());
                if (!_ts.Contains(value)) _ts.Put(value, new Collections.Queue<string>());
                _st.Get(key).Enqueue(value);
                _ts.Get(value).Enqueue(key);
            }


        }
        public void Check(string query)
        {

            if (_st.Contains(query))
                foreach (var vals in _st.Get(query))
                    Console.WriteLine($"  {vals}");
            if (_ts.Contains(query))
                foreach (var keys in _ts.Get(query))
                    Console.WriteLine($"  {keys}");
        }

    }
}
