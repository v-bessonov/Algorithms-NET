using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Core.Collections;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    ///  The <tt>Graph</tt> class represents an undirected graph of vertices
    /// named 0 through <em>V</em> - 1.
    /// It supports the following two primary operations: add an edge to the graph,
    /// iterate over all of the vertices adjacent to a vertex. It also provides
    /// methods for returning the number of vertices <em>V</em> and the number
    /// of edges <em>E</em>. Parallel edges and self-loops are permitted.
    /// <p>
    /// This implementation uses an adjacency-lists representation, which 
    /// is a vertex-indexed array of {@link Bag} objects.
    /// All operations take constant time (in the worst case) except
    /// iterating over the vertices adjacent to a given vertex, which takes
    /// time proportional to the number of such vertices.
    /// </p>
    /// </summary>
    public class Graph
    {
        private readonly string _newline = Environment.NewLine;
        /// <summary>
        /// 
        /// </summary>
        public int V { get; }
        /// <summary>
        /// 
        /// </summary>
        public int E { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private readonly Bag<Integer>[] _adj;

        /// <summary>
        /// Initializes an empty graph with <tt>V</tt> vertices and 0 edges.
        /// </summary>
        /// <param name="v">V number of vertices</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> < 0</exception>
        public Graph(int v)
        {
            if (V < 0) throw new ArgumentException("Number of vertices must be nonnegative");
            V = v;
            E = 0;
            _adj = new Bag<Integer>[V];
            for (var i = 0; i < V; i++)
            {
                _adj[i] = new Bag<Integer>();
            }
        }

        /// <summary>
        /// Initializes a graph from an input stream.
        /// The format is the number of vertices <em>V</em>,
        /// followed by the number of edges <em>E</em>,
        /// followed by <em>E</em> pairs of vertices, with each entry separated by whitespace.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <param name="edges"></param>
        /// <exception cref="ArgumentException">if the number of vertices or edges is negative</exception>
        public Graph(int v, int e, IEnumerable<EdgeU> edges) : this(v)
        {
            edges = edges.ToList();
            if (v < 0  || e < 0) throw new ArgumentException("Number of vertices and edges must be nonnegative");
            if (e != edges.Count()) throw new ArgumentException("Number of edges doesn't match");
            foreach (var edge in edges)
            {
                var ve = edge.V;
                var we = edge.W;
                AddEdge(ve, we);
            }

        }

        /// <summary>
        /// Initializes a new graph that is a deep copy of <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the graph to copy</param>
        public Graph(Graph g) :this(g.V)
        {
            E = g.E;
            for (var v = 0; v < g.V; v++)
            {
                // reverse so that adjacency list is in same order as original
                var reverse = new Collections.Stack<Integer>();
                foreach (int w in g._adj[v])
                {
                    reverse.Push(w);
                }
                foreach (int w in reverse)
                {
                    _adj[v].Add(w);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <exception cref="IndexOutOfRangeException">unless 0 <= v < V</exception>
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new IndexOutOfRangeException("vertex " + v + " is not between 0 and " + (V - 1));
        }

        /// <summary>
        /// Adds the undirected edge v-w to this graph.
        /// </summary>
        /// <param name="v">v one vertex in the edge</param>
        /// <param name="w">w the other vertex in the edge</param>
        /// <exception cref="IndexOutOfRangeException">unless both 0 &lt;= v &lt; V and 0 &lt;= w &lt; V</exception>
        public void AddEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            E++;
            _adj[v].Add(w);
            _adj[w].Add(v);
        }

        /// <summary>
        /// Returns the vertices adjacent to vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the vertices adjacent to vertex <tt>v</tt>, as an iterable</returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public IEnumerable<Integer> Adj(int v)
        {
            ValidateVertex(v);
            return _adj[v];
        }

        /// <summary>
        /// Returns the degree of vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the degree of vertex <tt>v</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public int Degree(int v)
        {
            ValidateVertex(v);
            return _adj[v].Size();
        }

        /// <summary>
        /// Returns a string representation of this graph.
        /// </summary>
        /// <returns>he number of vertices <em>V</em>, followed by the number of edges <em>E</em>, followed by the <em>V</em> adjacency lists</returns>
        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"{V}  vertices, {E} edges {_newline}");
            for (var v = 0; v < V; v++)
            {
                s.Append($"{v}: ");
                foreach (int w in _adj[v])
                {
                    s.Append($"{w} ");
                }
                s.Append(_newline);
            }
            return s.ToString();
        }

    }
}
