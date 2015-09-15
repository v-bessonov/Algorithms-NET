namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>KMP</tt> class finds the first occurrence of a pattern string
    /// in a text string.
    /// <p>
    /// This implementation uses a version of the Knuth-Morris-Pratt substring search
    /// algorithm. The version takes time as space proportional to
    /// <em>N</em> + <em>M R</em> in the worst case, where <em>N</em> is the length
    /// of the text string, <em>M</em> is the length of the pattern, and <em>R</em>
    /// is the alphabet size.
    /// </p>
    /// </summary>
    public class KMP
    {
        private readonly int _r;       // the radix
        private readonly int[,] _dfa;       // the KMP automoton

        private readonly char[] _pattern;    // either the character array for the pattern
        private readonly string _pat;        // or the pattern string

        /// <summary>
        /// Preprocesses the pattern string.
        /// </summary>
        /// <param name="pat">pat the pattern string</param>
        public KMP(string pat)
        {
            _r = 256;
            _pat = pat;

            // build DFA from pattern
            var m = pat.Length;
            _dfa = new int[_r,m];
            _dfa[pat[0],0] = 1;
            for (int x = 0, j = 1; j < m; j++)
            {
                for (var c = 0; c < _r; c++)
                    _dfa[c,j] = _dfa[c,x];     // Copy mismatch cases. 
                _dfa[pat[j],j] = j + 1;   // Set match case. 
                x = _dfa[pat[j],x];     // Update restart state. 
            }
        }

        /// <summary>
        /// Preprocesses the pattern string.
        /// </summary>
        /// <param name="pattern">pattern the pattern string</param>
        /// <param name="r">R the alphabet size</param>
        public KMP(char[] pattern, int r)
        {
            _r = r;
            _pattern = new char[pattern.Length];
            for (var j = 0; j < pattern.Length; j++)
                _pattern[j] = pattern[j];

            // build DFA from pattern
            var m = pattern.Length;
            _dfa = new int[r,m];
            _dfa[pattern[0],0] = 1;
            for (int x = 0, j = 1; j < m; j++)
            {
                for (var c = 0; c < r; c++)
                    _dfa[c,j] = _dfa[c,x];     // Copy mismatch cases. 
                _dfa[pattern[j],j] = j + 1;      // Set match case. 
                x = _dfa[pattern[j],x];        // Update restart state. 
            }
        }

        /// <summary>
        /// Returns the index of the first occurrrence of the pattern string
        /// in the text string.
        /// </summary>
        /// <param name="txt">txt the text string</param>
        /// <returns>the index of the first occurrence of the pattern string in the text string; N if no such match</returns>
        public int Search(string txt)
        {

            // simulate operation of DFA on text
            var m = _pat.Length;
            var n = txt.Length;
            int i, j;
            for (i = 0, j = 0; i < n && j < m; i++)
            {
                j = _dfa[txt[i],j];
            }
            if (j == m) return i - m;    // found
            return n;                    // not found
        }

        /// <summary>
        /// Returns the index of the first occurrrence of the pattern string
        /// in the text string.
        /// </summary>
        /// <param name="text">text the text string</param>
        /// <returns>the index of the first occurrence of the pattern string in the text string; N if no such match</returns>
        public int Search(char[] text)
        {

            // simulate operation of DFA on text
            var m = _pattern.Length;
            var n = text.Length;
            int i, j;
            for (i = 0, j = 0; i < n && j < m; i++)
            {
                j = _dfa[text[i],j];
            }
            if (j == m) return i - m;    // found
            return n;                    // not found
        }
    }
}
