using System;
using System.Collections.Generic;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>DijkstraAllPairsSP</tt> class represents a data type for solving the
    ///  all-pairs shortest paths problem in edge-weighted digraphs
    ///  where the edge weights are nonnegative.
    ///  <p>
    ///  This implementation runs Dijkstra's algorithm from each vertex.
    ///  The constructor takes time proportional to <em>V</em> (<em>E</em> log <em>V</em>)
    ///  and uses space proprtional to <em>V</em><sup>2</sup>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>dist()</tt> and <tt>hasPath()</tt> methods take
    ///  constant time and the <tt>path()</tt> method takes time proportional to the
    ///  number of edges in the shortest path returned.
    ///  </p>
    public class DijkstraAllPairsSP
    {
        private readonly DijkstraSP[] _all;

        /// <summary>
        /// Computes a shortest paths tree from each vertex to to every other vertex in
        /// the edge-weighted digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">G the edge-weighted digraph</param>
        /// <exception cref="ArgumentException">if an edge weight is negative</exception>
        /// <exception cref="ArgumentException">unless 0 &lt;= <tt>s</tt> &lt;= <tt>V</tt> - 1</exception>
        public DijkstraAllPairsSP(EdgeWeightedDigraph g)
        {
            _all = new DijkstraSP[g.V];
            for (var v = 0; v < g.V; v++)
                _all[v] = new DijkstraSP(g, v);
        }

        /// <summary>
        /// Returns a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt>.
        /// </summary>
        /// <param name="s">s the source vertex</param>
        /// <param name="t">t the destination vertex</param>
        /// <returns>a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt> as an iterable of edges, and <tt>null</tt> if no such path</returns>
        public IEnumerable<DirectedEdge> Path(int s, int t)
        {
            return _all[s].PathTo(t);
        }

        /// <summary>
        /// Is there a path from the vertex <tt>s</tt> to vertex <tt>t</tt>?
        /// </summary>
        /// <param name="s">s the source vertex</param>
        /// <param name="t">t the destination vertex</param>
        /// <returns><tt>true</tt> if there is a path from vertex <tt>s</tt> to vertex <tt>t</tt>, and <tt>false</tt> otherwise</returns>
        public bool HasPath(int s, int t)
        {
            return Dist(s, t) < double.PositiveInfinity;
        }

        /// <summary>
        /// Returns the length of a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt>.
        /// </summary>
        /// <param name="s">s the source vertex</param>
        /// <param name="t">t the destination vertex</param>
        /// <returns>the length of a shortest path from vertex <tt>s</tt> to vertex <tt>t</tt>; <tt>Double.POSITIVE_INFINITY</tt> if no such path</returns>
        public double Dist(int s, int t)
        {
            return _all[s].DistTo(t);
        }
    }
}
