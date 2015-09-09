namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DepthFirstSearch</tt> class represents a data type for 
    /// determining the vertices connected to a given source vertex <em>s</em>
    /// in an undirected graph. For versions that find the paths, see
    /// {@link DepthFirstPaths} and {@link BreadthFirstPaths}.
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// It uses extra space (not including the graph) proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class DepthFirstSearch
    {

        private readonly bool[] _marked;    // marked[v] = is there an s-v path?
        private int _count;           // number of vertices connected to s

        /// <summary>
        /// Computes the vertices in graph <tt>G</tt> that are
        /// connected to the source vertex <tt>s</tt>.
        /// </summary>
        /// <param name="g">g the graph</param>
        /// <param name="s">s the source vertex</param>
        public DepthFirstSearch(Graph g, int s)
        {
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
            _count++;
            _marked[v] = true;
            foreach (int w in g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(g, w);
                }
            }
        }

        /// <summary>
        /// Is there a path between the source vertex <tt>s</tt> and vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns><tt>true</tt> if there is a path, <tt>false</tt> otherwise</returns>
        public bool Marked(int v)
        {
            return _marked[v];
        }

        /// <summary>
        /// Returns the number of vertices connected to the source vertex <tt>s</tt>.
        /// </summary>
        /// <returns>the number of vertices connected to the source vertex <tt>s</tt></returns>
        public int Count()
        {
            return _count;
        }

    }
}
