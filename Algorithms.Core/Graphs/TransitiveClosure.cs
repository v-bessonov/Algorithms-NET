namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>TransitiveClosure</tt> class represents a data type for
    /// computing the transitive closure of a digraph.
    /// <p>
    /// This implementation runs depth-first search from each vertex.
    /// The constructor takes time proportional to <em>V</em>(<em>V</em> + <em>E</em>)
    /// (in the worst case) and uses space proportional to <em>V</em><sup>2</sup>,
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// </p>
    /// </summary>
    public class TransitiveClosure
    {
        private readonly DirectedDFS[] _tc;  // tc[v] = reachable from v

        /// <summary>
        /// Computes the transitive closure of the digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public TransitiveClosure(Digraph g)
        {
            _tc = new DirectedDFS[g.V];
            for (var v = 0; v < g.V; v++)
                _tc[v] = new DirectedDFS(g, v);
        }

        /// <summary>
        /// Is there a directed path from vertex <tt>v</tt> to vertex <tt>w</tt> in the digraph?
        /// </summary>
        /// <param name="v">v the source vertex</param>
        /// <param name="w">w the target vertex</param>
        /// <returns><tt>true</tt> if there is a directed path from <tt>v</tt> to <tt>w</tt>, <tt>false</tt> otherwise</returns>
        public bool Reachable(int v, int w)
        {
            return _tc[v].Marked(w);
        }
    }
}
