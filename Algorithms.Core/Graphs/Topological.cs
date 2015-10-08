using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>Topological</tt> class represents a data type for 
    /// determining a topological order of a directed acyclic graph (DAG).
    /// Recall, a digraph has a topological order if and only if it is a DAG.
    /// The <em>hasOrder</em> operation determines whether the digraph has
    /// a topological order, and if so, the <em>order</em> operation
    /// returns one.
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>hasOrder</em> and <em>rank</em> operations takes constant time;
    /// the <em>order</em> operation takes time proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class Topological
    {
        private readonly IEnumerable<Integer> _order;  // topological order
        private readonly int[] _rank;               // rank[v] = position of vertex v in topological order

        /// <summary>
        /// Determines whether the digraph <tt>G</tt> has a topological order and, if so,
        /// finds such a topological order.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public Topological(Digraph g)
        {
            var finder = new DirectedCycle(g);
            if (!finder.HasCycle())
            {
                var dfs = new DepthFirstOrder(g);
                _order = dfs.ReversePost();
                _rank = new int[g.V];
                var i = 0;
                foreach (int v in _order)
                    _rank[v] = i++;
            }
        }

        /// <summary>
        /// Determines whether the edge-weighted digraph <tt>G</tt> has a topological
        /// order and, if so, finds such an order.
        /// </summary>
        /// <param name="g">g the edge-weighted digraph</param>
        public Topological(EdgeWeightedDigraph g)
        {
            var finder = new EdgeWeightedDirectedCycle(g);
            if (!finder.HasCycle())
            {
                var dfs = new DepthFirstOrder(g);
                _order = dfs.ReversePost();
            }
        }

        /// <summary>
        /// Returns a topological order if the digraph has a topologial order,
        /// and <tt>null</tt> otherwise.
        /// </summary>
        /// <returns>a topological order of the vertices (as an interable) if the digraph has a topological order (or equivalently, if the digraph is a DAG), and <tt>null</tt> otherwise</returns>
        public IEnumerable<Integer> Order()
        {
            return _order;
        }

        /// <summary>
        /// Does the digraph have a topological order?
        /// </summary>
        /// <returns><tt>true</tt> if the digraph has a topological order (or equivalently, if the digraph is a DAG), and <tt>false</tt> otherwise</returns>
        public bool HasOrder()
        {
            return _order != null;
        }

        /// <summary>
        /// The the rank of vertex <tt>v</tt> in the topological order;
        /// -1 if the digraph is not a DAG
        /// </summary>
        /// <param name="v"></param>
        /// <returns>the position of vertex <tt>v</tt> in a topological order of the digraph; -1 if the digraph is not a DAG</returns>
        /// <exception cref="IndexOutOfRangeException">unless <tt>v</tt> is between 0 and <em>V</em> &amp;minus; 1</exception>
        public int Rank(int v)
        {
            ValidateVertex(v);
            if (HasOrder()) return _rank[v];
            return -1;
        }

        /// <summary>
        /// throw an IndexOutOfBoundsException unless 0 &lt;= v &lt; V
        /// </summary>
        /// <param name="v"></param>
        private void ValidateVertex(int v)
        {
            var length = _rank.Length;
            if (v < 0 || v >= length)
                throw new IndexOutOfRangeException($"vertex {v} is not between 0 and {(length - 1)}");
        }

    }
}
