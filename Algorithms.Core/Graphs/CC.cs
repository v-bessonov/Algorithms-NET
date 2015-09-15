namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>CC</tt> class represents a data type for 
    /// determining the connected components in an undirected graph.
    /// The <em>id</em> operation determines in which connected component
    /// a given vertex lies; the <em>connected</em> operation
    /// determines whether two vertices are in the same connected component;
    /// the <em>count</em> operation determines the number of connected
    /// components; and the <em>size</em> operation determines the number
    /// of vertices in the connect component containing a given vertex.
    /// 
    /// The <em>component identifier</em> of a connected component is one of the
    /// vertices in the connected component: two vertices have the same component
    /// identifier if and only if they are in the same connected component.
    /// 
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>id</em>, <em>count</em>, <em>connected</em>,
    /// and <em>size</em> operations take constant time.
    /// </p>
    /// </summary>
    public class CC
    {
        private readonly bool[] _marked;   // marked[v] = has vertex v been marked?
        private readonly int[] _id;           // id[v] = id of connected component containing v
        private readonly int[] _size;         // size[id] = number of vertices in given component
        private readonly int _count;          // number of connected components

        /// <summary>
        /// Computes the connected components of the undirected graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the undirected graph</param>
        public CC(Graph g)
        {
            _marked = new bool[g.V];
            _id = new int[g.V];
            _size = new int[g.V];
            for (var v = 0; v < g.V; v++)
            {
                if (_marked[v]) continue;
                Dfs(g, v);
                _count++;
            }
        }

        /// <summary>
        /// depth-first search
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(Graph g, int v)
        {
            _marked[v] = true;
            _id[v] = _count;
            _size[_count]++;
            foreach (int w in g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(g, w);
                }
            }
        }

        /// <summary>
        /// Returns the component id of the connected component containing vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the component id of the connected component containing vertex <tt>v</tt></returns>
        public int Id(int v)
        {
            return _id[v];
        }

        /// <summary>
        /// Returns the number of vertices in the connected component containing vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the number of vertices in the connected component containing vertex <tt>v</tt></returns>
        public int Size(int v)
        {
            return _size[_id[v]];
        }

        /// <summary>
        /// Returns the number of connected components in the graph <tt>G</tt>.
        /// </summary>
        /// <returns>the number of connected components in the graph <tt>G</tt></returns>
        public int Count()
        {
            return _count;
        }

        /// <summary>
        /// Returns true if vertices <tt>v</tt> and <tt>w</tt> are in the same
        /// connected component.
        /// </summary>
        /// <param name="v">v one vertex</param>
        /// <param name="w">w the other vertex</param>
        /// <returns><tt>true</tt> if vertices <tt>v</tt> and <tt>w</tt> are in the same connected component; <tt>false</tt> otherwise</returns>
        public bool Connected(int v, int w)
        {
            return Id(v) == Id(w);
        }


    }
}
