using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>Cycle</tt> class represents a data type for 
    /// determining whether an undirected graph has a cycle.
    /// The <em>hasCycle</em> operation determines whether the graph has
    /// a cycle and, if so, the <em>cycle</em> operation returns one.
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>hasCycle</em> operation takes constant time;
    /// the <em>cycle</em> operation takes time proportional
    /// to the length of the cycle.
    /// </p>
    /// </summary>
    public class Cycle
    {
        private bool[] _marked;
        private readonly int[] _edgeTo;
        private Collections.Stack<Integer> _cycle;

        /// <summary>
        /// Determines whether the undirected graph <tt>G</tt> has a cycle and,
        /// if so, finds such a cycle.
        /// </summary>
        /// <param name="g">g the undirected graph</param>
        public Cycle(Graph g)
        {
            if (HasSelfLoop(g)) return;
            if (HasParallelEdges(g)) return;
            _marked = new bool[g.V];
            _edgeTo = new int[g.V];
            for (var v = 0; v < g.V; v++)
                if (!_marked[v])
                    Dfs(g, -1, v);
        }


        /// <summary>
        /// does this graph have a self loop?
        /// side effect: initialize cycle to be self loop
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private bool HasSelfLoop(Graph g)
        {
            for (var v = 0; v < g.V; v++)
            {
                foreach (int w in g.Adj(v))
                {
                    if (v != w) continue;
                    _cycle = new Collections.Stack<Integer>();
                    _cycle.Push(v);
                    _cycle.Push(v);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// does this graph have two parallel edges?
        /// side effect: initialize cycle to be two parallel edges
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private bool HasParallelEdges(Graph g)
        {
            _marked = new bool[g.V];

            for (var v = 0; v < g.V; v++)
            {

                // check for parallel edges incident to v
                foreach (int w in g.Adj(v))
                {
                    if (_marked[w])
                    {
                        _cycle = new Collections.Stack<Integer>();
                        _cycle.Push(v);
                        _cycle.Push(w);
                        _cycle.Push(v);
                        return true;
                    }
                    _marked[w] = true;
                }

                // reset so marked[v] = false for all v
                foreach (int w in g.Adj(v))
                {
                    _marked[w] = false;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the graph <tt>G</tt> has a cycle.
        /// </summary>
        /// <returns><tt>true</tt> if the graph has a cycle; <tt>false</tt> otherwise</returns>
        public bool HasCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// Returns a cycle in the graph <tt>G</tt>
        /// </summary>
        /// <returns>a cycle if the graph <tt>G</tt> has a cycle, and <tt>null</tt> otherwise</returns>
        public IEnumerable<Integer> CycleIterator()
        {
            return _cycle;
        }

        private void Dfs(Graph g, int u, int v)
        {
            _marked[v] = true;
            foreach (int w in g.Adj(v))
            {

                // short circuit if cycle already found
                if (_cycle != null) return;

                if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    Dfs(g, v, w);
                }

                // check for cycle (but disregard reverse of edge leading to v)
                else if (w != u)
                {
                    _cycle = new Collections.Stack<Integer>();
                    for (var x = v; x != w; x = _edgeTo[x])
                    {
                        _cycle.Push(x);
                    }
                    _cycle.Push(w);
                    _cycle.Push(v);
                }
            }
        }
    }
}
