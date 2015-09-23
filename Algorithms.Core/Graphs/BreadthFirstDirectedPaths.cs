using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{

    /// <summary>
    /// The <tt>BreadthDirectedFirstPaths</tt> class represents a data type for finding
    /// shortest paths (number of edges) from a source vertex <em>s</em>
    /// (or set of source vertices) to every other vertex in the digraph.
    /// <p>
    /// This implementation uses breadth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>,
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// It uses extra space (not including the digraph) proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class BreadthFirstDirectedPaths
    {
        private const int INFINITY = int.MaxValue;
        private readonly bool[] _marked;  // marked[v] = is there an s->v path?
        private readonly int[] _edgeTo;      // edgeTo[v] = last edge on shortest s->v path
        private readonly int[] _distTo;      // distTo[v] = length of shortest s->v path

        /// <summary>
        /// Computes the shortest path from <tt>s</tt> and every other vertex in graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        /// <param name="s">s the source vertex</param>
        public BreadthFirstDirectedPaths(Digraph g, int s)
        {
            _marked = new bool[g.V];
            _distTo = new int[g.V];
            _edgeTo = new int[g.V];
            for (var v = 0; v < g.V; v++)
                _distTo[v] = INFINITY;
            Bfs(g, s);
        }

        /// <summary>
        /// Computes the shortest path from any one of the source vertices in <tt>sources</tt>
        /// to every other vertex in graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        /// <param name="sources">sources the source vertices</param>
        public BreadthFirstDirectedPaths(Digraph g, IEnumerable<Integer> sources)
        {
            _marked = new bool[g.V];
            _distTo = new int[g.V];
            _edgeTo = new int[g.V];
            for (var v = 0; v < g.V; v++)
                _distTo[v] = INFINITY;
            Bfs(g, sources);
        }

        /// <summary>
        /// BFS from single source
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        private void Bfs(Digraph g, int s)
        {
            var q = new Collections.Queue<Integer>();
            _marked[s] = true;
            _distTo[s] = 0;
            q.Enqueue(s);
            while (!q.IsEmpty())
            {
                int v = q.Dequeue();
                foreach (int w in g.Adj(v))
                {
                    if (!_marked[w])
                    {
                        _edgeTo[w] = v;
                        _distTo[w] = _distTo[v] + 1;
                        _marked[w] = true;
                        q.Enqueue(w);
                    }
                }
            }
        }

        /// <summary>
        /// BFS from multiple sources
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sources"></param>
        private void Bfs(Digraph g, IEnumerable<Integer> sources)
        {
            var q = new Collections.Queue<Integer>();
            foreach (int s in sources)
            {
                _marked[s] = true;
                _distTo[s] = 0;
                q.Enqueue(s);
            }
            while (!q.IsEmpty())
            {
                int v = q.Dequeue();
                foreach (int w in g.Adj(v))
                {
                    if (!_marked[w])
                    {
                        _edgeTo[w] = v;
                        _distTo[w] = _distTo[v] + 1;
                        _marked[w] = true;
                        q.Enqueue(w);
                    }
                }
            }
        }

        /// <summary>
        /// Is there a directed path from the source <tt>s</tt> (or sources) to vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns><tt>true</tt> if there is a directed path, <tt>false</tt> otherwise</returns>
        public bool HasPathTo(int v)
        {
            return _marked[v];
        }

        /// <summary>
        /// Returns the number of edges in a shortest path from the source <tt>s</tt>
        /// (or sources) to vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the number of edges in a shortest path</returns>
        public int DistTo(int v)
        {
            return _distTo[v];
        }

        /// <summary>
        /// Returns a shortest path from <tt>s</tt> (or sources) to <tt>v</tt>, or
        /// <tt>null</tt> if no such path.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the sequence of vertices on a shortest path, as an Iterable</returns>
        public IEnumerable<Integer> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Collections.Stack<Integer>();
            int x;
            for (x = v; _distTo[x] != 0; x = _edgeTo[x])
                path.Push(x);
            path.Push(x);
            return path;
        }
    }
}
