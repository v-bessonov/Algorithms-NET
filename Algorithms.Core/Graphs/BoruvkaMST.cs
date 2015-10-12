using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core.Collections;
using Algorithms.Core.QuickUnionUF;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>BoruvkaMST</tt> class represents a data type for computing a
    ///  <em>minimum spanning tree</em> in an edge-weighted graph.
    ///  The edge weights can be positive, zero, or negative and need not
    ///  be distinct. If the graph is not connected, it computes a <em>minimum
    ///  spanning forest</em>, which is the union of minimum spanning trees
    ///  in each connected component. The <tt>weight()</tt> method returns the 
    ///  weight of a minimum spanning tree and the <tt>edges()</tt> method
    ///  returns its edges.
    ///  <p>
    ///  This implementation uses <em>Boruvka's algorithm</em> and the union-find
    ///  data type.
    ///  The constructor takes time proportional to <em>E</em> log <em>V</em>
    ///  and extra space (not including the graph) proportional to <em>V</em>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>weight()</tt> method takes constant time
    ///  and the <tt>edges()</tt> method takes time proportional to <em>V</em>.
    ///  </p>
    public class BoruvkaMST
    {
        private static double FLOATING_POINT_EPSILON = 1E-12;

        private readonly Bag<EdgeW> _mst = new Bag<EdgeW>();    // edges in MST
        private readonly double _weight;                      // weight of MST

        /// <summary>
        /// Compute a minimum spanning tree (or forest) of an edge-weighted graph.
        /// </summary>
        /// <param name="g">g the edge-weighted graph</param>
        public BoruvkaMST(EdgeWeightedGraph g)
        {
            var uf = new UF(g.V);

            // repeat at most log V times or until we have V-1 edges
            for (var t = 1; t < g.V && _mst.Size() < g.V - 1; t = t + t)
            {

                // foreach tree in forest, find closest edge
                // if edge weights are equal, ties are broken in favor of first edge in G.edges()
                var closest = new EdgeW[g.V];
                foreach (var e in g.Edges())
                {
                    int v = e.Either(), w = e.Other(v);
                    int i = uf.Find(v), j = uf.Find(w);
                    if (i == j) continue;   // same tree
                    if (closest[i] == null || Less(e, closest[i])) closest[i] = e;
                    if (closest[j] == null || Less(e, closest[j])) closest[j] = e;
                }

                // add newly discovered edges to MST
                for (var i = 0; i < g.V; i++)
                {
                    var e = closest[i];
                    if (e != null)
                    {
                        int v = e.Either(), w = e.Other(v);
                        // don't add the same edge twice
                        if (!uf.Connected(v, w))
                        {
                            _mst.Add(e);
                            _weight += e.Weight;
                            uf.Union(v, w);
                        }
                    }
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
        /// is the weight of edge e strictly less than that of edge f?
        /// </summary>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        private static bool Less(EdgeW e, EdgeW f)
        {
            return e.Weight < f.Weight;
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
