using System;
using System.Collections.Generic;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DepthFirstOrder</tt> class represents a data type for 
    /// determining depth-first search ordering of the vertices in a digraph
    /// or edge-weighted digraph, including preorder, postorder, and reverse postorder.
    /// <p>
    /// This implementation uses depth-first search.
    /// The constructor takes time proportional to <em>V</em> + <em>E</em>
    /// (in the worst case),
    /// where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
    /// Afterwards, the <em>preorder</em>, <em>postorder</em>, and <em>reverse postorder</em>
    /// operation takes take time proportional to <em>V</em>.
    /// </p>
    /// </summary>
    public class DepthFirstOrder
    {
        private readonly bool[] _marked;          // marked[v] = has v been marked in dfs?
        private readonly int[] _pre;                 // pre[v]    = preorder  number of v
        private readonly int[] _post;                // post[v]   = postorder number of v
        private readonly Collections.Queue<Integer> _preorder;   // vertices in preorder
        private readonly Collections.Queue<Integer> _postorder;  // vertices in postorder
        private int _preCounter;            // counter or preorder numbering
        private int _postCounter;           // counter for postorder numbering

        /// <summary>
        /// Determines a depth-first order for the digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the digraph</param>
        public DepthFirstOrder(Digraph g)
        {
            _pre = new int[g.V];
            _post = new int[g.V];
            _postorder = new Collections.Queue<Integer>();
            _preorder = new Collections.Queue<Integer>();
            _marked = new bool[g.V];
            for (var v = 0; v < g.V; v++)
                if (!_marked[v]) Dfs(g, v);
        }

        /// <summary>
        /// Determines a depth-first order for the edge-weighted digraph <tt>G</tt>.
        /// </summary>
        /// <param name="g">g the edge-weighted digraph</param>
        public DepthFirstOrder(EdgeWeightedDigraph g)
        {
            _pre = new int[g.V];
            _post = new int[g.V];
            _postorder = new Collections.Queue<Integer>();
            _preorder = new Collections.Queue<Integer>();
            _marked = new bool[g.V];
            for (var v = 0; v < g.V; v++)
                if (!_marked[v]) Dfs(g, v);
        }

        /// <summary>
        /// run DFS in digraph G from vertex v and compute preorder/postorder
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(Digraph g, int v)
        {
            _marked[v] = true;
            _pre[v] = _preCounter++;
            _preorder.Enqueue(v);
            foreach (int w in g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(g, w);
                }
            }
            _postorder.Enqueue(v);
            _post[v] = _postCounter++;
        }

        /// <summary>
        /// run DFS in edge-weighted digraph G from vertex v and compute preorder/postorder
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void Dfs(EdgeWeightedDigraph g, int v)
        {
            _marked[v] = true;
            _pre[v] = _preCounter++;
            _preorder.Enqueue(v);
            foreach (var e in g.Adj(v))
            {
                var w = e.To();
                if (!_marked[w])
                {
                    Dfs(g, w);
                }
            }
            _postorder.Enqueue(v);
            _post[v] = _postCounter++;
        }

        /// <summary>
        /// Returns the preorder number of vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the preorder number of vertex <tt>v</tt></returns>
        public int Pre(int v)
        {
            return _pre[v];
        }

        /// <summary>
        /// Returns the postorder number of vertex <tt>v</tt>.
        /// </summary>
        /// <param name="v">v the vertex</param>
        /// <returns>the postorder number of vertex <tt>v</tt></returns>
        public int Post(int v)
        {
            return _post[v];
        }

        /// <summary>
        /// Returns the vertices in postorder.
        /// </summary>
        /// <returns>the vertices in postorder, as an iterable of vertices</returns>
        public IEnumerable<Integer> Post()
        {
            return _postorder;
        }

        /// <summary>
        /// Returns the vertices in preorder.
        /// </summary>
        /// <returns>the vertices in preorder, as an iterable of vertices</returns>
        public IEnumerable<Integer> Pre()
        {
            return _preorder;
        }

        /// <summary>
        /// Returns the vertices in reverse postorder.
        /// </summary>
        /// <returns>the vertices in reverse postorder, as an iterable of vertices</returns>
        public IEnumerable<Integer> ReversePost()
        {
            var reverse = new Collections.Stack<Integer>();
            foreach (int v in _postorder)
                reverse.Push(v);
            return reverse;
        }


        /// <summary>
        /// check that pre() and post() are consistent with pre(v) and post(v)
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool Check(Digraph g)
        {

            // check that post(v) is consistent with post()
            var r = 0;
            foreach (int v in Post())
            {
                if (Post(v) != r)
                {
                    Console.WriteLine("post(v) and post() inconsistent");
                    return false;
                }
                r++;
            }

            // check that pre(v) is consistent with pre()
            r = 0;
            foreach (int v in Pre())
            {
                if (Pre(v) != r)
                {
                    Console.WriteLine("pre(v) and pre() inconsistent");
                    return false;
                }
                r++;
            }
            return true;
        }
    }
}
