using System;
using System.Text;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// A data type for alphabets, for use with string-processing code
    /// that must convert between an alphabet of size R and the integers
    /// 0 through R-1.
    /// Warning: supports only the basic multilingual plane (BMP), i.e,
    /// Unicode characters between U+0000 and U+FFFF.
    /// </summary>
    public class Alphabet
    {
        /// <summary>
        /// The binary alphabet { 0, 1 }.
        /// </summary>
        public static Alphabet BINARY = new Alphabet("01");

        /// <summary>
        /// The octal alphabet { 0, 1, 2, 3, 4, 5, 6, 7 }.
        /// </summary>
        public static Alphabet OCTAL = new Alphabet("01234567");

        /// <summary>
        /// The decimal alphabet { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.
        /// </summary>
        public static Alphabet DECIMAL = new Alphabet("0123456789");

        /// <summary>
        /// The hexadecimal alphabet { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F }.
        /// </summary>
        public static Alphabet HEXADECIMAL = new Alphabet("0123456789ABCDEF");

        /// <summary>
        /// The DNA alphabet { A, C, T, G }.
        /// </summary>
        public static Alphabet DNA = new Alphabet("ACTG");

        /// <summary>
        /// The lowercase alphabet { a, b, c, ..., z }.
        /// </summary>
        public static Alphabet LOWERCASE = new Alphabet("abcdefghijklmnopqrstuvwxyz");

        /// <summary>
        /// The uppercase alphabet { A, B, C, ..., Z }.
        /// </summary>
        public static Alphabet UPPERCASE = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        /// <summary>
        /// The protein alphabet { A, C, D, E, F, G, H, I, K, L, M, N, P, Q, R, S, T, V, W, Y }.
        /// </summary>
        public static Alphabet PROTEIN = new Alphabet("ACDEFGHIKLMNPQRSTVWY");

        /// <summary>
        /// The base-64 alphabet (64 characters).
        /// </summary>
        public static Alphabet BASE64 = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");

        /// <summary>
        /// The ASCII alphabet (0-127).
        /// </summary>
        public static Alphabet ASCII = new Alphabet(128);

        /// <summary>
        /// The extended ASCII alphabet (0-255).
        /// </summary>
        public static Alphabet EXTENDED_ASCII = new Alphabet(256);

        /// <summary>
        /// The Unicode 16 alphabet (0-65,535).
        /// </summary>
        public static Alphabet UNICODE16 = new Alphabet(65536);


        private readonly char[] _alphabet;     // the characters in the alphabet
        private readonly int[] _inverse;       // indices
        private readonly int _r;               // the radix of the alphabet

        /// <summary>
        /// Initializes a new alphabet from the given set of characters.
        /// </summary>
        /// <param name="alpha">alpha the set of characters</param>
        public Alphabet(string alpha)
        {

            // check that alphabet contains no duplicate chars
            var unicode = new bool[char.MaxValue];
            foreach (var c in alpha)
            {
                if (unicode[c])
                    throw new ArgumentException("Illegal alphabet: repeated character = '" + c + "'");
                unicode[c] = true;
            }

            _alphabet = alpha.ToCharArray();
            _r = alpha.Length;
            _inverse = new int[char.MaxValue];
            for (var i = 0; i < _inverse.Length; i++)
                _inverse[i] = -1;

            // can't use char since R can be as big as 65,536
            for (var c = 0; c < _r; c++)
                _inverse[_alphabet[c]] = c;
        }

        /// <summary>
        /// Initializes a new alphabet using characters 0 through R-1.
        /// </summary>
        /// <param name="r">r the number of characters in the alphabet (the radix)</param>
        private Alphabet(int r)
        {
            _alphabet = new char[r];
            _inverse = new int[r];
            _r = r;

            // can't use char since R can be as big as 65,536
            for (var i = 0; i < r; i++)
                _alphabet[i] = (char)i;
            for (var i = 0; i < r; i++)
                _inverse[i] = i;
        }

        /// <summary>
        /// Initializes a new alphabet using characters 0 through 255.
        /// </summary>
        public Alphabet() : this(256)
        {
        }

        /// <summary>
        /// Returns true if the argument is a character in this alphabet.
        /// </summary>
        /// <param name="c">c the character</param>
        /// <returns><tt>true</tt> if <tt>c</tt> is a character in this alphabet; <tt>false</tt> otherwise</returns>
        public bool Contains(char c)
        {
            return _inverse[c] != -1;
        }

        /// <summary>
        /// Returns the number of characters in this alphabet (the radix).
        /// </summary>
        /// <returns>the number of characters in this alphabet</returns>
        public int R()
        {
            return _r;
        }

        /// <summary>
        /// Returns the binary logarithm of the number of characters in this alphabet.
        /// </summary>
        /// <returns>the binary logarithm (rounded up) of the number of characters in this alphabet</returns>
        public int LgR()
        {
            var lgR = 0;
            for (var t = _r - 1; t >= 1; t /= 2)
                lgR++;
            return lgR;
        }

        /// <summary>
        /// Returns the index corresponding to the argument character.
        /// </summary>
        /// <param name="c">c the character</param>
        /// <returns>the index corresponding to the character <tt>c</tt></returns>
        /// <exception cref="ArgumentException">unless <tt>c</tt> is a character in this alphabet</exception>
        public int ToIndex(char c)
        {
            if (c >= _inverse.Length || _inverse[c] == -1)
            {
                throw new ArgumentException("Character " + c + " not in alphabet");
            }
            return _inverse[c];
        }

        /// <summary>
        /// Returns the indices corresponding to the argument characters.
        /// </summary>
        /// <param name="s">s the characters</param>
        /// <returns>the indices corresponding to the characters <tt>s</tt></returns>
        /// <exception cref="ArgumentException">unless every character in <tt>s</tt> is a character in this alphabet</exception>
        public int[] ToIndices(string s)
        {
            var source = s.ToCharArray();
            var target = new int[s.Length];
            for (var i = 0; i < source.Length; i++)
                target[i] = ToIndex(source[i]);
            return target;
        }

        /// <summary>
        /// Returns the character corresponding to the argument index.
        /// </summary>
        /// <param name="index">index the index</param>
        /// <returns>the character corresponding to the index <tt>index</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless <tt>index</tt> is between <tt>0</tt> and <tt>R - 1</tt></exception>
        public char ToChar(int index)
        {
            if (index < 0 || index >= _r)
            {
                throw new IndexOutOfRangeException("Alphabet index out of bounds");
            }
            return _alphabet[index];
        }

        /// <summary>
        /// Returns the characters corresponding to the argument indices.
        /// </summary>
        /// <param name="indices">indices the indices</param>
        /// <returns>the characters corresponding to the indices <tt>indices</tt></returns>
        public string ToChars(int[] indices)
        {
            var s = new StringBuilder(indices.Length);
            foreach (var t in indices)
                s.Append(ToChar(t));
            return s.ToString();
        }


    }
}
