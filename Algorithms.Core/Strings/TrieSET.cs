using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>TrieSET</tt> class represents an ordered set of strings over
    /// the extended ASCII alphabet.
    /// It supports the usual <em>add</em>, <em>contains</em>, and <em>delete</em>
    /// methods. It also provides character-based methods for finding the string
    /// in the set that is the <em>longest prefix</em> of a given prefix,
    /// finding all strings in the set that <em>start with</em> a given prefix,
    /// and finding all strings in the set that <em>match</em> a given pattern.
    /// <p/>
    /// This implementation uses a 256-way trie.
    /// The <em>add</em>, <em>contains</em>, <em>delete</em>, and
    /// <em>longest prefix</em> methods take time proportional to the length
    /// of the key (in the worst case). Construction takes constant time.
    /// <p/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class TrieSET<TValue> : IEnumerable<string> where TValue : class
    {
        private const int R = 256;        // extended ASCII

        private
        Node<TValue> _root;      // root of trie
        private int _n;          // number of keys in trie



        /// <summary>
        /// Does the set contain the given key?
        /// 
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if the set contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        public bool Contains(string key)
        {
            var x = Get(_root, key, 0);
            return x != null && x.IsString;
        }

        private Node<TValue> Get(Node<TValue> x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length) return x;
            var c = key[d];
            return Get(x.Next[c], key, d + 1);
        }


        /// <summary>
        /// Adds the key to the set if it is not already present.
        /// </summary>
        /// <param name="key">key the key to add</param>
        public void Add(string key)
        {
            _root = Add(_root, key, 0);
        }

        private Node<TValue> Add(Node<TValue> x, string key, int d)
        {
            if (x == null) x = new Node<TValue>(R);
            if (d == key.Length)
            {
                if (!x.IsString) _n++;
                x.IsString = true;
            }
            else
            {
                var c = key[d];
                x.Next[c] = Add(x.Next[c], key, d + 1);
            }
            return x;
        }

        /// <summary>
        /// Returns the number of strings in the set.
        /// </summary>
        /// <returns>the number of strings in the set</returns>
        public int Size()
        {
            return _n;
        }

        /// <summary>
        /// Is the set empty?
        /// </summary>
        /// <returns><tt>true</tt> if the set is empty, and <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// Returns all of the keys in the set, as an iterator.
        /// To iterate over all of the keys in a set named <tt>set</tt>, use the
        /// foreach notation: <tt>for (Key key : set)</tt>.
        /// </summary>
        /// <returns>an iterator to all of the keys in the set</returns>
        public IEnumerator<string> Iterator()
        {
            return KeysWithPrefix(string.Empty).GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Iterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        private static void Collect(Node<TValue> x, StringBuilder prefix, Collections.Queue<string> results)
        {
            if (x == null) return;
            if (x.IsString) results.Enqueue(prefix.ToString());
            for (var c = 0; c < R; c++)
            {
                var ch = (char)c;
                prefix.Append(ch);
                Collect(x.Next[c], prefix, results);
                prefix.Remove(prefix.Length - 1, 1);
            }
        }

        /// <summary>
        /// Returns all of the keys in the set that match <tt>pattern</tt>,
        /// where . symbol is treated as a wildcard character.
        /// </summary>
        /// <param name="pattern">pattern the pattern</param>
        /// <returns>all of the keys in the set that match <tt>pattern</tt>, as an iterable, where . is treated as a wildcard character.</returns>
        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            var results = new Collections.Queue<string>();
            var prefix = new StringBuilder();
            Collect(_root, prefix, pattern, results);
            return results;
        }

        private static void Collect(Node<TValue> x, StringBuilder prefix, string pattern, Collections.Queue<string> results)
        {
            if (x == null) return;
            var d = prefix.Length;
            if (d == pattern.Length && x.IsString)
                results.Enqueue(prefix.ToString());
            if (d == pattern.Length)
                return;
            var c = pattern[d];
            if (c == '.')
            {
                for (var ch = 0; ch < R; ch++)
                {
                    var chh = (char)ch;
                    prefix.Append(chh);
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
        /// Returns the string in the set that is the longest prefix of <tt>query</tt>,
        /// or <tt>null</tt>, if no such string.
        /// </summary>
        /// <param name="query">query the query string</param>
        /// <returns>the string in the set that is the longest prefix of <tt>query</tt>, or <tt>null</tt> if no such string</returns>
        public string LongestPrefixOf(string query)
        {
            var length = LongestPrefixOf(_root, query, 0, -1);
            return length == -1 ? null : query.Substring(0, length);
        }

        // returns the length of the longest string key in the subtrie
        // rooted at x that is a prefix of the query string,
        // assuming the first d character match and we have already
        // found a prefix match of length length
        private static int LongestPrefixOf(Node<TValue> x, string query, int d, int length)
        {
            if (x == null) return length;
            if (x.IsString) length = d;
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
                if (x.IsString) _n--;
                x.IsString = false;
            }
            else
            {
                var c = key[d];
                x.Next[c] = Delete(x.Next[c], key, d + 1);
            }

            // remove subtrie rooted at x if it is completely empty
            if (x.IsString) return x;
            for (var c = 0; c < R; c++)
                if (x.Next[c] != null)
                    return x;
            return null;
        }


    }
}
