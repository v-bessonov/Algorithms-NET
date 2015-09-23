using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>NonrecursiveDirectedDFS</tt> class represents a data type for finding
    /// the vertices reachable from a source vertex <em>s</em> in the digraph.
    /// <p>
    /// This implementation uses a nonrecursive version of depth-first search
    /// with an explicit stack.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>,
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// It uses extra space (not including the digraph) proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class NonrecursiveDirectedDFS
    {
        private readonly bool[] _marked;  // marked[v] = is there an s->v path?

        /// <summary>
        /// Computes the vertices reachable from the source vertex <tt>s</tt> in the digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        /// <param name="s">s the source vertex</param>
        public NonrecursiveDirectedDFS(Digraph g, int s)
        {
            _marked = new bool[g.V];

            // to be able to iterate over each adjacency list, keeping track of which
            // vertex in each adjacency list needs to be explored next
            var adj = new IEnumerator<Integer>[g.V];
            for (var v = 0; v < g.V; v++)
                adj[v] = g.Adj(v).GetEnumerator();

            // depth-first search using an explicit stack
            var stack = new Collections.Stack<Integer>();
            _marked[s] = true;
            stack.Push(s);
            while (!stack.IsEmpty())
            {
                int v = stack.Peek();
                if (adj[v].MoveNext())
                {
                    int w = adj[v].Current;
                    // StdOut.printf("check %d\n", w);
                    if (!_marked[w])
                    {
                        // discovered vertex w for the first time
                        _marked[w] = true;
                        // edgeTo[w] = v;
                        stack.Push(w);
                        // StdOut.printf("dfs(%d)\n", w);
                    }
                }
                else
                {
                    // StdOut.printf("%d done\n", v);
                    stack.Pop();
                }
            }
        }

        /// <summary>
        /// Is vertex <tt>v</tt> reachable from the source vertex <tt>s</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns><tt>true</tt> if vertex <tt>v</tt> is reachable from the source vertex <tt>s</tt>, and <tt>false</tt> otherwise</returns>
        public bool Marked(int v)
        {
            return _marked[v];
        }
    }
}
