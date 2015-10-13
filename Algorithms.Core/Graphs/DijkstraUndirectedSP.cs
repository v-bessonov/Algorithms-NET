using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core.Sorting;
using Double = Algorithms.Core.Helpers.Double;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>DijkstraUndirectedSP</tt> class represents a data type for solving
    ///  the single-source shortest paths problem in edge-weighted graphs
    ///  where the edge weights are nonnegative.
    ///  <p>
    ///  This implementation uses Dijkstra's algorithm with a binary heap.
    ///  The constructor takes time proportional to <em>E</em> log <em>V</em>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>distTo()</tt> and <tt>hasPathTo()</tt> methods take
    ///  constant time and the <tt>pathTo()</tt> method takes time proportional to the
    ///  number of edges in the shortest path returned.
    ///  </p>
    public class DijkstraUndirectedSP
    {
        private readonly double[] _distTo;          // distTo[v] = distance  of shortest s->v path
        private readonly EdgeW[] _edgeTo;            // edgeTo[v] = last edge on shortest s->v path
        private readonly IndexMinPQ<Double> _pq;    // priority queue of vertices

        /// <summary>
        /// Computes a shortest-paths tree from the source vertex <tt>s</tt> to every
        /// other vertex in the edge-weighted graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the edge-weighted graph</param>
        /// <param name="s">s the source vertex</param>
        /// <exception cref="ArgumentException">if an edge weight is negative</exception>
        /// <exception cref="ArgumentException">unless 0 &lt;= <tt>s</tt> &lt;= <tt>V</tt> - 1</exception>
        public DijkstraUndirectedSP(EdgeWeightedGraph g, int s)
        {
            foreach (var e in g.Edges())
            {
                if (e.Weight < 0)
                    throw new ArgumentException($"edge {e} has negative weight");
            }

            _distTo = new double[g.V];
            _edgeTo = new EdgeW[g.V];
            for (var v = 0; v < g.V; v++)
                _distTo[v] = double.PositiveInfinity;
            _distTo[s] = 0.0;

            // relax vertices in order of distance from s
            _pq = new IndexMinPQ<Double>(g.V);
            _pq.Insert(s, _distTo[s]);
            while (!_pq.IsEmpty())
            {
                var v = _pq.DelMin();
                foreach (var e in g.Adj(v))
                    Relax(e, v);
            }

            // check optimality conditions
            //assert check(G, s);
        }

        /// <summary>
        /// relax edge e and update pq if changed
        /// </summary>
        /// <param name="e"></param>
        /// <param name="v"></param>
        private void Relax(EdgeW e, int v)
        {
            var w = e.Other(v);
            if (_distTo[w] > _distTo[v] + e.Weight)
            {
                _distTo[w] = _distTo[v] + e.Weight;
                _edgeTo[w] = e;
                if (_pq.Contains(w)) _pq.DecreaseKey(w, _distTo[w]);
                else _pq.Insert(w, _distTo[w]);
            }
        }

        /// <summary>
        /// Returns the length of a shortest path between the source vertex <tt>s</tt> and
        /// vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns>the length of a shortest path between the source vertex <tt>s</tt> and the vertex <tt>v</tt>; <tt>Double.POSITIVE_INFINITY</tt> if no such path</returns>
        public double DistTo(int v)
        {
            return _distTo[v];
        }

        /// <summary>
        /// Returns true if there is a path between the source vertex <tt>s</tt> and
        /// vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns><tt>true</tt> if there is a path between the source vertex <tt>s</tt> to vertex <tt>v</tt>; <tt>false</tt> otherwise</returns>
        public bool HasPathTo(int v)
        {
            return _distTo[v] < double.PositiveInfinity;
        }

        /// <summary>
        /// Returns a shortest path between the source vertex <tt>s</tt> and vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns>a shortest path between the source vertex <tt>s</tt> and vertex <tt>v</tt>; <tt>null</tt> if no such path</returns>
        public IEnumerable<EdgeW> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Collections.Stack<EdgeW>();
            var x = v;
            for (var e = _edgeTo[v]; e != null; e = _edgeTo[x])
            {
                path.Push(e);
                x = e.Other(x);
            }
            return path;
        }


        /// <summary>
        /// check optimality conditions:
        /// (i) for all edges e = v-w:            distTo[w] &lt;= distTo[v] + e.weight()
        /// (ii) for all edge e = v-w on the SPT: distTo[w] == distTo[v] + e.weight()
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Check(EdgeWeightedGraph g, int s)
        {

            // check that edge weights are nonnegative
            if (g.Edges().Any(e => e.Weight < 0))
            {
                Console.Error.WriteLine("negative edge weight detected");
                return false;
            }

            // check that distTo[v] and edgeTo[v] are consistent
            if (Math.Abs(_distTo[s]) > 1E12 || _edgeTo[s] != null)
            {
                Console.Error.WriteLine("distTo[s] and edgeTo[s] inconsistent");
                return false;
            }
            for (var v = 0; v < g.V; v++)
            {
                if (v == s) continue;
                if (_edgeTo[v] == null && !double.IsPositiveInfinity(_distTo[v]))
                {
                    Console.Error.WriteLine("distTo[] and edgeTo[] inconsistent");
                    return false;
                }
            }

            // check that all edges e = v-w satisfy distTo[w] <= distTo[v] + e.weight()
            for (var v = 0; v < g.V; v++)
            {
                foreach (var e in g.Adj(v))
                {
                    var w = e.Other(v);
                    if (_distTo[v] + e.Weight < _distTo[w])
                    {
                        Console.Error.WriteLine($"edge {e} not relaxed");
                        return false;
                    }
                }
            }

            // check that all edges e = v-w on SPT satisfy distTo[w] == distTo[v] + e.weight()
            for (var w = 0; w < g.V; w++)
            {
                if (_edgeTo[w] == null) continue;
                var e = _edgeTo[w];
                if (w != e.Either() && w != e.Other(e.Either())) return false;
                int v = e.Other(w);
                if (Math.Abs(_distTo[v] + e.Weight - _distTo[w]) > 1E12)
                {
                    Console.Error.WriteLine($"edge {e} on shortest path not tight");
                    return false;
                }
            }
            return true;
        }
    }
}
