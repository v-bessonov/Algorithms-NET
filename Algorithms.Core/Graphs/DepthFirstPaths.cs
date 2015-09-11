using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DepthFirstPaths</tt> class represents a data type for finding
    /// paths from a source vertex <em>s</em> to every other vertex
    /// in an undirected graph.
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>,
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// It uses extra space (not including the graph) proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class DepthFirstPaths
    {
        private readonly bool[] _marked;    // marked[v] = is there an s-v path?
        private readonly int[] _edgeTo;        // edgeTo[v] = last edge on s-v path
        private readonly int _s;         // source vertex

        /// <summary>
        /// Computes a path between <tt>s</tt> and every other vertex in graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the graph</param>
        /// <param name="s">s the source vertex</param>
        public DepthFirstPaths(Graph g, int s)
        {
            _s = s;
            _edgeTo = new int[g.V];
            _marked = new bool[g.V];
            Dfs(g, s);
        }

        /// <summary>
        /// depth first search from v
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(Graph g, int v)
        {
            _marked[v] = true;
            foreach (int w in g.Adj(v))
            {
                if (_marked[w]) continue;
                _edgeTo[w] = v;
                Dfs(g, w);
            }
        }

        /// <summary>
        /// Is there a path between the source vertex <tt>s</tt> and vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns><tt>true</tt> if there is a path, <tt>false</tt> otherwise</returns>
        public bool HasPathTo(int v)
        {
            return _marked[v];
        }

        /// <summary>
        /// Returns a path between the source vertex <tt>s</tt> and vertex <tt>v</tt>, or
        /// <tt>null</tt> if no such path.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the sequence of vertices on a path between the source vertex <tt>s</tt> and vertex <tt>v</tt>, as an Iterable</returns>
        public IEnumerable<Integer> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Collections.Stack<Integer>();
            for (var x = v; x != _s; x = _edgeTo[x])
                path.Push(x);
            path.Push(_s);
            return path;
        }

    }
}
