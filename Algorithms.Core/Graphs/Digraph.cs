using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Core.Collections;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>Digraph</tt> class represents a directed graph of vertices
    /// named 0 through <em>V</em> - 1.
    /// It supports the following two primary operations: add an edge to the digraph,
    /// iterate over all of the vertices adjacent from a given vertex.
    /// Parallel edges and self-loops are permitted.
    /// <p>
    /// This implementation uses an adjacency-lists representation, which 
    /// is a vertex-indexed array of {@link Bag} objects.
    /// All operations take constant time (in the worst case) except
    /// iterating over the vertices adjacent from a given vertex, which takes
    /// time proportional to the number of such vertices.
    /// </p>
    /// </summary>
    public class Digraph
    {
        private static readonly string Newline = Environment.NewLine;
        /// <summary>
        /// Returns the number of vertices in this digraph.
        /// </summary>
        public int V { get; }
        /// <summary>
        /// Returns the number of edges in this digraph.
        /// </summary>
        public int E { get; private set; }
        private readonly Bag<Integer>[] _adj;    // adj[v] = adjacency list for vertex v
        private readonly int[] _indegree;        // indegree[v] = indegree of vertex v

        /// <summary>
        /// Initializes an empty digraph with <em>V</em> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <exception cref="ArgumentException">if V &lt; 0</exception>
        public Digraph(int v)
        {
            if (v < 0) throw new ArgumentException("Number of vertices in a Digraph must be nonnegative");
            V = v;
            E = 0;
            _indegree = new int[v];
            _adj = new Bag<Integer>[v];
            for (var i = 0; i < v; i++)
            {
                _adj[i] = new Bag<Integer>();
            }
        }

        /// <summary>
        /// Initializes a digraph from the specified input stream.
        /// The format is the number of vertices <em>V</em>,
        /// followed by the number of edges <em>E</em>,
        /// followed by <em>E</em> pairs of vertices, with each entry separated by whitespace.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <param name="edges"></param>
        /// <exception cref="ArgumentException">if the number of vertices or edges is negative</exception>
        /// <exception cref="IndexOutOfRangeException">if the endpoints of any edge are not in prescribed range</exception>
        public Digraph(int v, int e, IEnumerable<EdgeD> edges)
        {
                V = v;
                if (V < 0) throw new ArgumentException("Number of vertices in a Digraph must be nonnegative");
                _indegree = new int[V];
                _adj = new Bag<Integer>[V];
                for (var i = 0; i < V; i++)
                {
                    _adj[i] = new Bag<Integer>();
                }
                if (e < 0) throw new ArgumentException("Number of edges in a Digraph must be nonnegative");
                foreach (var edge in edges)
                {
                    var ve = edge.V;
                    var we = edge.W;
                    AddEdge(ve, we);
                }
        }

        /// <summary>
        /// Initializes a new digraph that is a deep copy of the specified digraph.
        /// </summary>
        /// <param name="g">g the digraph to copy</param>
        public Digraph(Digraph g):this(g.V)
        {
            
            E = g.E;
            for (var v = 0; v < V; v++)
                _indegree[v] = g.Indegree(v);
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
        /// throw an IndexOutOfBoundsException unless 0 &lt;= v &lt; V
        /// </summary>
        /// <param name="v"></param>
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new IndexOutOfRangeException($"vertex {v} is not between 0 and {(V - 1)}");
        }

        /// <summary>
        /// Adds the directed edge v->w to this digraph.
        /// </summary>
        /// <param name="v">v the tail vertex</param>
        /// <param name="w">w the head vertex</param>
        /// <exception cref="IndexOutOfRangeException">unless both 0 &lt;= v &lt; V and 0 &lt;= w &lt; V</exception>
        public void AddEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            _adj[v].Add(w);
            _indegree[w]++;
            E++;
        }

        /// <summary>
        /// Returns the vertices adjacent from vertex <tt>v</tt> in this digraph.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the vertices adjacent from vertex <tt>v</tt> in this digraph, as an iterable</returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public IEnumerable<Integer> Adj(int v)
        {
            ValidateVertex(v);
            return _adj[v];
        }

        /// <summary>
        /// Returns the number of directed edges incident from vertex <tt>v</tt>.
        /// This is known as the <em>outdegree</em> of vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the outdegree of vertex <tt>v</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public int Outdegree(int v)
        {
            ValidateVertex(v);
            return _adj[v].Size();
        }

        /// <summary>
        /// Returns the number of directed edges incident to vertex <tt>v</tt>.
        /// This is known as the <em>indegree</em> of vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the indegree of vertex <tt>v</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public int Indegree(int v)
        {
            ValidateVertex(v);
            return _indegree[v];
        }

        /// <summary>
        /// Returns the reverse of the digraph.
        /// </summary>
        /// <returns>the reverse of the digraph</returns>
        public Digraph Reverse()
        {
            var r = new Digraph(V);
            for (var v = 0; v < V; v++)
            {
                foreach (int w in Adj(v))
                {
                    r.AddEdge(w, v);
                }
            }
            return r;
        }

        /// <summary>
        /// Returns a string representation of the graph.
        /// </summary>
        /// <returns>the number of vertices <em>V</em>, followed by the number of edges <em>E</em>, followed by the <em>V</em> adjacency lists</returns>
        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"{V} vertices, {E} edges {Newline}");
            for (var v = 0; v < V; v++)
            {
                s.Append($"{v}: ");
                foreach (int w in _adj[v])
                {
                    s.Append($"{w} ");
                }
                s.Append(Newline);
            }
            return s.ToString();
        }

    }
}
