using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>TrieST</tt> class represents an symbol table of key-value
    /// pairs, with string keys and generic values.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides character-based methods for finding the string
    /// in the symbol table that is the <em>longest prefix</em> of a given prefix,
    /// finding all strings in the symbol table that <em>start with</em> a given prefix,
    /// and finding all strings in the symbol table that <em>match</em> a given pattern.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&amp;mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p/>
    /// This implementation uses a 256-way trie.
    /// The <em>put</em>, <em>contains</em>, <em>delete</em>, and
    /// <em>longest prefix</em> operations take time proportional to the length
    /// of the key (in the worst case). Construction takes constant time.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// <p/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class TrieST<TValue> where TValue : class
    {
        private const int R = 256; // extended ASCII
        private Node<TValue> _root;      // root of trie
        private int _n;          // number of keys in trie





        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        public TValue Get(string key)
        {
            var x = Get(_root, key, 0);
            return x?.Value;
        }


        /// <summary>
        /// Does this symbol table contain the given key?
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this symbol table contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        public bool Contains(string key)
        {
            return Get(key) != null;
        }

        private Node<TValue> Get(Node<TValue> x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length) return x;
            var c = key[d];
            return Get(x.Next[c], key, d + 1);
        }

        /// <summary>
        /// Inserts the key-value pair into the symbol table, overwriting the old value
        /// with the new value if the key is already in the symbol table.
        /// If the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <param name="val">val the value</param>
        public void Put(string key, TValue val)
        {
            if (val == null) Delete(key);
            else _root = Put(_root, key, val, 0);
        }

        private Node<TValue> Put(Node<TValue> x, string key, TValue val, int d)
        {
            if (x == null) x = new Node<TValue>(R);
            if (d == key.Length)
            {
                if (x.Value == null) _n++;
                x.Value = val;
                return x;
            }
            var c = key[d];
            x.Next[c] = Put(x.Next[c], key, val, d + 1);
            return x;
        }

        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns>the number of key-value pairs in this symbol table</returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Is this symbol table empty?
        /// </summary>
        /// <returns><tt>true</tt> if this symbol table is empty and <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// Returns all keys in the symbol table as an <tt>Iterable</tt>.
        /// To iterate over all of the keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation: <tt>for (Key key : st.keys())</tt>.
        /// </summary>
        /// <returns>all keys in the sybol table as an <tt>Iterable</tt></returns>
        public IEnumerable<string> Keys()
        {
            return KeysWithPrefix(string.Empty);
        }

        /// <summary>
        /// Returns all of the keys in the set that start with <tt>prefix</tt>.
        /// </summary>
        /// <param name="prefix">prefix the prefix</param>
        /// <returns>all of the keys in the set that start with <tt>prefix</tt>, as an iterable</returns>
        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            var results = new Collections.Queue<string>();
            var x = Get(_root, prefix, 0);
            Collect(x, new StringBuilder(prefix), results);
            return results;
        }

        private void Collect(Node<TValue> x, StringBuilder prefix, Collections.Queue<string> results)
        {
            if (x == null) return;
            if (x.Value != null) results.Enqueue(prefix.ToString());
            for (var c = 0; c < R; c++)
            {
                var ch = (char)c;
                prefix.Append(ch);
                Collect(x.Next[c], prefix, results);
                prefix.Remove(prefix.Length - 1, 1);
            }
        }

        /// <summary>
        /// Returns all of the keys in the symbol table that match <tt>pattern</tt>,
        /// where . symbol is treated as a wildcard character.
        /// </summary>
        /// <param name="pattern">pattern the pattern</param>
        /// <returns>all of the keys in the symbol table that match <tt>pattern</tt>, as an iterable, where . is treated as a wildcard character.</returns>
        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            Collections.Queue<string> results = new Collections.Queue<string>();
            Collect(_root, new StringBuilder(), pattern, results);
            return results;
        }

        private void Collect(Node<TValue> x, StringBuilder prefix, string pattern, Collections.Queue<string> results)
        {
            if (x == null) return;
            var d = prefix.Length;
            if (d == pattern.Length && x.Value != null)
                results.Enqueue(prefix.ToString());
            if (d == pattern.Length)
                return;
            var c = pattern[d];
            if (c == '.')
            {
                for (var ch = 0; ch < R; ch++)
                {
                    var chr = (char)ch;

                    prefix.Append(chr);
                    Collect(x.Next[ch], prefix, pattern, results);
                    prefix.Remove(prefix.Length - 1, 1);
                }
            }
            else
            {
                prefix.Append(c);
                Collect(x.Next[c], prefix, pattern, results);
                prefix.Remove(prefix.Length - 1, 1);
            }
        }

        /// <summary>
        /// Returns the string in the symbol table that is the longest prefix of <tt>query</tt>,
        /// or <tt>null</tt>, if no such string.
        /// </summary>
        /// <param name="query">query the query string</param>
        /// <returns>the string in the symbol table that is the longest prefix of <tt>query</tt>, or <tt>null</tt> if no such string</returns>
        public string LongestPrefixOf(string query)
        {
            var length = LongestPrefixOf(_root, query, 0, -1);
            return length == -1 ? null : query.Substring(0, length);
        }

        // returns the length of the longest string key in the subtrie
        // rooted at x that is a prefix of the query string,
        // assuming the first d character match and we have already
        // found a prefix match of given length (-1 if no such match)
        private int LongestPrefixOf(Node<TValue> x, string query, int d, int length)
        {
            if (x == null) return length;
            if (x.Value != null) length = d;
            if (d == query.Length) return length;
            var c = query[d];
            return LongestPrefixOf(x.Next[c], query, d + 1, length);
        }

        /// <summary>
        /// Removes the key from the set if the key is present.
        /// </summary>
        /// <param name="key">key the key</param>
        public void Delete(string key)
        {
            _root = Delete(_root, key, 0);
        }

        private Node<TValue> Delete(Node<TValue> x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length)
            {
                if (x.Value != null) _n--;
                x.Value = null;
            }
            else
            {
                var c = key[d];
                x.Next[c] = Delete(x.Next[c], key, d + 1);
            }

            // remove subtrie rooted at x if it is completely empty
            if (x.Value != null) return x;
            for (var c = 0; c < R; c++)
                if (x.Next[c] != null)
                    return x;
            return null;
        }


    }
}
