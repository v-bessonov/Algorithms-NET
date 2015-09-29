using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DirectedEulerianCycle</tt> class represents a data type
    /// for finding an Eulerian cycle or path in a digraph.
    /// An <em>Eulerian cycle</em> is a cycle (not necessarily simple) that
    /// uses every edge in the digraph exactly once.
    /// <p>
    /// This implementation uses a nonrecursive depth-first search.
    /// The constructor runs in O(<Em>E</em> + <em>V</em>) time,
    /// and uses O(<em>V</em>) extra space, where <em>E</em> is the
    /// number of edges and <em>V</em> the number of vertices
    /// All other methods take O(1) time.
    /// </p>
    /// </summary>
    public class DirectedEulerianCycle
    {
        private readonly Collections.Stack<Integer> _cycle;  // Eulerian cycle; null if no such cylce

        /// <summary>
        /// Computes an Eulerian cycle in the specified digraph, if one exists.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public DirectedEulerianCycle(Digraph g)
        {

            // must have at least one edge
            if (g.E == 0) return;

            // necessary condition: indegree(v) = outdegree(v) for each vertex v
            // (without this check, DFS might return a path instead of a cycle)
            for (var v = 0; v < g.V; v++)
                if (g.Outdegree(v) != g.Indegree(v))
                    return;

            // create local view of adjacency lists, to iterate one vertex at a time
            var adj = new IEnumerator<Integer>[g.V];
            for (var v = 0; v < g.V; v++)
                adj[v] = g.Adj(v).GetEnumerator();

            // initialize stack with any non-isolated vertex
            var s = NonIsolatedVertex(g);
            var stack = new Collections.Stack<Integer>();
            stack.Push(s);

            // greedily add to putative cycle, depth-first search style
            _cycle = new Collections.Stack<Integer>();
            while (!stack.IsEmpty())
            {
                int v = stack.Pop();
                while (adj[v].MoveNext())
                {
                    stack.Push(v);
                    v = adj[v].Current;
                }
                // add vertex with no more leaving edges to cycle
                _cycle.Push(v);
            }

            // check if all edges have been used
            // (in case there are two or more vertex-disjoint Eulerian cycles)
            if (_cycle.Size() != g.E + 1)
                _cycle = null;

            //assert certifySolution(G);
        }

        /// <summary>
        /// Returns the sequence of vertices on an Eulerian cycle.
        /// </summary>
        /// <returns>the sequence of vertices on an Eulerian cycle; <tt>null</tt> if no such cycle</returns>
        public IEnumerable<Integer> Cycle()
        {
            return _cycle;
        }

        /// <summary>
        /// Returns true if the digraph has an Eulerian cycle.
        /// </summary>
        /// <returns><tt>true</tt> if the digraph has an Eulerian cycle; <tt>false</tt> otherwise</returns>
        public bool HasEulerianCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// returns any non-isolated vertex; -1 if no such vertex
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private static int NonIsolatedVertex(Digraph g)
        {
            for (var v = 0; v < g.V; v++)
                if (g.Outdegree(v) > 0)
                    return v;
            return -1;
        }


        #region Testing

        /// <summary>
        /// Determines whether a digraph has an Eulerian cycle using necessary
        /// and sufficient conditions (without computing the cycle itself):
        ///    - at least one edge
        ///    - indegree(v) = outdegree(v) for every vertex
        ///    - the graph is connected, when viewed as an undirected graph
        ///      (ignoring isolated vertices)
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private static bool HasEulerianCycle(Digraph g)
        {

            // Condition 0: at least 1 edge
            if (g.E == 0) return false;

            // Condition 1: indegree(v) == outdegree(v) for every vertex
            for (var v = 0; v < g.V; v++)
                if (g.Outdegree(v) != g.Indegree(v))
                    return false;

            // Condition 2: graph is connected, ignoring isolated vertices
            var h = new Graph(g.V);
            for (var v = 0; v < g.V; v++)
                foreach (int w in g.Adj(v))
                    h.AddEdge(v, w);

            // check that all non-isolated vertices are conneted
            var s = NonIsolatedVertex(g);
            var bfs = new BreadthFirstPaths(h, s);
            for (var v = 0; v < g.V; v++)
                if (h.Degree(v) > 0 && !bfs.HasPathTo(v))
                    return false;

            return true;
        }

        /// <summary>
        /// check that solution is correct
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool CertifySolution(Digraph g)
        {

            // internal consistency check
            if (HasEulerianCycle() == (Cycle() == null)) return false;

            // hashEulerianCycle() returns correct value
            if (HasEulerianCycle() != HasEulerianCycle(g)) return false;

            // nothing else to check if no Eulerian cycle
            if (_cycle == null) return true;

            // check that cycle() uses correct number of edges
            if (_cycle.Size() != g.E + 1) return false;

            // check that cycle() is a directed cycle of G
            // TODO

            return true;
        }

        public static void UnitTest(Digraph g, string description)
        {
            Console.WriteLine(description);
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(g);

            var euler = new DirectedEulerianCycle(g);

            Console.Write("Eulerian cycle: ");
            if (euler.HasEulerianCycle())
            {
                foreach (int v in euler.Cycle())
                {
                    Console.Write($"{v} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("none");
            }
            Console.WriteLine();
        }

        #endregion

    }
}
