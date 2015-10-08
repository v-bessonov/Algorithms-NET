using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;
using Algorithms.Core.Searching;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>SymbolDigraph</tt> class represents a digraph, where the
    /// vertex names are arbitrary strings.
    /// By providing mappings between string vertex names and integers,
    /// it serves as a wrapper around the
    /// {@link Digraph} data type, which assumes the vertex names are integers
    /// between 0 and <em>V</em> - 1.
    /// It also supports initializing a symbol digraph from a file.
    /// <p>
    /// This implementation uses an {@link ST} to map from strings to integers,
    /// an array to map from integers to strings, and a {@link Digraph} to store
    /// the underlying graph.
    /// The <em>index</em> and <em>contains</em> operations take time 
    /// proportional to log <em>V</em>, where <em>V</em> is the number of vertices.
    /// The <em>name</em> operation takes constant time.
    /// </p>
    /// </summary>
    public class SymbolDigraph
    {
        private readonly ST<string, Integer> _st;  // string -> index
        private readonly string[] _keys;           // index  -> string
        /// <summary>
        /// Returns the digraph assoicated with the symbol graph. It is the client's responsibility
        /// not to mutate the digraph.
        /// </summary>
        public Digraph G { get; }

        /// <summary>
        /// Initializes a digraph from a file using the specified delimiter.
        /// Each line in the file contains
        /// the name of a vertex, followed by a list of the names
        /// of the vertices adjacent to that vertex, separated by the delimiter.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="delimiter"></param>
        public SymbolDigraph(IList<string> lines, char delimiter)
        {
            _st = new ST<string, Integer>();

            // First pass builds the index by reading strings to associate
            // distinct strings with an index
            foreach (var line in lines)
            {
                var a = line.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in a)
                {
                    if (!_st.Contains(word))
                        _st.Put(word, _st.Size());
                }
            }

            // inverted index to get string keys in an aray
            _keys = new string[_st.Size()];
            foreach (var name in _st.Keys())
            {
                _keys[_st.Get(name)] = name;
            }

            // second pass builds the digraph by connecting first vertex on each
            // line to all others
            G = new Digraph(_st.Size());

            foreach (var line in lines)
            {
                var a = line.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                int v = _st.Get(a[0]);
                for (var i = 1; i < a.Length; i++)
                {
                    int w = _st.Get(a[i]);
                    G.AddEdge(v, w);
                }
            }
        }

        /// <summary>
        /// Does the digraph contain the vertex named <tt>s</tt>?
        /// </summary>
        /// <param name="s">s the name of a vertex</param>
        /// <returns><tt>true</tt> if <tt>s</tt> is the name of a vertex, and <tt>false</tt> otherwise</returns>
        public bool Contains(string s)
        {
            return _st.Contains(s);
        }

        /// <summary>
        /// Returns the integer associated with the vertex named <tt>s</tt>.
        /// </summary>
        /// <param name="s">s the name of a vertex</param>
        /// <returns>the integer (between 0 and <em>V</em> - 1) associated with the vertex named <tt>s</tt></returns>
        public int Index(string s)
        {
            return _st.Get(s);
        }

        /// <summary>
        /// Returns the name of the vertex associated with the integer <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the integer corresponding to a vertex (between 0 and <em>V</em> - 1)</param>
        /// <returns>the name of the vertex associated with the integer <tt>v</tt></returns>
        public string Name(int v)
        {
            return _keys[v];
        }

    }
}
