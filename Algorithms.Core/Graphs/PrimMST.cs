﻿using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core.QuickUnionUF;
using Algorithms.Core.Sorting;
using Double = Algorithms.Core.Helpers.Double;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>PrimMST</tt> class represents a data type for computing a
    ///  <em>minimum spanning tree</em> in an edge-weighted graph.
    ///  The edge weights can be positive, zero, or negative and need not
    ///  be distinct. If the graph is not connected, it computes a <em>minimum
    ///  spanning forest</em>, which is the union of minimum spanning trees
    ///  in each connected component. The <tt>weight()</tt> method returns the 
    ///  weight of a minimum spanning tree and the <tt>edges()</tt> method
    ///  returns its edges.
    ///  <p>
    ///  This implementation uses <em>Prim's algorithm</em> with an indexed
    ///  binary heap.
    ///  The constructor takes time proportional to <em>E</em> log <em>V</em>
    ///  and extra space (not including the graph) proportional to <em>V</em>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>weight()</tt> method takes constant time
    ///  and the <tt>edges()</tt> method takes time proportional to <em>V</em>.
    ///  </p>
    public class PrimMST
    {
        private static double FLOATING_POINT_EPSILON = 1E-12;

        private readonly EdgeW[] _edgeTo;        // edgeTo[v] = shortest edge from tree vertex to non-tree vertex
        private readonly double[] _distTo;      // distTo[v] = weight of shortest such edge
        private readonly bool[] _marked;     // marked[v] = true if v on tree, false otherwise
        private readonly IndexMinPQ<Double> _pq;

        /// <summary>
        /// Compute a minimum spanning tree (or forest) of an edge-weighted graph.
        /// </summary>
        /// <param name="g">g the edge-weighted graph</param>
        public PrimMST(EdgeWeightedGraph g)
        {
            _edgeTo = new EdgeW[g.V];
            _distTo = new double[g.V];
            _marked = new bool[g.V];
            _pq = new IndexMinPQ<Double>(g.V);
            for (var v = 0; v < g.V; v++)
                _distTo[v] = double.PositiveInfinity;

            for (var v = 0; v < g.V; v++)      // run from each vertex to find
                if (!_marked[v]) Prim(g, v);      // minimum spanning forest

            // check optimality conditions
            //assert check(G);
        }

        /// <summary>
        /// run Prim's algorithm in graph G, starting from vertex s
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        private void Prim(EdgeWeightedGraph g, int s)
        {
            _distTo[s] = 0.0;
            _pq.Insert(s, _distTo[s]);
            while (!_pq.IsEmpty())
            {
                var v = _pq.DelMin();
                Scan(g, v);
            }
        }

        /// <summary>
        /// scan vertex v
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Scan(EdgeWeightedGraph g, int v)
        {
            _marked[v] = true;
            foreach (var e in g.Adj(v))
            {
                var w = e.Other(v);
                if (_marked[w]) continue;         // v-w is obsolete edge
                if (e.Weight < _distTo[w])
                {
                    _distTo[w] = e.Weight;
                    _edgeTo[w] = e;
                    if (_pq.Contains(w)) _pq.DecreaseKey(w, _distTo[w]);
                    else _pq.Insert(w, _distTo[w]);
                }
            }
        }

        /// <summary>
        /// Returns the edges in a minimum spanning tree (or forest).
        /// </summary>
        /// <returns>the edges in a minimum spanning tree (or forest) as an iterable of edges</returns>
        public IEnumerable<EdgeW> Edges()
        {
            var mst = new Collections.Queue<EdgeW>();
            for (var v = 0; v < _edgeTo.Length; v++)
            {
                var e = _edgeTo[v];
                if (e != null)
                {
                    mst.Enqueue(e);
                }
            }
            return mst;
        }

        /// <summary>
        /// Returns the sum of the edge weights in a minimum spanning tree (or forest).
        /// </summary>
        /// <returns>the sum of the edge weights in a minimum spanning tree (or forest)</returns>
        public double Weight()
        {
            return Edges().Sum(e => e.Weight);
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
                foreach (var f in Edges())
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
