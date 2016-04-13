using System;
using System.Collections.Generic;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>FloydWarshall</tt> class represents a data type for solving the
    /// all-pairs shortest paths problem in edge-weighted digraphs with
    /// no negative cycles.
    /// The edge weights can be positive, negative, or zero.
    /// This class finds either a shortest path between every pair of vertices
    /// or a negative cycle.
    /// <p/>
    /// This implementation uses the Floyd-Warshall algorithm.
    /// The constructor takes time proportional to <em>V</em><sup>3</sup> in the
    /// worst case, where <em>V</em> is the number of vertices.
    /// Afterwards, the <tt>dist()</tt>, <tt>hasPath()</tt>, and <tt>hasNegativeCycle()</tt>
    /// methods take constant time; the <tt>path()</tt> and <tt>negativeCycle()</tt>
    /// method takes time proportional to the number of edges returned.
    /// <p/>
    /// </summary>
    public class FloydWarshall
    {
        /// <summary>
        /// Is there a negative cycle?
        /// <tt>true</tt> if there is a negative cycle, and <tt>false</tt> otherwise
        /// </summary>
        public bool HasNegativeCycle { get; }
        private readonly double[][] _distTo;  // distTo[v][w] = length of shortest v->w path
        private readonly DirectedEdge[][] _edgeTo;  // edgeTo[v][w] = last edge on shortest v->w path

        /// <summary>
        /// Computes a shortest paths tree from each vertex to to every other vertex in
        /// the edge-weighted digraph <tt>G</tt>. If no such shortest path exists for
        /// some pair of vertices, it computes a negative cycle.
        /// </summary>
        /// <param name="g">G the edge-weighted digraph</param>
        public FloydWarshall(AdjMatrixEdgeWeightedDigraph g)
        {
            var vv = g.V;
            _distTo = new double[vv][];
            for (var i = 0; i < vv; i++)
            {
                _distTo[i] = new double[vv];
            }
            _edgeTo = new DirectedEdge[vv][];

            for (var i = 0; i < vv; i++)
            {
                _edgeTo[i] = new DirectedEdge[vv];
            }

            // initialize distances to infinity
            for (var v = 0; v < vv; v++)
            {
                for (var w = 0; w < vv; w++)
                {
                    _distTo[v][w] = double.PositiveInfinity;
                }
            }

            // initialize distances using edge-weighted digraph's
            for (var v = 0; v < g.V; v++)
            {
                foreach (var e in g.Adj(v))
                {
                    _distTo[e.From()][e.To()] = e.Weight;
                    _edgeTo[e.From()][e.To()] = e;
                }
                // in case of self-loops
                if (_distTo[v][v] >= 0.0)
                {
                    _distTo[v][v] = 0.0;
                    _edgeTo[v][v] = null;
                }
            }

            // Floyd-Warshall updates
            for (var i = 0; i < vv; i++)
            {
                // compute shortest paths using only 0, 1, ..., i as intermediate vertices
                for (var v = 0; v < vv; v++)
                {
                    if (_edgeTo[v][i] == null) continue;  // optimization
                    for (var w = 0; w < vv; w++)
                    {
                        if (_distTo[v][w] > _distTo[v][i] + _distTo[i][w])
                        {
                            _distTo[v][w] = _distTo[v][i] + _distTo[i][w];
                            _edgeTo[v][w] = _edgeTo[i][w];
                        }
                    }
                    // check for negative cycle
                    if (_distTo[v][v] < 0.0)
                    {
                        HasNegativeCycle = true;
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// Returns a negative cycle, or <tt>null</tt> if there is no such cycle.
        /// </summary>
        /// <returns>a negative cycle as an iterable of edges, or <tt>null</tt> if there is no such cycle</returns>
        public IEnumerable<DirectedEdge> NegativeCycle()
        {
            for (var v = 0; v < _distTo.Length; v++)
            {
                // negative cycle in v's predecessor graph
                if (_distTo[v][v] < 0.0)
                {
                    var spt = new EdgeWeightedDigraph(_edgeTo.Length);
                    for (var w = 0; w < _edgeTo.Length; w++)
                        if (_edgeTo[v][w] != null)
                            spt.AddEdge(_edgeTo[v][w]);
                    var finder = new EdgeWeightedDirectedCycle(spt);
                    return finder.Cycle();
                }
            }
            return null;
        }

        /// <summary>
        /// Is there a path from the vertex <tt>s</tt> to vertex <tt>t</tt>?
        /// </summary>
        /// <param name="s">s the source vertex</param>
        /// <param name="t">t the destination vertex</param>
        /// <returns><tt>true</tt> if there is a path from vertex <tt>s</tt> to vertex <tt>t</tt>, and <tt>false</tt> otherwise</returns>
        public bool HasPath(int s, int t)
        {
            return _distTo[s][t] < double.PositiveInfinity;
        }

        /// <summary>
        /// Returns the length of a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt>.
        /// </summary>
        /// <param name="s">s the source vertex</param>
        /// <param name="t">t the destination vertex</param>
        /// <returns>the length of a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt>; <tt>Double.POSITIVE_INFINITY</tt> if no such path</returns>
        /// <exception cref="NotSupportedException">if there is a negative cost cycle</exception>
        public double Dist(int s, int t)
        {
            if (HasNegativeCycle)
                throw new NotSupportedException("Negative cost cycle exists");
            return _distTo[s][t];
        }

        /// <summary>
        /// Returns a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt>.
        /// </summary>
        /// <param name="s">s the source vertex</param>
        /// <param name="t">t the destination vertex</param>
        /// <returns>a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt> as an iterable of edges, and <tt>null</tt> if no such path</returns>
        /// <exception cref="NotSupportedException">if there is a negative cost cycle</exception>
        public IEnumerable<DirectedEdge> Path(int s, int t)
        {
            if (HasNegativeCycle)
                throw new NotSupportedException("Negative cost cycle exists");
            if (!HasPath(s, t)) return null;
            var path = new Collections.Stack<DirectedEdge>();
            for (var e = _edgeTo[s][t]; e != null; e = _edgeTo[s][e.From()])
            {
                path.Push(e);
            }
            return path;
        }

        // check optimality conditions
        private bool Check(EdgeWeightedDigraph g, int s)
        {

            // no negative cycle
            if (!HasNegativeCycle)
            {
                for (var v = 0; v < g.V; v++)
                {
                    foreach (var e in g.Adj(v))
                    {
                        var w = e.To();
                        for (var i = 0; i < g.V; i++)
                        {
                            if (_distTo[i][w] > _distTo[i][v] + e.Weight)
                            {
                                Console.WriteLine($"edge {e} is eligible");
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
