using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Core.Collections;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EdgeWeightedDigraph</tt> class represents a edge-weighted
    /// digraph of vertices named 0 through <em>V</em> - 1, where each
    /// directed edge is of type {@link DirectedEdge} and has a real-valued weight.
    /// It supports the following two primary operations: add a directed edge
    /// to the digraph and iterate over all of edges incident from a given vertex.
    /// It also provides
    /// methods for returning the number of vertices <em>V</em> and the number
    /// of edges <em>E</em>. Parallel edges and self-loops are permitted.
    /// <p>
    /// This implementation uses an adjacency-lists representation, which 
    /// is a vertex-indexed array of @link{Bag} objects.
    /// All operations take constant time (in the worst case) except
    /// iterating over the edges incident from a given vertex, which takes
    /// time proportional to the number of such edges.
    /// </p>
    /// </summary>
    public class EdgeWeightedDigraph
    {
        private static readonly string Newline = Environment.NewLine;

        /// <summary>
        /// Returns the number of vertices in this edge-weighted digraph.
        /// </summary>
        public int V { get; }

        /// <summary>
        /// Returns the number of edges in this edge-weighted digraph.
        /// </summary>
        public int E { get; private set; }
        private readonly Bag<DirectedEdge>[] _adj;    // adj[v] = adjacency list for vertex v
        private readonly int[] _indegree;             // indegree[v] = indegree of vertex v

        /// <summary>
        /// Initializes an empty edge-weighted digraph with <tt>V</tt> vertices and 0 edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> &lt; 0</exception>
        public EdgeWeightedDigraph(int v)
        {
            if (v < 0) throw new ArgumentException("Number of vertices in a Digraph must be nonnegative");
            V = v;
            E = 0;
            _indegree = new int[v];
            _adj = new Bag<DirectedEdge>[v];
            for (var vi = 0; vi < v; vi++)
                _adj[vi] = new Bag<DirectedEdge>();
        }

        /// <summary>
        /// Initializes a random edge-weighted digraph with <tt>V</tt> vertices and <em>E</em> edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> &lt; 0</exception>
        /// <exception cref="ArgumentException">if <tt>E</tt> &lt; 0</exception>
        public EdgeWeightedDigraph(int v, int e) :this(v)
        {
            if (e < 0) throw new ArgumentException("Number of edges in a Digraph must be nonnegative");
            for (var i = 0; i < e; i++)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var weight = .01 * StdRandom.Uniform(100);
                var edge = new DirectedEdge(ve, we, weight);
                AddEdge(edge);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <param name="edges"></param>
        public EdgeWeightedDigraph(int v, int e, IEnumerable<DirectedEdge> edges) : this(v)
        {
            if (e < 0) throw new ArgumentException("Number of edges must be nonnegative");

            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        /// <summary>
        /// Initializes a new edge-weighted digraph that is a deep copy of <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the edge-weighted digraph to copy</param>
        public EdgeWeightedDigraph(EdgeWeightedDigraph g) :this(g.V)
        {
            E = g.E;
            for (var v = 0; v < g.V; v++)
                _indegree[v] = g.Indegree(v);
            for (var v = 0; v < g.V; v++)
            {
                // reverse so that adjacency list is in same order as original
                var reverse = new Collections.Stack<DirectedEdge>();
                foreach (var e in g._adj[v])
                {
                    reverse.Push(e);
                }
                foreach (var e in reverse)
                {
                    _adj[v].Add(e);
                }
            }
        }


        // throw an IndexOutOfBoundsException unless 0 <= v < V
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new IndexOutOfRangeException("vertex " + v + " is not between 0 and " + (V - 1));
        }

        /// <summary>
        /// Adds the directed edge <tt>e</tt> to this edge-weighted digraph.
        /// </summary>
        /// <param name="e">e the edge</param>
        /// <exception cref="IndexOutOfRangeException">unless endpoints of edge are between 0 and V-1</exception>
        public void AddEdge(DirectedEdge e)
        {
            var v = e.From();
            var w = e.To();
            ValidateVertex(v);
            ValidateVertex(w);
            _adj[v].Add(e);
            E++;
        }


        /// <summary>
        /// Returns the directed edges incident from vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the directed edges incident from vertex <tt>v</tt> as an Iterable</returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public IEnumerable<DirectedEdge> Adj(int v)
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
        /// Returns all directed edges in this edge-weighted digraph.
        /// To iterate over the edges in this edge-weighted digraph, use foreach notation:
        /// </summary>
        /// <returns>all edges in this edge-weighted digraph, as an iterable</returns>
        public IEnumerable<DirectedEdge> Edges()
        {
            var list = new Bag<DirectedEdge>();
            for (var v = 0; v < V; v++)
            {
                foreach (var e in Adj(v))
                {
                    list.Add(e);
                }
            }
            return list;
        }

        /// <summary>
        /// Returns a string representation of this edge-weighted digraph.
        /// </summary>
        /// <returns>the number of vertices <em>V</em>, followed by the number of edges <em>E</em>, followed by the <em>V</em> adjacency lists of edges</returns>
        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"{V} {E}{Newline}");
            for (var v = 0; v < V; v++)
            {
                s.Append($"{v}: ");
                foreach (var e in _adj[v])
                {
                    s.Append($"{e}  ");
                }
                s.Append(Newline);
            }
            return s.ToString();
        }
    }
}
