using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EulerianPath</tt> class represents a data type
    /// for finding an Eulerian path in a graph.
    /// An <em>Eulerian path</em> is a path (not necessarily simple) that
    /// uses every edge in the graph exactly once.
    /// <p>
    /// This implementation uses a nonrecursive depth-first search.
    /// The constructor runs in O(<Em>E</em> + <em>V</em>) time,
    /// and uses O(<em>E</em> + <em>V</em>) extra space, where <em>E</em> is the
    /// number of edges and <em>V</em> the number of vertices
    /// All other methods take O(1) time.
    /// </p>
    /// </summary>
    public class EulerianPath
    {
        private readonly Collections.Stack<Integer> _path;   // Eulerian path; null if no suh path


        /// <summary>
        /// Computes an Eulerian path in the specified graph, if one exists.
        /// </summary>
        /// <param name="g">g the graph</param>
        public EulerianPath(Graph g)
        {

            // find vertex from which to start potential Eulerian path:
            // a vertex v with odd degree(v) if it exits;
            // otherwise a vertex with degree(v) > 0
            var oddDegreeVertices = 0;
            var s = NonIsolatedVertex(g);
            for (var v = 0; v < g.V; v++)
            {
                if (g.Degree(v) % 2 != 0)
                {
                    oddDegreeVertices++;
                    s = v;
                }
            }

            // graph can't have an Eulerian path
            // (this condition is needed for correctness)
            if (oddDegreeVertices > 2) return;

            // special case for graph with zero edges (has a degenerate Eulerian path)
            if (s == -1) s = 0;

            // create local view of adjacency lists, to iterate one vertex at a time
            // the helper Edge data type is used to avoid exploring both copies of an edge v-w
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

            // initialize stack with any non-isolated vertex
            var stack = new Collections.Stack<Integer>();
            stack.Push(s);

            // greedily search through edges in iterative DFS style
            _path = new Collections.Stack<Integer>();
            while (!stack.IsEmpty())
            {
                int v = stack.Pop();
                while (!adj[v].IsEmpty())
                {
                    var edge = adj[v].Dequeue();
                    if (edge.IsUsed) continue;
                    edge.IsUsed = true;
                    stack.Push(v);
                    v = edge.Other(v);
                }
                // push vertex with no more leaving edges to path
                _path.Push(v);
            }

            // check if all edges are used
            if (_path.Size() != g.E + 1)
                _path = null;

            //assert certifySolution(G);
        }

        /// <summary>
        /// Returns the sequence of vertices on an Eulerian path.
        /// </summary>
        /// <returns>the sequence of vertices on an Eulerian path; <tt>null</tt> if no such path</returns>
        public IEnumerable<Integer> Path()
        {
            return _path;
        }

        /// <summary>
        /// Returns true if the graph has an Eulerian path.
        /// </summary>
        /// <returns><tt>true</tt> if the graph has an Eulerian path; <tt>false</tt> otherwise</returns>
        public bool HasEulerianPath()
        {
            return _path != null;
        }


        // returns any non-isolated vertex; -1 if no such vertex
        private static int NonIsolatedVertex(Graph g)
        {
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) > 0)
                    return v;
            return -1;
        }


        #region Testing

        /// <summary>
        /// Determines whether a graph has an Eulerian path using necessary
        /// and sufficient conditions (without computing the path itself):
        ///    - degree(v) is even for every vertex, except for possibly two
        ///    - the graph is connected (ignoring isolated vertices)
        /// This method is solely for unit testing.
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private static bool HasEulerianPath(Graph g)
        {
            if (g.E == 0) return true;

            // Condition 1: degree(v) is even except for possibly two
            var oddDegreeVertices = 0;
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) % 2 != 0)
                    oddDegreeVertices++;
            if (oddDegreeVertices > 2) return false;

            // Condition 2: graph is connected, ignoring isolated vertices
            var s = NonIsolatedVertex(g);
            var bfs = new BreadthFirstPaths(g, s);
            for (var v = 0; v < g.V; v++)
                if (g.Degree(v) > 0 && !bfs.HasPathTo(v))
                    return false;

            return true;
        }

        // check that solution is correct
        public bool CertifySolution(Graph g)
        {

            // internal consistency check
            if (HasEulerianPath() == (Path() == null)) return false;

            // hashEulerianPath() returns correct value
            if (HasEulerianPath() != HasEulerianPath(g)) return false;

            // nothing else to check if no Eulerian path
            if (_path == null) return true;

            // check that path() uses correct number of edges
            if (_path.Size() != g.E + 1) return false;

            // check that path() is a path in G
            // TODO

            return true;
        }


        public static void UnitTest(Graph g, string description)
        {
            Console.WriteLine(description);
            Console.WriteLine("-------------------------------------");
            Console.Write(g);

            var euler = new EulerianPath(g);

            Console.Write("Eulerian path:  ");
            if (euler.HasEulerianPath())
            {
                foreach (int v in euler.Path())
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
