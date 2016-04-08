using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>BellmanFordSP</tt> class represents a data type for solving the
    /// single-source shortest paths problem in edge-weighted digraphs with
    /// no negative cycles. 
    /// The edge weights can be positive, negative, or zero.
    /// This class finds either a shortest path from the source vertex <em>s</em>
    /// to every other vertex or a negative cycle reachable from the source vertex.
    /// <p/>
    /// This implementation uses the Bellman-Ford-Moore algorithm.
    /// The constructor takes time proportional to <em>V</em> (<em>V</em> + <em>E</em>)
    /// in the worst case, where <em>V</em> is the number of vertices and <em>E</em>
    /// is the number of edges.
    /// Afterwards, the <tt>distTo()</tt>, <tt>hasPathTo()</tt>, and <tt>hasNegativeCycle()</tt>
    /// methods take constant time; the <tt>pathTo()</tt> and <tt>negativeCycle()</tt>
    /// method takes time proportional to the number of edges returned.
    /// <p/> 
    /// </summary>
    public class BellmanFordSP
    {
        private readonly double[] _distTo;               // distTo[v] = distance  of shortest s->v path
        private readonly DirectedEdge[] _edgeTo;         // edgeTo[v] = last edge on shortest s->v path
        private readonly bool[] _onQueue;             // onQueue[v] = is v currently on the queue?
        private readonly Collections.Queue<Integer> _queue;          // queue of vertices to relax
        private int _cost;                      // number of calls to relax()
        private IEnumerable<DirectedEdge> _cycle;  // negative cycle (or null if no such cycle)

        /// <summary>
        /// Computes a shortest paths tree from <tt>s</tt> to every other vertex in
        /// the edge-weighted digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the acyclic digraph</param>
        /// <param name="s">s the source vertex</param>
        /// <exception cref="ArgumentException">unless 0 le; <tt>s</tt> le; <tt>V</tt> - 1</exception>

        public BellmanFordSP(EdgeWeightedDigraph g, int s)
        {
            _distTo = new double[g.V];
            _edgeTo = new DirectedEdge[g.V];
            _onQueue = new bool[g.V];
            for (var v = 0; v < g.V; v++)
                _distTo[v] = double.PositiveInfinity;
            _distTo[s] = 0.0;

            // Bellman-Ford algorithm
            _queue = new Collections.Queue<Integer>();
            _queue.Enqueue(s);
            _onQueue[s] = true;
            while (!_queue.IsEmpty() && !HasNegativeCycle())
            {
                int v = _queue.Dequeue();
                _onQueue[v] = false;
                Relax(g, v);
            }

        }

        // relax vertex v and put other endpoints on queue if changed
        private void Relax(EdgeWeightedDigraph g, int v)
        {
            foreach (var e in g.Adj(v))
            {
                var w = e.To();
                if (_distTo[w] > _distTo[v] + e.Weight)
                {
                    _distTo[w] = _distTo[v] + e.Weight;
                    _edgeTo[w] = e;
                    if (!_onQueue[w])
                    {
                        _queue.Enqueue(w);
                        _onQueue[w] = true;
                    }
                }
                if (_cost++ % g.V == 0)
                {
                    FindNegativeCycle();
                    if (HasNegativeCycle()) return;  // found a negative cycle
                }
            }
        }


        /// <summary>
        /// Is there a negative cycle reachable from the source vertex <tt>s</tt>?
        /// </summary>
        /// <returns><tt>true</tt> if there is a negative cycle reachable from the source vertex <tt>s</tt>, and <tt>false</tt> otherwise</returns>
        public bool HasNegativeCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// Returns a negative cycle reachable from the source vertex <tt>s</tt>, or <tt>null</tt>
        /// if there is no such cycle.
        /// </summary>
        /// <returns>a negative cycle reachable from the soruce vertex <tt>s</tt> as an iterable of edges, and <tt>null</tt> if there is no such cycle</returns>
        public IEnumerable<DirectedEdge> NegativeCycle()
        {
            return _cycle;
        }

        // by finding a cycle in predecessor graph
        private void FindNegativeCycle()
        {
            var vv = _edgeTo.Length;
            var spt = new EdgeWeightedDigraph(vv);
            for (var v = 0; v < vv; v++)
                if (_edgeTo[v] != null)
                    spt.AddEdge(_edgeTo[v]);

            var finder = new EdgeWeightedDirectedCycle(spt);
            _cycle = finder.Cycle();
        }



        /// <summary>
        /// Returns the length of a shortest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns>the length of a shortest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>; <tt>Double.POSITIVE_INFINITY</tt> if no such path</returns>
        /// <exception cref="NotSupportedException">if there is a negative cost cycle reachable from the source vertex <tt>s</tt></exception>
        public double DistTo(int v)
        {
            if (HasNegativeCycle())
                throw new NotSupportedException("Negative cost cycle exists");
            return _distTo[v];
        }

        /// <summary>
        /// Is there a path from the source <tt>s</tt> to vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns><tt>true</tt> if there is a path from the source vertex <tt>s</tt> to vertex <tt>v</tt>, and <tt>false</tt> otherwise</returns>
        public bool HasPathTo(int v)
        {
            return _distTo[v] < double.PositiveInfinity;
        }

        /// <summary>
        /// Returns a shortest path from the source <tt>s</tt> to vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns>a shortest path from the source <tt>s</tt> to vertex <tt>v</tt> as an iterable of edges, and <tt>null</tt> if no such path</returns>
        /// <exception cref="NotSupportedException">if there is a negative cost cycle reachable from the source vertex <tt>s</tt></exception>
        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            if (HasNegativeCycle())
                throw new NotSupportedException("Negative cost cycle exists");
            if (!HasPathTo(v)) return null;
            var path = new Collections.Stack<DirectedEdge>();
            for (var e = _edgeTo[v]; e != null; e = _edgeTo[e.From()])
            {
                path.Push(e);

            }
            return path;
        }

        // check optimality conditions: either 
        // (i) there exists a negative cycle reacheable from s
        //     or 
        // (ii)  for all edges e = v->w:            distTo[w] <= distTo[v] + e.weight()
        // (ii') for all edges e = v->w on the SPT: distTo[w] == distTo[v] + e.weight()
        private bool Check(EdgeWeightedDigraph g, int s)
        {

            // has a negative cycle
            if (HasNegativeCycle())
            {
                var weight = NegativeCycle().Sum(e => e.Weight);
                if (weight >= 0.0)
                {
                    Console.WriteLine($"error: weight of negative cycle = {weight}");
                    return false;
                }
            }

            // no negative cycle reachable from source
            else
            {

                // check that distTo[v] and edgeTo[v] are consistent
                if (Math.Abs(_distTo[s]) > 0.0001 || _edgeTo[s] != null)
                {
                    Console.WriteLine("distanceTo[s] and edgeTo[s] inconsistent");
                    return false;
                }
                for (var v = 0; v < g.V; v++)
                {
                    if (v == s) continue;
                    if (_edgeTo[v] != null || double.IsPositiveInfinity(_distTo[v])) continue;
                    Console.WriteLine("distTo[] and edgeTo[] inconsistent");
                    return false;
                }

                // check that all edges e = v->w satisfy distTo[w] <= distTo[v] + e.weight()
                for (var v = 0; v < g.V; v++)
                {
                    foreach (var e in g.Adj(v))
                    {
                        var w = e.To();
                        if (!(_distTo[v] + e.Weight < _distTo[w])) continue;
                        Console.WriteLine($"edge {e} not relaxed");
                        return false;
                    }
                }

                // check that all edges e = v->w on SPT satisfy distTo[w] == distTo[v] + e.weight()
                for (var w = 0; w < g.V; w++)
                {
                    if (_edgeTo[w] == null) continue;
                    var e = _edgeTo[w];
                    var v = e.From();
                    if (w != e.To()) return false;
                    if (Math.Abs(_distTo[v] + e.Weight - _distTo[w]) < 0.0001) continue;
                    Console.WriteLine($"edge {e} on shortest path not tight");
                    return false;
                }
            }
            Console.WriteLine("Satisfies optimality conditions");
            Console.WriteLine();
            return true;
        }


    }
}
