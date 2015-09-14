using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>BreadthFirstPaths</tt> class represents a data type for finding
    /// shortest paths (number of edges) from a source vertex <em>s</em>
    /// (or a set of source vertices)
    /// to every other vertex in an undirected graph.
    /// <p>
    /// This implementation uses breadth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>,
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// It uses extra space (not including the graph) proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class BreadthFirstPaths
    {
        private const int INFINITY = int.MaxValue;
        private readonly bool[] _marked;  // marked[v] = is there an s-v path
        private readonly int[] _edgeTo;      // edgeTo[v] = previous edge on shortest s-v path
        private readonly int[] _distTo;      // distTo[v] = number of edges shortest s-v path

        /// <summary>
        /// Computes the shortest path between the source vertex <tt>s</tt>
        /// and every other vertex in the graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the graph</param>
        /// <param name="s">s the source vertex</param>
        public BreadthFirstPaths(Graph g, int s)
        {
            _marked = new bool[g.V];
            _distTo = new int[g.V];
            _edgeTo = new int[g.V];
            Bfs(g, s);

            //assert check(G, s);
        }

        /// <summary>
        /// Computes the shortest path between any one of the source vertices in <tt>sources</tt>
        /// and every other vertex in graph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the graph</param>
        /// <param name="sources">sources the source vertices</param>
        public BreadthFirstPaths(Graph g, IEnumerable<Integer> sources)
        {
            _marked = new bool[g.V];
            _distTo = new int[g.V];
            _edgeTo = new int[g.V];
            for (var v = 0; v < g.V; v++)
                _distTo[v] = INFINITY;
            Bfs(g, sources);
        }


        /// <summary>
        /// breadth-first search from a single source
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        private void Bfs(Graph g, int s)
        {
            var q = new Collections.Queue<Integer>();
            for (var v = 0; v < g.V; v++)
                _distTo[v] = INFINITY;
            _distTo[s] = 0;
            _marked[s] = true;
            q.Enqueue(s);

            while (!q.IsEmpty())
            {
                int v = q.Dequeue();
                foreach (int w in g.Adj(v))
                {
                    if (_marked[w]) continue;
                    _edgeTo[w] = v;
                    _distTo[w] = _distTo[v] + 1;
                    _marked[w] = true;
                    q.Enqueue(w);
                }
            }
        }

        /// <summary>
        /// breadth-first search from multiple sources
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sources"></param>
        private void Bfs(Graph g, IEnumerable<Integer> sources)
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
                    if (_marked[w]) continue;
                    _edgeTo[w] = v;
                    _distTo[w] = _distTo[v] + 1;
                    _marked[w] = true;
                    q.Enqueue(w);
                }
            }
        }

        /// <summary>
        /// Is there a path between the source vertex <tt>s</tt> (or sources) and vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns><tt>true</tt> if there is a path, and <tt>false</tt> otherwise</returns>
        public bool HasPathTo(int v)
        {
            return _marked[v];
        }

        /// <summary>
        /// Returns the number of edges in a shortest path between the source vertex <tt>s</tt>
        /// (or sources) and vertex <tt>v</tt>?
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the number of edges in a shortest path</returns>
        public int DistTo(int v)
        {
            return _distTo[v];
        }

        /// <summary>
        /// Returns a shortest path between the source vertex <tt>s</tt> (or sources)
        /// and <tt>v</tt>, or <tt>null</tt> if no such path.
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


        /// <summary>
        /// check optimality conditions for single source
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool Check(Graph g, int s)
        {

            // check that the distance of s = 0
            if (_distTo[s] != 0)
            {
                Console.WriteLine("distance of source " + s + " to itself = " + _distTo[s]);
                return false;
            }

            // check that for each edge v-w dist[w] <= dist[v] + 1
            // provided v is reachable from s
            for (var v = 0; v < g.V; v++)
            {
                foreach (int w in g.Adj(v))
                {
                    if (HasPathTo(v) != HasPathTo(w))
                    {
                        Console.WriteLine("edge " + v + "-" + w);
                        Console.WriteLine("hasPathTo(" + v + ") = " + HasPathTo(v));
                        Console.WriteLine("hasPathTo(" + w + ") = " + HasPathTo(w));
                        return false;
                    }
                    if (HasPathTo(v) && (_distTo[w] > _distTo[v] + 1))
                    {
                        Console.WriteLine("edge " + v + "-" + w);
                        Console.WriteLine("distTo[" + v + "] = " + _distTo[v]);
                        Console.WriteLine("distTo[" + w + "] = " + _distTo[w]);
                        return false;
                    }
                }
            }

            // check that v = edgeTo[w] satisfies distTo[w] + distTo[v] + 1
            // provided v is reachable from s
            for (var w = 0; w < g.V; w++)
            {
                if (!HasPathTo(w) || w == s) continue;
                var v = _edgeTo[w];
                if (_distTo[w] == _distTo[v] + 1) continue;
                Console.WriteLine("shortest path edge " + v + "-" + w);
                Console.WriteLine("distTo[" + v + "] = " + _distTo[v]);
                Console.WriteLine("distTo[" + w + "] = " + _distTo[w]);
                return false;
            }

            return true;
        }

    }
}
