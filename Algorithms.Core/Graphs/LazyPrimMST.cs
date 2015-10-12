using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core.QuickUnionUF;
using Algorithms.Core.Sorting;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>LazyPrimMST</tt> class represents a data type for computing a
    ///  <em>minimum spanning tree</em> in an edge-weighted graph.
    ///  The edge weights can be positive, zero, or negative and need not
    ///  be distinct. If the graph is not connected, it computes a <em>minimum
    ///  spanning forest</em>, which is the union of minimum spanning trees
    ///  in each connected component. The <tt>weight()</tt> method returns the 
    ///  weight of a minimum spanning tree and the <tt>edges()</tt> method
    ///  returns its edges.
    ///  <p>
    ///  This implementation uses a lazy version of <em>Prim's algorithm</em>
    ///  with a binary heap of edges.
    ///  The constructor takes time proportional to <em>E</em> log <em>E</em>
    ///  and extra space (not including the graph) proportional to <em>E</em>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>weight()</tt> method takes constant time
    ///  and the <tt>edges()</tt> method takes time proportional to <em>V</em>.
    ///  </p>
    public class LazyPrimMST
    {
        private static double FLOATING_POINT_EPSILON = 1E-12;

        private double _weight;       // total weight of MST
        private readonly Collections.Queue<EdgeW> _mst;     // edges in the MST
        private readonly bool[] _marked;    // marked[v] = true if v on tree
        private readonly MinPQ<EdgeW> _pq;      // edges with one endpoint in tree

        /// <summary>
        /// Compute a minimum spanning tree (or forest) of an edge-weighted graph.
        /// </summary>
        /// <param name="g">g the edge-weighted graph</param>
        public LazyPrimMST(EdgeWeightedGraph g)
        {
            _mst = new Collections.Queue<EdgeW>();
            _pq = new MinPQ<EdgeW>();
            _marked = new bool[g.V];
            for (var v = 0; v < g.V; v++)     // run Prim from all vertices to
                if (!_marked[v]) Prim(g, v);     // get a minimum spanning forest

            // check optimality conditions
            //assert check(G);
        }

        /// <summary>
        /// run Prim's algorithm
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        private void Prim(EdgeWeightedGraph g, int s)
        {
            Scan(g, s);
            while (!_pq.IsEmpty())
            {                        // better to stop when mst has V-1 edges
                var e = _pq.DelMin();                      // smallest edge on pq
                int v = e.Either(), w = e.Other(v);        // two endpoints
                //assert marked[v] || marked[w];
                if (_marked[v] && _marked[w]) continue;      // lazy, both v and w already scanned
                _mst.Enqueue(e);                            // add e to MST
                _weight += e.Weight;
                if (!_marked[v]) Scan(g, v);               // v becomes part of tree
                if (!_marked[w]) Scan(g, w);               // w becomes part of tree
            }
        }

        /// <summary>
        /// add all edges e incident to v onto pq if the other endpoint has not yet been scanned
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Scan(EdgeWeightedGraph g, int v)
        {
            //assert !marked[v];
            _marked[v] = true;
            foreach (var e in g.Adj(v))
                if (!_marked[e.Other(v)]) _pq.Insert(e);
        }

        /// <summary>
        /// Returns the edges in a minimum spanning tree (or forest).
        /// </summary>
        /// <returns>the edges in a minimum spanning tree (or forest) as an iterable of edges</returns>
        public IEnumerable<EdgeW> Edges()
        {
            return _mst;
        }

        /// <summary>
        /// Returns the sum of the edge weights in a minimum spanning tree (or forest).
        /// </summary>
        /// <returns>the sum of the edge weights in a minimum spanning tree (or forest)</returns>
        public double Weight()
        {
            return _weight;
        }

        /// <summary>
        /// check optimality conditions (takes time proportional to E V lg* V)
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool Check(EdgeWeightedGraph g)
        {

            // check weight
            var totalWeight = Edges().Sum(e => e.Weight);
            if (Math.Abs(totalWeight - Weight()) > FLOATING_POINT_EPSILON)
            {
                Console.Error.WriteLine($"Weight of edges does not equal weight(): {totalWeight} vs. {Weight()}{Environment.NewLine}");
                return false;
            }

            // check that it is acyclic
            var uf = new UF(g.V);
            foreach (var e in Edges())
            {
                int v = e.Either(), w = e.Other(v);
                if (uf.Connected(v, w))
                {
                    Console.Error.WriteLine("Not a forest");
                    return false;
                }
                uf.Union(v, w);
            }

            // check that it is a spanning forest
            foreach (var e in g.Edges())
            {
                int v = e.Either(), w = e.Other(v);
                if (!uf.Connected(v, w))
                {
                    Console.Error.WriteLine("Not a spanning forest");
                    return false;
                }
            }

            // check that it is a minimal spanning forest (cut optimality conditions)
            foreach (var e in Edges())
            {

                // all edges in MST except e
                uf = new UF(g.V);
                foreach (var f in _mst)
                {
                    int x = f.Either(), y = f.Other(x);
                    if (f != e) uf.Union(x, y);
                }

                // check that e is min weight edge in crossing cut
                foreach (var f in g.Edges())
                {
                    int x = f.Either(), y = f.Other(x);
                    if (!uf.Connected(x, y))
                    {
                        if (f.Weight < e.Weight)
                        {
                            Console.Error.WriteLine($"Edge {f} violates cut optimality conditions");
                            return false;
                        }
                    }
                }

            }

            return true;
        }
    }
}
