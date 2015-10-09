namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>KosarajuSharirSCC</tt> class represents a data type for
    /// determining the strong components in a digraph.
    /// The <em>id</em> operation determines in which strong component
    /// a given vertex lies; the <em>areStronglyConnected</em> operation
    /// determines whether two vertices are in the same strong component;
    /// and the <em>count</em> operation determines the number of strong
    /// components.
    /// The <em>component identifier</em> of a component is one of the
    /// vertices in the strong component: two vertices have the same component
    /// identifier if and only if they are in the same strong component.
    /// <p>
    /// This implementation uses the Kosaraju-Sharir algorithm.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>id</em>, <em>count</em>, and <em>areStronglyConnected</em>
    /// operations take constant time.
    /// For alternate implementations of the same API, see
    /// {@link TarjanSCC} and {@link GabowSCC}.
    /// </p>
    /// </summary>
    public class KosarajuSharirSCC
    {
        private readonly bool[] _marked;     // marked[v] = has vertex v been visited?
        private readonly int[] _id;             // id[v] = id of strong component containing v
        private readonly int _count;            // number of strongly-connected components

        /// <summary>
        /// Computes the strong components of the digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public KosarajuSharirSCC(Digraph g)
        {

            // compute reverse postorder of reverse graph
            var dfs = new DepthFirstOrder(g.Reverse());

            // run DFS on G, using reverse postorder to guide calculation
            _marked = new bool[g.V];
            _id = new int[g.V];
            foreach (int v in dfs.ReversePost())
            {
                if (!_marked[v])
                {
                    Dfs(g, v);
                    _count++;
                }
            }

            // check that id[] gives strong components
            //assert check(G);
        }

        /// <summary>
        /// DFS on graph G
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(Digraph g, int v)
        {
            _marked[v] = true;
            _id[v] = _count;
            foreach (int w in g.Adj(v))
            {
                if (!_marked[w]) Dfs(g, w);
            }
        }

        /// <summary>
        /// Returns the number of strong components.
        /// </summary>
        /// <returns>the number of strong components</returns>
        public int Count()
        {
            return _count;
        }

        /// <summary>
        /// Are vertices <tt>v</tt> and <tt>w</tt> in the same strong component?
        /// </summary>
        /// <param name="v">v one vertex</param>
        /// <param name="w">w the other vertex</param>
        /// <returns><tt>true</tt> if vertices <tt>v</tt> and <tt>w</tt> are in the same strong component, and <tt>false</tt> otherwise</returns>
        public bool StronglyConnected(int v, int w)
        {
            return _id[v] == _id[w];
        }

        /// <summary>
        /// Returns the component id of the strong component containing vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the component id of the strong component containing vertex <tt>v</tt></returns>
        public int Id(int v)
        {
            return _id[v];
        }

        /// <summary>
        /// does the id[] array contain the strongly connected components?
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool Check(Digraph g)
        {
            var tc = new TransitiveClosure(g);
            for (var v = 0; v < g.V; v++)
            {
                for (var w = 0; w < g.V; w++)
                {
                    if (StronglyConnected(v, w) != (tc.Reachable(v, w) && tc.Reachable(w, v)))
                        return false;
                }
            }
            return true;
        }

    }
}
