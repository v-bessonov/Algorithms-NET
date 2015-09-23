using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DirectedDFS</tt> class represents a data type for 
    /// determining the vertices reachable from a given source vertex <em>s</em>
    /// (or set of source vertices) in a digraph. For versions that find the paths,
    /// see {@link DepthFirstDirectedPaths} and {@link BreadthFirstDirectedPaths}.
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// </p>
    /// </summary>
    public class DirectedDFS
    {
        private readonly bool[] _marked;  // marked[v] = true if v is reachable
                                   // from source (or sources)
        private int _count;         // number of vertices reachable from s

        /// <summary>
        /// Computes the vertices in digraph <tt>G</tt> that are
        /// reachable from the source vertex <tt>s</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        /// <param name="s">s the source vertex</param>
        public DirectedDFS(Digraph g, int s)
        {
            _marked = new bool[g.V];
            Dfs(g, s);
        }

        /// <summary>
        /// Computes the vertices in digraph <tt>G</tt> that are
        /// connected to any of the source vertices <tt>sources</tt>.
        /// </summary>
        /// <param name="g">g the graph</param>
        /// <param name="sources">sources the source vertices</param>
        public DirectedDFS(Digraph g, IEnumerable<Integer> sources)
        {
            _marked = new bool[g.V];
            foreach (int v in sources)
            {
                if (!_marked[v]) Dfs(g, v);
            }
        }

        private void Dfs(Digraph g, int v)
        {
            _count++;
            _marked[v] = true;
            foreach (int w in g.Adj(v))
            {
                if (!_marked[w]) Dfs(g, w);
            }
        }

        /// <summary>
        /// Is there a directed path from the source vertex (or any
        /// of the source vertices) and vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns><tt>true</tt> if there is a directed path, <tt>false</tt> otherwise</returns>
        public bool Marked(int v)
        {
            return _marked[v];
        }

        /// <summary>
        /// Returns the number of vertices reachable from the source vertex
        /// (or source vertices).
        /// </summary>
        /// <returns>the number of vertices reachable from the source vertex (or source vertices)</returns>
        public int Count()
        {
            return _count;
        }
    }
}
