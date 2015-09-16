using System;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>BoyerMoore</tt> class finds the first occurrence of a pattern string
    /// in a text string.
    /// <p>
    /// This implementation uses the Boyer-Moore algorithm (with the bad-character
    /// rule, but not the strong good suffix rule).
    /// </p>
    /// </summary>
    public class BoyerMoore
    {
        private readonly int _r;     // the radix
        private readonly int[] _right;     // the bad-character skip array

        private readonly char[] _pattern;  // store the pattern as a character array
        private readonly string _pat;      // or as a string

        /// <summary>
        /// Preprocesses the pattern string.
        /// </summary>
        /// <param name="pat">pat the pattern string</param>
        public BoyerMoore(string pat)
        {
            _r = 256;
            _pat = pat;

            // position of rightmost occurrence of c in the pattern
            _right = new int[_r];
            for (var c = 0; c < _r; c++)
                _right[c] = -1;
            for (var j = 0; j < pat.Length; j++)
                _right[pat[j]] = j;
        }

        /// <summary>
        /// Preprocesses the pattern string.
        /// </summary>
        /// <param name="pattern">pattern the pattern string</param>
        /// <param name="r">r the alphabet size</param>
        public BoyerMoore(char[] pattern, int r)
        {
            _r = r;
            _pattern = new char[pattern.Length];
            for (var j = 0; j < pattern.Length; j++)
                _pattern[j] = pattern[j];

            // position of rightmost occurrence of c in the pattern
            _right = new int[r];
            for (var c = 0; c < r; c++)
                _right[c] = -1;
            for (var j = 0; j < pattern.Length; j++)
                _right[pattern[j]] = j;
        }

        /// <summary>
        /// Returns the index of the first occurrrence of the pattern string
        /// in the text string.
        /// </summary>
        /// <param name="txt">txt the text string</param>
        /// <returns>the index of the first occurrence of the pattern string in the text string; N if no such match</returns>
        public int Search(string txt)
        {
            var m = _pat.Length;
            var n = txt.Length;
            int skip;
            for (var i = 0; i <= n - m; i += skip)
            {
                skip = 0;
                for (var j = m - 1; j >= 0; j--)
                {
                    if (_pat[j] != txt[i + j])
                    {
                        skip = Math.Max(1, j - _right[txt[i + j]]);
                        break;
                    }
                }
                if (skip == 0) return i;    // found
            }
            return n;                       // not found
        }


        /// <summary>
        /// Returns the index of the first occurrrence of the pattern string
        /// in the text string.
        /// </summary>
        /// <param name="text">text the text string</param>
        /// <returns>the index of the first occurrence of the pattern string in the text string; N if no such match</returns>
        public int Search(char[] text)
        {
            var m = _pattern.Length;
            var n = text.Length;
            int skip;
            for (var i = 0; i <= n - m; i += skip)
            {
                skip = 0;
                for (var j = m - 1; j >= 0; j--)
                {
                    if (_pattern[j] != text[i + j])
                    {
                        skip = Math.Max(1, j - _right[text[i + j]]);
                        break;
                    }
                }
                if (skip == 0) return i;    // found
            }
            return n;                       // not found
        }

    }
}
