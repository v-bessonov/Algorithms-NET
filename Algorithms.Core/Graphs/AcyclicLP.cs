using System;
using System.Collections.Generic;

namespace Algorithms.Core.Graphs
{
    ///  The <tt>AcyclicLP</tt> class represents a data type for solving the
    ///  single-source longest paths problem in edge-weighted directed
    ///  acyclic graphs (DAGs). The edge weights can be positive, negative, or zero.
    ///  <p>
    ///  This implementation uses a topological-sort based algorithm.
    ///  The constructor takes time proportional to <em>V</em> + <em>E</em>,
    ///  where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    ///  Afterwards, the <tt>distTo()</tt> and <tt>hasPathTo()</tt> methods take
    ///  constant time and the <tt>pathTo()</tt> method takes time proportional to the
    ///  number of edges in the longest path returned.
    ///  </p>
    public class AcyclicLP
    {
        private readonly double[] _distTo;          // distTo[v] = distance  of longest s->v path
        private readonly DirectedEdge[] _edgeTo;    // edgeTo[v] = last edge on longest s->v path

        /// <summary>
        /// Computes a longest paths tree from <tt>s</tt> to every other vertex in
        /// the directed acyclic graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the acyclic digraph</param>
        /// <param name="s">s the source vertex</param>
        /// <exception cref="ArgumentException">if the digraph is not acyclic</exception>
        /// <exception cref="ArgumentException">unless 0 &lt;= <tt>s</tt> &lt;= <tt>V</tt> - 1</exception>
        public AcyclicLP(EdgeWeightedDigraph g, int s)
        {
            _distTo = new double[g.V];
            _edgeTo = new DirectedEdge[g.V];
            for (var v = 0; v < g.V; v++)
                _distTo[v] = double.NegativeInfinity;
            _distTo[s] = 0.0;

            // relax vertices in toplogical order
            var topological = new Topological(g);
            if (!topological.HasOrder())
                throw new ArgumentException("Digraph is not acyclic.");
            foreach (int v in topological.Order())
            {
                foreach (var e in g.Adj(v))
                    Relax(e);
            }
        }

        /// <summary>
        /// relax edge e, but update if you find a *longer* path
        /// </summary>
        /// <param name="e"></param>
        private void Relax(DirectedEdge e)
        {
            int v = e.From(), w = e.To();
            if (_distTo[w] < _distTo[v] + e.Weight)
            {
                _distTo[w] = _distTo[v] + e.Weight;
                _edgeTo[w] = e;
            }
        }

        /// <summary>
        /// Returns the length of a longest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns>the length of a longest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>; <tt>Double.NEGATIVE_INFINITY</tt> if no such path</returns>
        public double DistTo(int v)
        {
            return _distTo[v];
        }

        /// <summary>
        /// Is there a path from the source vertex <tt>s</tt> to vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns><tt>true</tt> if there is a path from the source vertex <tt>s</tt> to vertex <tt>v</tt>, and <tt>false</tt> otherwise</returns>
        public bool HasPathTo(int v)
        {
            return _distTo[v] > double.NegativeInfinity;
        }

        /// <summary>
        /// Returns a longest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the destination vertex</param>
        /// <returns>a longest path from the source vertex <tt>s</tt> to vertex <tt>v</tt> as an iterable of edges, and <tt>null</tt> if no such path</returns>
        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Collections.Stack<DirectedEdge>();
            for (var e = _edgeTo[v]; e != null; e = _edgeTo[e.From()])
            {
                path.Push(e);
            }
            return path;
        }
    }
}
