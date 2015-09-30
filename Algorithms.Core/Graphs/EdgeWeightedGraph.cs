using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Core.Collections;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EdgeWeightedGraph</tt> class represents an edge-weighted
    /// graph of vertices named 0 through <em>V</em> - 1, where each
    /// undirected edge is of type {@link Edge} and has a real-valued weight.
    /// It supports the following two primary operations: add an edge to the graph,
    /// iterate over all of the edges incident to a vertex. It also provides
    /// methods for returning the number of vertices <em>V</em> and the number
    /// of edges <em>E</em>. Parallel edges and self-loops are permitted.
    /// <p>
    /// This implementation uses an adjacency-lists representation, which 
    /// is a vertex-indexed array of @link{Bag} objects.
    /// All operations take constant time (in the worst case) except
    /// iterating over the edges incident to a given vertex, which takes
    /// time proportional to the number of such edges.
    /// </p>
    /// </summary>
    public class EdgeWeightedGraph
    {
        private static readonly string Newline = Environment.NewLine;

        /// <summary>
        /// Returns the number of vertices in this edge-weighted graph.
        /// </summary>
        public int V { get; }
        /// <summary>
        /// Returns the number of edges in this edge-weighted graph.
        /// </summary>
        public int E { get; private set; }
        private readonly Bag<EdgeW>[] _adj;

        /// <summary>
        /// Initializes an empty edge-weighted graph with <tt>V</tt> vertices and 0 edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> &lt; 0</exception>
        public EdgeWeightedGraph(int v)
        {
            if (v < 0) throw new ArgumentException("Number of vertices must be nonnegative");
            V = v;
            E = 0;
            _adj = new Bag<EdgeW>[v];
            for (var vi = 0; vi < v; vi++)
            {
                _adj[vi] = new Bag<EdgeW>();
            }
        }

        /// <summary>
        /// Initializes a random edge-weighted graph with <tt>V</tt> vertices and <em>E</em> edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> &lt; 0</exception>
        /// <exception cref="ArgumentException">if <tt>E</tt> &lt; 0</exception>
        public EdgeWeightedGraph(int v, int e) : this(v)
        {
            if (e < 0) throw new ArgumentException("Number of edges must be nonnegative");
            for (var i = 0; i < e; i++)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var weight = Math.Round(100 * StdRandom.Uniform()) / 100.0;
                var edge = new EdgeW(ve, we, weight);
                AddEdge(edge);
            }
        }

        /// <summary>
        /// Initializes an edge-weighted graph from an input stream.
        /// The format is the number of vertices <em>V</em>,
        /// followed by the number of edges <em>E</em>,
        /// followed by <em>E</em> pairs of vertices and edge weights,
        /// with each entry separated by whitespace.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <param name="edges"></param>
        public EdgeWeightedGraph(int v, int e, IEnumerable<EdgeW> edges) : this(v)
        {
            if (e < 0) throw new ArgumentException("Number of edges must be nonnegative");
            var edgeWs = edges as IList<EdgeW> ?? edges.ToList();
            foreach (var edge in edgeWs)
            {
                AddEdge(edge);
            }
        }

        /// <summary>
        /// Initializes a new edge-weighted graph that is a deep copy of <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the edge-weighted graph to copy</param>
        public EdgeWeightedGraph(EdgeWeightedGraph g) : this(g.V)
        {
            E = g.E;
            for (var v = 0; v < g.V; v++)
            {
                // reverse so that adjacency list is in same order as original
                var reverse = new Collections.Stack<EdgeW>();
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




        /// <summary>
        /// throw an IndexOutOfBoundsException unless 0 &lt;= v &lt; V
        /// </summary>
        /// <param name="v"></param>
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new IndexOutOfRangeException("vertex " + v + " is not between 0 and " + (V - 1));
        }

        /// <summary>
        /// Adds the undirected edge <tt>e</tt> to this edge-weighted graph.
        /// </summary>
        /// <param name="e">e the edge</param>
        /// <exception cref="IndexOutOfRangeException">unless both endpoints are between 0 and V-1</exception>
        public void AddEdge(EdgeW e)
        {
            var v = e.Either();
            var w = e.Other(v);
            ValidateVertex(v);
            ValidateVertex(w);
            _adj[v].Add(e);
            _adj[w].Add(e);
            E++;
        }

        /// <summary>
        /// Returns the edges incident on vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the edges incident on vertex <tt>v</tt> as an Iterable</returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= v &lt; V</exception>
        public IEnumerable<EdgeW> Adj(int v)
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
        /// Returns all edges in this edge-weighted graph.
        /// To iterate over the edges in this edge-weighted graph, use foreach notation:
        /// </summary>
        /// <returns>all edges in this edge-weighted graph, as an iterable</returns>
        public IEnumerable<EdgeW> Edges()
        {
            var list = new Bag<EdgeW>();
            for (var v = 0; v < V; v++)
            {
                var selfLoops = 0;
                foreach (var e in Adj(v))
                {
                    if (e.Other(v) > v)
                    {
                        list.Add(e);
                    }
                    // only add one copy of each self loop (self loops will be consecutive)
                    else if (e.Other(v) == v)
                    {
                        if (selfLoops % 2 == 0) list.Add(e);
                        selfLoops++;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Returns a string representation of the edge-weighted graph.
        /// This method takes time proportional to <em>E</em> + <em>V</em>.
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
