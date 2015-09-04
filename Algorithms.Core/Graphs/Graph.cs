using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Core.Collections;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    public class Graph
    {
        private readonly string _newline = Environment.NewLine;

        public int V { get; }
        public int E { get; set; }
        private readonly Bag<Integer>[] _adj;

        /**
     * Initializes an empty graph with <tt>V</tt> vertices and 0 edges.
     * param V the number of vertices
     *
     * @param  V number of vertices
     * @throws IllegalArgumentException if <tt>V</tt> < 0
     */
        public Graph(int v)
        {
            if (V < 0) throw new ArgumentException("Number of vertices must be nonnegative");
            V = v;
            E = 0;
            _adj = new Bag<Integer>[V];
            for (var i = 0; i < V; i++)
            {
                _adj[v] = new Bag<Integer>();
            }
        }

        /**  
    * Initializes a graph from an input stream.
    * The format is the number of vertices <em>V</em>,
    * followed by the number of edges <em>E</em>,
    * followed by <em>E</em> pairs of vertices, with each entry separated by whitespace.
    *
    * @param  in the input stream
    * @throws IndexOutOfBoundsException if the endpoints of any edge are not in prescribed range
    * @throws IllegalArgumentException if the number of vertices or edges is negative
    */
        public Graph(int v, int e, IEnumerable<Edge> edges) : this(v)
        {
            edges = edges.ToList();
            if (e < 0) throw new ArgumentException("Number of edges must be nonnegative");
            if (e != edges.Count()) throw new ArgumentException("Number of edges doesn't match");
            foreach (var edge in edges)
            {
                var vv = edge.V;
                var w = edge.W;
                AddEdge(vv, w);
            }

        }

        /**
     * Initializes a new graph that is a deep copy of <tt>G</tt>.
     *
     * @param  G the graph to copy
     */
        public Graph(Graph g) :this(g.V)
        {
            E = g.E;
            for (var v = 0; v < g.V; v++)
            {
                // reverse so that adjacency list is in same order as original
                var reverse = new Collections.Stack<Integer>();
                foreach (int w in g._adj[v])
                {
                    reverse.Push(w);
                }
                foreach (int w in reverse)
                {
                    _adj[v].Add(w);
                }
            }
        }

        // throw an IndexOutOfBoundsException unless 0 <= v < V
        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new IndexOutOfRangeException("vertex " + v + " is not between 0 and " + (V - 1));
        }

        /**
         * Adds the undirected edge v-w to this graph.
         *
         * @param  v one vertex in the edge
         * @param  w the other vertex in the edge
         * @throws IndexOutOfBoundsException unless both 0 <= v < V and 0 <= w < V
         */
        public void AddEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            E++;
            _adj[v].Add(w);
            _adj[w].Add(v);
        }

        /**
    * Returns the vertices adjacent to vertex <tt>v</tt>.
    *
    * @param  v the vertex
    * @return the vertices adjacent to vertex <tt>v</tt>, as an iterable
    * @throws IndexOutOfBoundsException unless 0 <= v < V
    */
        public IEnumerable<Integer> Adj(int v)
        {
            ValidateVertex(v);
            return _adj[v];
        }

        /**
         * Returns the degree of vertex <tt>v</tt>.
         *
         * @param  v the vertex
         * @return the degree of vertex <tt>v</tt>
         * @throws IndexOutOfBoundsException unless 0 <= v < V
         */
        public int Degree(int v)
        {
            ValidateVertex(v);
            return _adj[v].Size();
        }

        /**
     * Returns a string representation of this graph.
     *
     * @return the number of vertices <em>V</em>, followed by the number of edges <em>E</em>,
     *         followed by the <em>V</em> adjacency lists
     */
        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"{V}  vertices, {E} edges {_newline}");
            for (var v = 0; v < V; v++)
            {
                s.Append($"{v}: ");
                foreach (int w in _adj[v])
                {
                    s.Append($"{w} ");
                }
                s.Append(_newline);
            }
            return s.ToString();
        }

    }
}
