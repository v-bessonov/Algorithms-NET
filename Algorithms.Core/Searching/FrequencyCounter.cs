using System;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>FrequencyCounter</tt> class provides a client for 
    /// reading in a sequence of words and printing a word (exceeding
    /// a given length) that occurs most frequently. It is useful as
    /// a test client for various symbol table implementations.
    /// </summary>
    public class FrequencyCounter
    {
        int _distinct, _words;
        private const int MINLEN = 1;
        readonly ST<string, Integer> _st;
        string _max = string.Empty;

        public FrequencyCounter()
        {
            _st = new ST<string, Integer>();
        }

        public void InsertWord(string key)
        {
            if (key.Length < MINLEN) return;
            _words++;
            if (_st.Contains(key))
            {
                _st.Put(key, _st.Get(key) + 1);
            }
            else
            {
                _st.Put(key, 1);
                _distinct++;
            }

        }

        /// <summary>
        /// find a key with the highest frequency count
        /// </summary>
        public void SetMax()
        {
            _st.Put(_max, 0);
            foreach (var word in _st.Keys())
            {
                if (_st.Get(word) == null)
                {
                    continue;
                }
                if (_st.Get(word) > _st.Get(_max))
                    _max = word;
            }
        }

        public void ShowStats()
        {
            Console.WriteLine(_max + " " + _st.Get(_max).Value);
            Console.WriteLine("distinct = " + _distinct);
            Console.WriteLine("words    = " + _words);

        }
    }
}
