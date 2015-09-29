using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Graphs
{
    public class DepthFirstOrder
    {
        //private bool[] marked;          // marked[v] = has v been marked in dfs?
        //private int[] pre;                 // pre[v]    = preorder  number of v
        //private int[] post;                // post[v]   = postorder number of v
        //private Collections.Queue<Integer> preorder;   // vertices in preorder
        //private Collections.Queue<Integer> postorder;  // vertices in postorder
        //private int preCounter;            // counter or preorder numbering
        //private int postCounter;           // counter for postorder numbering

        ///**
        // * Determines a depth-first order for the digraph <tt>G</tt>.
        // * @param G the digraph
        // */
        //public DepthFirstOrder(Digraph G)
        //{
        //    pre = new int[G.V()];
        //    post = new int[G.V()];
        //    postorder = new Collections.Queue<Integer>();
        //    preorder = new Collections.Queue<Integer>();
        //    marked = new bool[G.V()];
        //    for (int v = 0; v < G.V(); v++)
        //        if (!marked[v]) dfs(G, v);
        //}

        ///**
        // * Determines a depth-first order for the edge-weighted digraph <tt>G</tt>.
        // * @param G the edge-weighted digraph
        // */
        //public DepthFirstOrder(EdgeWeightedDigraph G)
        //{
        //    pre = new int[G.V()];
        //    post = new int[G.V()];
        //    postorder = new Collections.Queue<Integer>();
        //    preorder = new Collections.Queue<Integer>();
        //    marked = new bool[G.V()];
        //    for (int v = 0; v < G.V(); v++)
        //        if (!marked[v]) dfs(G, v);
        //}

        //// run DFS in digraph G from vertex v and compute preorder/postorder
        //private void dfs(Digraph G, int v)
        //{
        //    marked[v] = true;
        //    pre[v] = preCounter++;
        //    preorder.enCollections.Queue(v);
        //    for (int w : G.adj(v))
        //    {
        //        if (!marked[w])
        //        {
        //            dfs(G, w);
        //        }
        //    }
        //    postorder.enCollections.Queue(v);
        //    post[v] = postCounter++;
        //}

        //// run DFS in edge-weighted digraph G from vertex v and compute preorder/postorder
        //private void dfs(EdgeWeightedDigraph G, int v)
        //{
        //    marked[v] = true;
        //    pre[v] = preCounter++;
        //    preorder.enCollections.Queue(v);
        //    for (DirectedEdge e : G.adj(v))
        //    {
        //        int w = e.to();
        //        if (!marked[w])
        //        {
        //            dfs(G, w);
        //        }
        //    }
        //    postorder.enCollections.Queue(v);
        //    post[v] = postCounter++;
        //}

        ///**
        // * Returns the preorder number of vertex <tt>v</tt>.
        // * @param v the vertex
        // * @return the preorder number of vertex <tt>v</tt>
        // */
        //public int pre(int v)
        //{
        //    return pre[v];
        //}

        ///**
        // * Returns the postorder number of vertex <tt>v</tt>.
        // * @param v the vertex
        // * @return the postorder number of vertex <tt>v</tt>
        // */
        //public int post(int v)
        //{
        //    return post[v];
        //}

        ///**
        // * Returns the vertices in postorder.
        // * @return the vertices in postorder, as an iterable of vertices
        // */
        //public Iterable<Integer> post()
        //{
        //    return postorder;
        //}

        ///**
        // * Returns the vertices in preorder.
        // * @return the vertices in preorder, as an iterable of vertices
        // */
        //public Iterable<Integer> pre()
        //{
        //    return preorder;
        //}

        ///**
        // * Returns the vertices in reverse postorder.
        // * @return the vertices in reverse postorder, as an iterable of vertices
        // */
        //public Iterable<Integer> reversePost()
        //{
        //    Stack<Integer> reverse = new Stack<Integer>();
        //    for (int v : postorder)
        //        reverse.push(v);
        //    return reverse;
        //}


        //// check that pre() and post() are consistent with pre(v) and post(v)
        //private bool check(Digraph G)
        //{

        //    // check that post(v) is consistent with post()
        //    int r = 0;
        //    for (int v : post())
        //    {
        //        if (post(v) != r)
        //        {
        //            StdOut.println("post(v) and post() inconsistent");
        //            return false;
        //        }
        //        r++;
        //    }

        //    // check that pre(v) is consistent with pre()
        //    r = 0;
        //    for (int v : pre())
        //    {
        //        if (pre(v) != r)
        //        {
        //            StdOut.println("pre(v) and pre() inconsistent");
        //            return false;
        //        }
        //        r++;
        //    }


        //    return true;
        //}
    }
}
