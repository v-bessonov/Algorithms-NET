using System;
using System.Collections.Generic;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EdgeWeightedDirectedCycle</tt> class represents a data type for
    /// determining whether an edge-weighted digraph has a directed cycle.
    /// The <em>hasCycle</em> operation determines whether the edge-weighted
    /// digraph has a directed cycle and, if so, the <em>cycle</em> operation
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
    public class EdgeWeightedDirectedCycle
    {
        private readonly bool[] _marked;             // marked[v] = has vertex v been marked?
        private readonly DirectedEdge[] _edgeTo;        // edgeTo[v] = previous edge on path to v
        private readonly bool[] _onStack;            // onStack[v] = is vertex on the stack?
        private Collections.Stack<DirectedEdge> _cycle;    // directed cycle (or null if no such cycle)

        /// <summary>
        /// Determines whether the edge-weighted digraph <tt>G</tt> has a directed cycle and,
        /// if so, finds such a cycle.
        /// </summary>
        /// <param name="g">g the edge-weighted digraph</param>
        public EdgeWeightedDirectedCycle(EdgeWeightedDigraph g)
        {
            _marked = new bool[g.V];
            _onStack = new bool[g.V];
            _edgeTo = new DirectedEdge[g.V];
            for (var v = 0; v < g.V; v++)
                if (!_marked[v]) Dfs(g, v);

            // check that digraph has a cycle
            //assert check(G);
        }

        /// <summary>
        /// check that algorithm computes either the topological order or finds a directed cycle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(EdgeWeightedDigraph g, int v)
        {
            _onStack[v] = true;
            _marked[v] = true;
            foreach (var e in g.Adj(v))
            {
                var w = e.To();

                // short circuit if directed cycle found
                if (_cycle != null) return;

                //found new vertex, so recur
                if (!_marked[w])
                {
                    _edgeTo[w] = e;
                    Dfs(g, w);
                }

                // trace back directed cycle
                else if (_onStack[w])
                {
                    TraceBackDirectedCycle(e, w);
                    return;
                }
            }

            _onStack[v] = false;
        }

        private void TraceBackDirectedCycle(DirectedEdge e, int w)
        {
            _cycle = new Collections.Stack<DirectedEdge>();
            while (e.From() != w)
            {
                _cycle.Push(e);
                e = _edgeTo[e.From()];
            }
            _cycle.Push(e);
        }

        /// <summary>
        /// Does the edge-weighted digraph have a directed cycle?
        /// </summary>
        /// <returns><tt>true</tt> if the edge-weighted digraph has a directed cycle, <tt>false</tt> otherwise</returns>
        public bool HasCycle()
        {
            return _cycle != null;
        }

        /// <summary>
        /// Returns a directed cycle if the edge-weighted digraph has a directed cycle,
        /// and <tt>null</tt> otherwise.
        /// </summary>
        /// <returns>a directed cycle (as an iterable) if the edge-weighted digraph has a directed cycle, and <tt>null</tt> otherwise</returns>
        public IEnumerable<DirectedEdge> Cycle()
        {
            return _cycle;
        }


        /// <summary>
        /// certify that digraph is either acyclic or has a directed cycle
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool Check(EdgeWeightedDigraph g)
        {

            // edge-weighted digraph is cyclic
            if (HasCycle())
            {
                // verify cycle
                DirectedEdge first = null, last = null;
                foreach (var e in Cycle())
                {
                    if (first == null) first = e;
                    if (last != null)
                    {
                        if (last.To() != e.From())
                        {
                            Console.Error.WriteLine($"cycle edges {last} and {e} not incident{Environment.NewLine}");
                            return false;
                        }
                    }
                    last = e;
                }

                if (last != null && last.To() != first.From())
                {
                    Console.Error.WriteLine($"cycle edges {last} and {first} not incident{Environment.NewLine}");
                    return false;
                }
            }

            return true;
        }
    }
}
