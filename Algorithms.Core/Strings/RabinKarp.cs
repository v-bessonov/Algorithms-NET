using System;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>RabinKarp</tt> class finds the first occurrence of a pattern string
    /// in a text string.
    /// <p>
    /// This implementation uses the Rabin-Karp algorithm.
    /// </p>
    /// </summary>
    public class RabinKarp
    {
        private readonly string _pat;      // the pattern  // needed only for Las Vegas
        private readonly long _patHash;    // pattern hash value
        private readonly int _m;           // pattern length
        private readonly long _q;          // a large prime, small enough to avoid long overflow
        private readonly int _r;           // radix
        private readonly long _rm;         // R^(M-1) % Q

        /// <summary>
        /// Preprocesses the pattern string.
        /// </summary>
        /// <param name="pattern">pattern the pattern string</param>
        /// <param name="r">R the alphabet size</param>
        public RabinKarp(char[] pattern, int r)
        {
            throw new InvalidOperationException("Operation not supported yet");
        }

        /// <summary>
        /// Preprocesses the pattern string.
        /// </summary>
        /// <param name="pat">pat the pattern string</param>
        public RabinKarp(string pat)
        {
            _pat = pat;      // save pattern (needed only for Las Vegas)
            _r = 256;
            _m = pat.Length;
            _q = LongRandomPrime();

            // precompute R^(M-1) % Q for use in removing leading digit
            _rm = 1;
            for (var i = 1; i <= _m - 1; i++)
                _rm = (_r * _rm) % _q;
            _patHash = Hash(pat, _m);
        }

        /// <summary>
        /// Compute hash for key[0..M-1]. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private long Hash(string key, int m)
        {
            long h = 0;
            for (var j = 0; j < m; j++)
                h = (_r * h + key[j]) % _q;
            return h;
        }

        /// <summary>
        /// Las Vegas version: does pat[] match txt[i..i-M+1] ?
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool Check(string txt, int i)
        {
            for (var j = 0; j < _m; j++)
                if (_pat[j] != txt[i + j])
                    return false;
            return true;
        }

        /// <summary>
        /// Monte Carlo version: always return true
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool Check(int i)
        {
            return true;
        }

        /// <summary>
        /// Returns the index of the first occurrrence of the pattern string
        /// in the text string.
        /// </summary>
        /// <param name="txt">txt the text string</param>
        /// <returns>the index of the first occurrence of the pattern string in the text string; N if no such match</returns>
        public int Search(string txt)
        {
            var n = txt.Length;
            if (n < _m) return n;
            var txtHash = Hash(txt, _m);

            // check for match at offset 0
            if ((_patHash == txtHash) && Check(txt, 0))
                return 0;

            // check for hash match; if hash match, check for exact match
            for (var i = _m; i < n; i++)
            {
                // Remove leading digit, add trailing digit, check for match. 
                txtHash = (txtHash + _q - _rm * txt[i - _m] % _q) % _q;
                txtHash = (txtHash * _r + txt[i]) % _q;

                // match
                var offset = i - _m + 1;
                if ((_patHash == txtHash) && Check(txt, offset))
                    return offset;
            }

            // no match
            return n;
        }


        /// <summary>
        /// a random 31-bit prime
        /// </summary>
        /// <returns></returns>
        private static long LongRandomPrime()
        {
            return Numerics.GetPrime();
            //return 31;
        }
    }
}
