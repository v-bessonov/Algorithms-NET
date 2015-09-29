using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DirectedEulerianPath</tt> class represents a data type
    /// for finding an Eulerian path in a digraph.
    /// An <em>Eulerian path</em> is a path (not necessarily simple) that
    /// uses every edge in the digraph exactly once.
    /// <p>
    /// This implementation uses a nonrecursive depth-first search.
    /// The constructor runs in O(E + V) time, and uses O(V) extra space,
    /// where E is the number of edges and V the number of vertices
    /// All other methods take O(1) time.
    /// </p>
    /// </summary>
    public class DirectedEulerianPath
    {
        private readonly Collections.Stack<Integer> _path;   // Eulerian path; null if no suh path

        /// <summary>
        /// Computes an Eulerian path in the specified digraph, if one exists.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public DirectedEulerianPath(Digraph g)
        {

            // find vertex from which to start potential Eulerian path:
            // a vertex v with outdegree(v) > indegree(v) if it exits;
            // otherwise a vertex with outdegree(v) > 0
            var deficit = 0;
            var s = NonIsolatedVertex(g);
            for (var v = 0; v < g.V; v++)
            {
                if (g.Outdegree(v) > g.Indegree(v))
                {
                    deficit += (g.Outdegree(v) - g.Indegree(v));
                    s = v;
                }
            }

            // digraph can't have an Eulerian path
            // (this condition is needed)
            if (deficit > 1) return;

            // special case for digraph with zero edges (has a degenerate Eulerian path)
            if (s == -1) s = 0;

            // create local view of adjacency lists, to iterate one vertex at a time
            var adj = new IEnumerator<Integer>[g.V];
            for (var v = 0; v < g.V; v++)
                adj[v] = g.Adj(v).GetEnumerator();

            // greedily add to cycle, depth-first search style
            var stack = new Collections.Stack<Integer>();
            stack.Push(s);
            _path = new Collections.Stack<Integer>();
            while (!stack.IsEmpty())
            {
                int v = stack.Pop();
                while (adj[v].MoveNext())
                {
                    stack.Push(v);
                    v = adj[v].Current;
                }
                // push vertex with no more available edges to path
                _path.Push(v);
            }

            // check if all edges have been used
            if (_path.Size() != g.E + 1)
                _path = null;

            //assert check(G);
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
        /// Returns true if the digraph has an Eulerian path.
        /// </summary>
        /// <returns><tt>true</tt> if the digraph has an Eulerian path; <tt>false</tt> otherwise</returns>
        public bool HasEulerianPath()
        {
            return _path != null;
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
        /// Determines whether a digraph has an Eulerian path using necessary
        /// and sufficient conditions (without computing the path itself):
        ///    - indegree(v) = outdegree(v) for every vertex,
        ///      except one vertex v may have outdegree(v) = indegree(v) + 1
        ///      (and one vertex v may have indegree(v) = outdegree(v) + 1)
        ///    - the graph is connected, when viewed as an undirected graph
        ///      (ignoring isolated vertices)
        /// This method is solely for unit testing.
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private static bool HasEulerianPath(Digraph g)
        {
            if (g.E == 0) return true;

            // Condition 1: indegree(v) == outdegree(v) for every vertex,
            // except one vertex may have outdegree(v) = indegree(v) + 1
            var deficit = 0;
            for (var v = 0; v < g.V; v++)
                if (g.Outdegree(v) > g.Indegree(v))
                    deficit += (g.Outdegree(v) - g.Indegree(v));
            if (deficit > 1) return false;

            // Condition 2: graph is connected, ignoring isolated vertices
            var h = new Graph(g.V);
            for (var v = 0; v < g.V; v++)
                foreach (int w in g.Adj(v))
                    h.AddEdge(v, w);

            // check that all non-isolated vertices are connected
            var s = NonIsolatedVertex(g);
            var bfs = new BreadthFirstPaths(h, s);
            for (var v = 0; v < g.V; v++)
                if (h.Degree(v) > 0 && !bfs.HasPathTo(v))
                    return false;

            return true;
        }


        public bool Check(Digraph g)
        {

            // internal consistency check
            if (HasEulerianPath() == (Path() == null)) return false;

            // hashEulerianPath() returns correct value
            if (HasEulerianPath() != HasEulerianPath(g)) return false;

            // nothing else to check if no Eulerian path
            if (_path == null) return true;

            // check that path() uses correct number of edges
            if (_path.Size() != g.E + 1) return false;

            // check that path() is a directed path in G
            // TODO

            return true;
        }

        public static void UnitTest(Digraph g, string description)
        {
            Console.WriteLine(description);
            Console.WriteLine("-------------------------------------");
            Console.Write(g);

            var euler = new DirectedEulerianPath(g);

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
