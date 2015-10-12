using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core.QuickUnionUF;
using Algorithms.Core.Sorting;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>KruskalMST</tt> class represents a data type for computing a
    ///  <em>minimum spanning tree</em> in an edge-weighted graph.
    ///  The edge weights can be positive, zero, or negative and need not
    ///  be distinct. If the graph is not connected, it computes a <em>minimum
    ///  spanning forest</em>, which is the union of minimum spanning trees
    ///  in each connected component. The <tt>weight()</tt> method returns the 
    ///  weight of a minimum spanning tree and the <tt>edges()</tt> method
    ///  returns its edges.
    ///  <p>
    ///  This implementation uses <em>Krusal's algorithm</em> and the
    ///  union-find data type.
    ///  The constructor takes time proportional to <em>E</em> log <em>E</em>
    ///  and extra space (not including the graph) proportional to <em>V</em>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>weight()</tt> method takes constant time
    ///  and the <tt>edges()</tt> method takes time proportional to <em>V</em>.
    ///  </p>
    public class KruskalMST
    {

        private static double FLOATING_POINT_EPSILON = 1E-12;

        private readonly double _weight;                        // weight of MST
        private readonly Collections.Queue<EdgeW> _mst = new Collections.Queue<EdgeW>();  // edges in MST

        /// <summary>
        /// Compute a minimum spanning tree (or forest) of an edge-weighted graph.
        /// </summary>
        /// <param name="g">g the edge-weighted graph</param>
        public KruskalMST(EdgeWeightedGraph g)
        {
            // more efficient to build heap by passing array of edges
            var pq = new MinPQ<EdgeW>();
            foreach (var e in g.Edges())
            {
                pq.Insert(e);
            }

            // run greedy algorithm
            var uf = new UF(g.V);
            while (!pq.IsEmpty() && _mst.Size() < g.V - 1)
            {
                var e = pq.DelMin();
                var v = e.Either();
                var w = e.Other(v);
                if (!uf.Connected(v, w))
                { // v-w does not create a cycle
                    uf.Union(v, w);  // merge v and w components
                    _mst.Enqueue(e);  // add edge e to mst
                    _weight += e.Weight;
                }
            }

            // check optimality conditions
            //assert check(G);
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

            // check total weight
            var total = Edges().Sum(e => e.Weight);
            if (Math.Abs(total - Weight()) > FLOATING_POINT_EPSILON)
            {
                Console.Error.WriteLine($"Weight of edges does not equal weight(): {total} vs. {Weight()}{Environment.NewLine}");
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
                            Console.Error.WriteLine($"Edge {f}  violates cut optimality conditions");
                            return false;
                        }
                    }
                }

            }

            return true;
        }

    }
}
