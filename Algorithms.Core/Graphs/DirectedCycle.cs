using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DirectedCycle</tt> class represents a data type for
    /// determining whether a digraph has a directed cycle.
    /// The <em>hasCycle</em> operation determines whether the digraph has
    /// a directed cycle and, and of so, the <em>cycle</em> operation
    /// returns one.
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
    public class DirectedCycle
    {
        private readonly bool[] _marked;        // marked[v] = has vertex v been marked?
        private readonly int[] _edgeTo;            // edgeTo[v] = previous vertex on path to v
        private readonly bool[] _onStack;       // onStack[v] = is vertex on the stack?
        private Collections.Stack<Integer> _cycle;    // directed cycle (or null if no such cycle)

        /// <summary>
        /// Determines whether the digraph <tt>G</tt> has a directed cycle and, if so,
        /// finds such a cycle.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public DirectedCycle(Digraph g)
        {
            _marked = new bool[g.V];
            _onStack = new bool[g.V];
            _edgeTo = new int[g.V];
            for (var v = 0; v < g.V; v++)
                if (!_marked[v] && _cycle == null) Dfs(g, v);
        }

        /// <summary>
        /// check that algorithm computes either the topological order or finds a directed cycle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(Digraph g, int v)
        {
            _onStack[v] = true;
            _marked[v] = true;
            foreach (int w in g.Adj(v))
            {
                // short circuit if directed cycle found
                if (_cycle != null) return;

                //found new vertex, so recur
                if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    Dfs(g, w);
                }

                // trace back directed cycle
                else if (_onStack[w])
                {
                    _cycle = new Collections.Stack<Integer>();
                    for (var x = v; x != w; x = _edgeTo[x])
                    {
                        _cycle.Push(x);
                    }
                    _cycle.Push(w);
                    _cycle.Push(v);
                    //assert check();
                }
            }
            _onStack[v] = false;
        }

        /// <summary>
        /// Does the digraph have a directed cycle?
        /// </summary>
        /// <returns><tt>true</tt> if the digraph has a directed cycle, <tt>false</tt> otherwise</returns>
        public bool HasCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// Returns a directed cycle if the digraph has a directed cycle, and <tt>null</tt> otherwise.
        /// </summary>
        /// <returns>a directed cycle (as an iterable) if the digraph has a directed cycle, and <tt>null</tt> otherwise</returns>
        public IEnumerable<Integer> Cycle()
        {
            return _cycle;
        }


        /// <summary>
        /// certify that digraph has a directed cycle if it reports one
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (!HasCycle()) return true;
            // verify cycle
            int first = -1, last = -1;
            foreach (int v in Cycle())
            {
                if (first == -1) first = v;
                last = v;
            }
            if (first != last)
            {
                Console.Error.WriteLine($"cycle begins with {first} and ends with {last}{Environment.NewLine}");
                return false;
            }


            return true;
        }
    }
}
