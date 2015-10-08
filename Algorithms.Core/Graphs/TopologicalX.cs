using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>TopologicalX</tt> class represents a data type for 
    /// determining a topological order of a directed acyclic graph (DAG).
    /// Recall, a digraph has a topological order if and only if it is a DAG.
    /// The <em>hasOrder</em> operation determines whether the digraph has
    /// a topological order, and if so, the <em>order</em> operation
    /// returns one.
    /// <p>
    /// This implementation uses a nonrecursive, queue-based algorithm.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>hasOrder</em> and <em>rank</em> operations takes constant time;
    /// the <em>order</em> operation takes time proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class TopologicalX
    {
        private readonly Collections.Queue<Integer> _order;     // vertices in topological order
        private readonly int[] _rank;               // rank[v] = order where vertex v appers in order

        /// <summary>
        /// Determines whether the digraph <tt>G</tt> has a topological order and, if so,
        /// finds such a topological order.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public TopologicalX(Digraph g)
        {

            // indegrees of remaining vertices
            var indegree = new int[g.V];
            for (var v = 0; v < g.V; v++)
            {
                indegree[v] = g.Indegree(v);
            }

            // initialize 
            _rank = new int[g.V];
            _order = new Collections.Queue<Integer>();
            var count = 0;

            // initialize queue to contain all vertices with indegree = 0
            var queue = new Collections.Queue<Integer>();
            for (var v = 0; v < g.V; v++)
                if (indegree[v] == 0) queue.Enqueue(v);

            for (var j = 0; !queue.IsEmpty(); j++)
            {
                int v = queue.Dequeue();
                _order.Enqueue(v);
                _rank[v] = count++;
                foreach (int w in g.Adj(v))
                {
                    indegree[w]--;
                    if (indegree[w] == 0) queue.Enqueue(w);
                }
            }

            // there is a directed cycle in subgraph of vertices with indegree >= 1.
            if (count != g.V)
            {
                _order = null;
            }

            //assert check(G);
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
        /// certify that digraph is acyclic
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool Check(Digraph g)
        {

            // digraph is acyclic
            if (HasOrder())
            {
                // check that ranks are a permutation of 0 to V-1
                var found = new bool[g.V];
                for (var i = 0; i < g.V; i++)
                {
                    found[Rank(i)] = true;
                }
                for (var i = 0; i < g.V; i++)
                {
                    if (!found[i])
                    {
                        Console.Error.WriteLine($"No vertex with rank {i}");
                        return false;
                    }
                }

                // check that ranks provide a valid topological order
                for (var v = 0; v < g.V; v++)
                {
                    foreach (int w in g.Adj(v))
                    {
                        if (Rank(v) > Rank(w))
                        {
                            Console.WriteLine($"{v}-{w}: rank({v}) = {Rank(v)}, rank({w}) = {Rank(w)}{Environment.NewLine}");
                            return false;
                        }
                    }
                }

                // check that order() is consistent with rank()
                var r = 0;
                foreach (int v in Order())
                {
                    if (Rank(v) != r)
                    {
                        Console.WriteLine("order() and rank() inconsistent");
                        return false;
                    }
                    r++;
                }
            }


            return true;
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
