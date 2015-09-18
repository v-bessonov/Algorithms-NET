using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>BipartiteX</tt> class represents a data type for 
    /// determining whether an undirected graph is bipartite or whether
    /// it has an odd-length cycle.
    /// The <em>isBipartite</em> operation determines whether the graph is
    /// bipartite. If so, the <em>color</em> operation determines a
    /// bipartition; if not, the <em>oddCycle</em> operation determines a
    /// cycle with an odd number of edges.
    /// <p>
    /// This implementation uses breadth-first search and is nonrecursive.
    /// TThe constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>isBipartite</em> and <em>color</em> operations
    /// take constant time; the <em>oddCycle</em> operation takes time proportional
    /// to the length of the cycle.
    /// </p>
    /// </summary>
    public class BipartiteX
    {
        private const bool WHITE = false;
        private static bool BLACK = true;

        private bool _isBipartite;   // is the graph bipartite?
        private readonly bool[] _color;       // color[v] gives vertices on one side of bipartition
        private readonly bool[] _marked;      // marked[v] = true if v has been visited in DFS
        private readonly int[] _edgeTo;          // edgeTo[v] = last edge on path to v
        private Collections.Queue<Integer> _cycle;  // odd-length cycle

        /// <summary>
        /// Determines whether an undirected graph is bipartite and finds either a
        /// bipartition or an odd-length cycle.
        /// </summary>
        /// <param name="g">g the graph</param>
        public BipartiteX(Graph g)
        {
            _isBipartite = true;
            _color = new bool[g.V];
            _marked = new bool[g.V];
            _edgeTo = new int[g.V];

            for (var v = 0; v < g.V && _isBipartite; v++)
            {
                if (!_marked[v])
                {
                    Bfs(g, v);
                }
            }
            //assert check(G);
        }


        private void Bfs(Graph g, int s)
        {
            var q = new Collections.Queue<Integer>();
            _color[s] = WHITE;
            _marked[s] = true;
            q.Enqueue(s);

            while (!q.IsEmpty())
            {
                int v = q.Dequeue();
                foreach (int w in g.Adj(v))
                {
                    if (!_marked[w])
                    {
                        _marked[w] = true;
                        _edgeTo[w] = v;
                        _color[w] = !_color[v];
                        q.Enqueue(w);
                    }
                    else if (_color[w] == _color[v])
                    {
                        _isBipartite = false;

                        // to form odd cycle, consider s-v path and s-w path
                        // and let x be closest node to v and w common to two paths
                        // then (w-x path) + (x-v path) + (edge v-w) is an odd-length cycle
                        // Note: distTo[v] == distTo[w];
                        _cycle = new Collections.Queue<Integer>();
                        var stack = new Collections.Stack<Integer>();
                        int x = v, y = w;
                        while (x != y)
                        {
                            stack.Push(x);
                            _cycle.Enqueue(y);
                            x = _edgeTo[x];
                            y = _edgeTo[y];
                        }
                        stack.Push(x);
                        while (!stack.IsEmpty())
                            _cycle.Enqueue(stack.Pop());
                        _cycle.Enqueue(w);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the graph is bipartite.
        /// </summary>
        /// <returns><tt>true</tt> if the graph is bipartite; <tt>false</tt> otherwise</returns>
        public bool IsBipartite()
        {
            return _isBipartite;
        }

        /// <summary>
        /// Returns the side of the bipartite that vertex <tt>v</tt> is on.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the side of the bipartition that vertex <tt>v</tt> is on; two vertices are in the same side of the bipartition if and only if they have the same color</returns>
        /// <exception cref="ArgumentException">unless <tt>0 &le; v &lt; V</tt> </exception>
        /// <exception cref="InvalidOperationException">if this method is called when the graph is not bipartite</exception>
        public bool Color(int v)
        {
            if (!_isBipartite)
                throw new InvalidOperationException("Graph is not bipartite");
            return _color[v];
        }


        /// <summary>
        /// Returns an odd-length cycle if the graph is not bipartite, and
        /// <tt>null</tt> otherwise.
        /// </summary>
        /// <returns>an odd-length cycle if the graph is not bipartite  (and hence has an odd-length cycle), and <tt>null</tt> otherwise</returns>
        public IEnumerable<Integer> OddCycle()
        {
            return _cycle;
        }

        private bool Check(Graph g)
        {
            // graph is bipartite
            if (_isBipartite)
            {
                for (var v = 0; v < g.V; v++)
                {
                    foreach (int w in g.Adj(v))
                    {
                        if (_color[v] != _color[w]) continue;
                        Console.Error.WriteLine($"edge {v}-{w} with {v} and {w} in same side of bipartition\n");
                        return false;
                    }
                }
            }

            // graph has an odd-length cycle
            else
            {
                // verify cycle
                int first = -1, last = -1;
                foreach (int v in OddCycle())
                {
                    if (first == -1) first = v;
                    last = v;
                }
                if (first != last)
                {
                    Console.Error.WriteLine($"cycle begins with {first} and ends with {last}{Environment.NewLine}");
                    return false;
                }
            }
            return true;
        }

    }
}
