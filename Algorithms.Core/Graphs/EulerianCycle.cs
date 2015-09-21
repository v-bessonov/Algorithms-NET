using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EulerianCycle</tt> class represents a data type
    /// for finding an Eulerian cycle or path in a graph.
    /// An <em>Eulerian cycle</em> is a cycle (not necessarily simple) that
    /// uses every edge in the graph exactly once.
    /// <p>
    /// This implementation uses a nonrecursive depth-first search.
    /// The constructor runs in O(<Em>E</em> + <em>V</em>) time,
    /// and uses O(<em>E</em> + <em>V</em>) extra space, where <em>E</em> is the
    /// number of edges and <em>V</em> the number of vertices
    /// All other methods take O(1) time.
    /// </p>
    /// </summary>
    public class EulerianCycle
    {
        private readonly Collections.Stack<Integer> _cycle = new Collections.Stack<Integer>();  // Eulerian cycle; null if no such cycle


        /// <summary>
        /// Computes an Eulerian cycle in the specified graph, if one exists.
        /// </summary>
        /// <param name="g">g the graph</param>
        public EulerianCycle(Graph g)
        {

            // must have at least one EdgeW
            if (g.E == 0) return;

            // necessary condition: all vertices have even degree
            // (this test is needed or it might find an Eulerian path instead of cycle)
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) % 2 != 0)
                    return;

            // create local view of adjacency lists, to iterate one vertex at a time
            // the helper EdgeW data type is used to avoid exploring both copies of an EdgeW v-w
            var adj = new Collections.Queue<EdgeW>[g.V];
            for (var v = 0; v < g.V; v++)
                adj[v] = new Collections.Queue<EdgeW>();

            for (var v = 0; v < g.V; v++)
            {
                var selfLoops = 0;
                foreach (int w in g.Adj(v))
                {
                    // careful with self loops
                    if (v == w)
                    {
                        if (selfLoops % 2 == 0)
                        {
                            var e = new EdgeW(v, w, 0);
                            adj[v].Enqueue(e);
                            adj[w].Enqueue(e);
                        }
                        selfLoops++;
                    }
                    else if (v < w)
                    {
                        var e = new EdgeW(v, w, 0);
                        adj[v].Enqueue(e);
                        adj[w].Enqueue(e);
                    }
                }
            }

            // initialize Collections.Stack with any non-isolated vertex
            var s = NonIsolatedVertex(g);
            var stack = new Collections.Stack<Integer>();
            stack.Push(s);

            // greedily search through EdgeWs in iterative DFS style
            _cycle = new Collections.Stack<Integer>();
            while (!stack.IsEmpty())
            {
                int v = stack.Pop();
                while (!adj[v].IsEmpty())
                {
                    var edgeW = adj[v].Dequeue();
                    if (edgeW.IsUsed) continue;
                    edgeW.IsUsed = true;
                    stack.Push(v);
                    v = edgeW.Other(v);
                }
                // push vertex with no more leaving EdgeWs to cycle
                _cycle.Push(v);
            }

            // check if all EdgeWs are used
            if (_cycle.Size() != g.E + 1)
                _cycle = null;

            //assert certifySolution(G);
        }

        /// <summary>
        /// Returns the sequence of vertices on an Eulerian cycle.
        /// </summary>
        /// <returns>the sequence of vertices on an Eulerian cycle;  <tt>null</tt> if no such cycle</returns>
        public IEnumerable<Integer> Cycle()
        {
            return _cycle;
        }

        /// <summary>
        /// Returns true if the graph has an Eulerian cycle.
        /// </summary>
        /// <returns><tt>true</tt> if the graph has an Eulerian cycle; <tt>false</tt> otherwise</returns>
        public bool HasEulerianCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// returns any non-isolated vertex; -1 if no such vertex
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private static int NonIsolatedVertex(Graph g)
        {
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) > 0)
                    return v;
            return -1;
        }


        #region Testing

        /// <summary>
        /// Determines whether a graph has an Eulerian cycle using necessary
        /// and sufficient conditions (without computing the cycle itself):
        ///    - at least one EdgeW
        ///    - degree(v) is even for every vertex v
        ///    - the graph is connected (ignoring isolated vertices)
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private static bool HasEulerianCycle(Graph g)
        {

            // Condition 0: at least 1 EdgeW
            if (g.E == 0) return false;

            // Condition 1: degree(v) is even for every vertex
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) % 2 != 0)
                    return false;

            // Condition 2: graph is connected, ignoring isolated vertices
            var s = NonIsolatedVertex(g);
            var bfs = new BreadthFirstPaths(g, s);
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) > 0 && !bfs.HasPathTo(v))
                    return false;

            return true;
        }

        /// <summary>
        /// check that solution is correct
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool CertifySolution(Graph g)
        {

            // internal consistency check
            if (HasEulerianCycle() == (Cycle() == null)) return false;

            // hashEulerianCycle() returns correct value
            if (HasEulerianCycle() != HasEulerianCycle(g)) return false;

            // nothing else to check if no Eulerian cycle
            if (_cycle == null) return true;

            // check that cycle() uses correct number of EdgeWs
            if (_cycle.Size() != g.E + 1) return false;

            // check that cycle() is a cycle of G
            // TODO

            // check that first and last vertices in cycle() are the same
            int first = -1, last = -1;
            foreach (int v in Cycle())
            {
                if (first == -1) first = v;
                last = v;
            }
            if (first != last) return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="description"></param>
        public static void UnitTest(Graph g, string description)
        {
            Console.WriteLine(description);
            Console.WriteLine("-------------------------------------");
            Console.Write(g);

            var euler = new EulerianCycle(g);

            Console.Write("Eulerian cycle: ");
            if (euler.HasEulerianCycle())
            {
                foreach (int v in euler.Cycle())
                {
                    Console.Write(v + " ");
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
