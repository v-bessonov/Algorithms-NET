using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>AdjMatrixEdgeWeightedDigraph</tt> class represents a edge-weighted
    /// digraph of vertices named 0 through <em>V</em> - 1, where each
    /// directed edge is of type {@link DirectedEdge} and has a real-valued weight.
    /// It supports the following two primary operations: add a directed edge
    /// to the digraph and iterate over all of edges incident from a given vertex.
    /// It also provides
    /// methods for returning the number of vertices <em>V</em> and the number
    /// of edges <em>E</em>. Parallel edges are disallowed; self-loops are permitted.
    /// <p/>
    /// This implementation uses an adjacency-matrix representation.
    /// All operations take constant time (in the worst case) except
    /// iterating over the edges incident from a given vertex, which takes
    /// time proportional to <em>V</em>.
    /// <p/> 
    /// </summary>
    public class AdjMatrixEdgeWeightedDigraph
    {
        private static readonly string Newline = Environment.NewLine;

        /// <summary>
        /// Returns the number of vertices in the edge-weighted digraph.
        /// </summary>
        public int V { get; }
        /// <summary>
        /// Returns the number of edges in the edge-weighted digraph.
        /// </summary>
        public int E { get; private set; }
        private readonly DirectedEdge[][] _adj;



        /// <summary>
        /// Initializes an empty edge-weighted digraph with <tt>V</tt> vertices and 0 edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> &lt; 0</exception>
        public AdjMatrixEdgeWeightedDigraph(int v)
        {
            if (v < 0) throw new ArgumentException("Number of vertices must be nonnegative");
            V = v;
            E = 0;
            _adj = new DirectedEdge[v][];
            for (var i = 0; i < v; i++)
            {
                _adj[i] = new DirectedEdge[v];
            }
        }

        /// <summary>
        /// Initializes a random edge-weighted digraph with <tt>V</tt> vertices and <em>E</em> edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <exception cref="ArgumentException">if <tt>V</tt> &lt; 0</exception>
        /// <exception cref="ArgumentException">if <tt>E</tt> &lt; 0</exception>
        public AdjMatrixEdgeWeightedDigraph(int v, int e) : this(v)
        {
            if (e < 0) throw new ArgumentException("Number of edges must be nonnegative");
            if (e > v * v) throw new ArgumentException("Too many edges");

            // can be inefficient
            while (E != e)
            {
                var vv = StdRandom.Uniform(v);
                var w = StdRandom.Uniform(v);
                var weight = Math.Round(100 * StdRandom.Uniform()) / 100.0;
                AddEdge(new DirectedEdge(vv, w, weight));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <param name="edges"></param>
        public AdjMatrixEdgeWeightedDigraph(int v, int e, IEnumerable<DirectedEdge> edges) : this(v)
        {
            if (e < 0) throw new ArgumentException("Number of edges must be nonnegative");

            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }


        /// <summary>
        /// Adds the directed edge <tt>e</tt> to the edge-weighted digraph (if there
        /// is not already an edge with the same endpoints).
        /// </summary>
        /// <param name="e">e the edge</param>
        public void AddEdge(DirectedEdge e)
        {
            var v = e.From();
            var w = e.To();
            if (_adj[v][w] != null) return;
            E++;
            _adj[v][w] = e;
        }

        /// <summary>
        /// Returns the directed edges incident from vertex <tt>v</tt>.
        /// </summary>
        /// <param name="vv"></param>
        /// <param name="v">v the vertex</param>
        /// <param name="adj"></param>
        /// <returns>the directed edges incident from vertex <tt>v</tt> as an Iterable</returns>
        public IEnumerable<DirectedEdge> Adj(int v)
        {
            return new AdjEnumerator(V, v, _adj);
        }



        /// <summary>
        /// Returns a string representation of the edge-weighted digraph. This method takes
        /// time proportional to <em>V</em><sup>2</sup>.
        /// </summary>
        /// <returns>the number of vertices <em>V</em>, followed by the number of edges <em>E</em>, followed by the <em>V</em> adjacency lists of edges</returns>
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendFormat($"{V} {E}{Newline}");
            for (var v = 0; v < V; v++)
            {
                s.AppendFormat($"{v}: ");
                foreach (var e in Adj(v))
                {
                    s.AppendFormat($"{e}  ");
                }
                s.Append(Newline);
            }
            return s.ToString();
        }

    }
}
