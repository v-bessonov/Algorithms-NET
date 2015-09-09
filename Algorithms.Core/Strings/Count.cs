using System;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// Create an alphabet specified on the command line, read in a 
    /// sequence of characters over that alphabet (ignoring characters
    /// not in the alphabet), computes the frequency of occurrence of
    /// each character, and print out the results.
    /// </summary>
    public class Count
    {
        private readonly Alphabet _alpha;

        private readonly int _r;
        private readonly int[] _count;

        public Count(string alpha)
        {
            _alpha = new Alphabet(alpha);
            _r = _alpha.R();
            _count = new int[_r];
        }


        public void Run(string content)
        {
            var n = content.Length;
            for (var i = 0; i < n; i++)
                if (_alpha.Contains(content[i]))
                    _count[_alpha.ToIndex(content[i])]++;
            for (var c = 0; c < _r; c++)
                Console.WriteLine(_alpha.ToChar(c) + " " + _count[c]);
        }
    }
}
