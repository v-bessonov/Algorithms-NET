using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DirectedCycleX</tt> class represents a data type for 
    /// determining whether a digraph has a directed cycle.
    /// The <em>hasCycle</em> operation determines whether the digraph has
    /// a directed cycle and, and of so, the <em>cycle</em> operation
    /// returns one.
    /// <p>
    /// This implementation uses a nonrecursive, queue-based algorithm.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>hasCycle</em> operation takes constant time;
    /// the <em>cycle</em> operation takes time proportional
    /// to the length of the cycle.
    /// </p>
    /// </summary>
    public class DirectedCycleX
    {
        private readonly Collections.Stack<Integer> _cycle;     // the directed cycle; null if digraph is acyclic

        public DirectedCycleX(Digraph g)
        {

            // indegrees of remaining vertices
            var indegree = new int[g.V];
            for (var v = 0; v < g.V; v++)
            {
                indegree[v] = g.Indegree(v);
            }

            // initialize queue to contain all vertices with indegree = 0
            var queue = new Collections.Queue<Integer>();
            for (var v = 0; v < g.V; v++)
                if (indegree[v] == 0) queue.Enqueue(v);

            for (var j = 0; !queue.IsEmpty(); j++)
            {
                int v = queue.Dequeue();
                foreach (int w in g.Adj(v))
                {
                    indegree[w]--;
                    if (indegree[w] == 0) queue.Enqueue(w);
                }
            }

            // there is a directed cycle in subgraph of vertices with indegree >= 1.
            var edgeTo = new int[g.V];
            var root = -1;  // any vertex with indegree >= -1
            for (var v = 0; v < g.V; v++)
            {
                if (indegree[v] == 0) continue;
                root = v;
                foreach (int w in g.Adj(v))
                {
                    if (indegree[w] > 0)
                    {
                        edgeTo[w] = v;
                    }
                }
            }

            if (root != -1)
            {

                // find any vertex on cycle
                var visited = new bool[g.V];
                while (!visited[root])
                {
                    visited[root] = true;
                    root = edgeTo[root];
                }

                // extract cycle
                _cycle = new Collections.Stack<Integer>();
                var v = root;
                do
                {
                    _cycle.Push(v);
                    v = edgeTo[v];
                } while (v != root);
                _cycle.Push(root);
            }

            //assert check();
        }

        /// <summary>
        /// Returns a directed cycle if the digraph has a directed cycle, and <tt>null</tt> otherwise.
        /// </summary>
        /// <returns>a directed cycle (as an iterable) if the digraph has a directed cycle, and <tt>null</tt> otherwise</returns>
        public IEnumerable<Integer> Cycle()
        {
            return _cycle;
        }

        /// <summary>
        /// Does the digraph have a directed cycle?
        /// </summary>
        /// <returns><tt>true</tt> if the digraph has a directed cycle, <tt>false</tt> otherwise</returns>
        public bool HasCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// certify that digraph has a directed cycle if it reports one
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {

            if (HasCycle())
            {
                // verify cycle
                int first = -1, last = -1;
                foreach (int v in Cycle())
                {
                    if (first == -1) first = v;
                    last = v;
                }
                if (first != last)
                {
                    Console.Error.WriteLine($"cycle begins with {first} and ends with {last}{Environment.NewLine}");
                    return false;
                }
            }

            return true;
        }
    }
}
