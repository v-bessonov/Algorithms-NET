using System;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>LookupCSV</tt> class provides a data-driven client for reading in a
    /// key-value pairs from a file; then, printing the values corresponding to the
    /// keys found on standard input. Both keys and values are strings.
    /// The fields to serve as the key and value are taken as command-line arguments.
    /// </summary>
    public class LookupCSV
    {
        readonly ST<string, string> _st;
        public LookupCSV()
        {
            _st = new ST<string, string>();
        }

        public void InsertItem(string key, string value)
        {
            _st.Put(key, value);

        }

        public void Check(string key)
        {
            Console.WriteLine(_st.Contains(key) ? _st.Get(key) : "Not found");
        }
    }
}
