using System;
using Algorithms.Core.Searching;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DigraphGenerator</tt> class provides static methods for creating
    /// various digraphs, including Erdos-Renyi random digraphs, random DAGs,
    /// random rooted trees, random rooted DAGs, random tournaments, path digraphs,
    /// cycle digraphs, and the complete digraph.
    /// </summary>
    public class DigraphGenerator
    {
        /// <summary>
        /// Returns a random simple digraph containing <tt>V</tt> vertices and <tt>E</tt> edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of vertices</param>
        /// <returns>a random simple digraph on <tt>V</tt> vertices, containing a total of <tt>E</tt> edges</returns>
        /// <exception cref="ArgumentException">if no such simple digraph exists</exception>
        public static Digraph Simple(int v, int e)
        {
            if (e > (long)v * (v - 1)) throw new ArgumentException("Too many edges");
            if (e < 0) throw new ArgumentException("Too few edges");
            var g = new Digraph(v);
            var set = new SET<EdgeD>();
            while (g.E < e)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var edge = new EdgeD(ve, we);
                if ((ve != we) && !set.Contains(edge))
                {
                    set.Add(edge);
                    g.AddEdge(ve, we);
                }
            }
            return g;
        }


        /// <summary>
        /// Returns a random simple digraph on <tt>V</tt> vertices, with an 
        /// edge between any two vertices with probability <tt>p</tt>. This is sometimes
        /// referred to as the Erdos-Renyi random digraph model.
        /// This implementations takes time propotional to V^2 (even if <tt>p</tt> is small).
        /// 
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="p">p the probability of choosing an edge</param>
        /// <returns>a random simple digraph on <tt>V</tt> vertices, with an edge between any two vertices with probability <tt>p</tt></returns>
        /// <exception cref="ArgumentException">if probability is not between 0 and 1</exception>
        public static Digraph Simple(int v, double p)
        {
            if (p < 0.0 || p > 1.0)
                throw new ArgumentException("Probability must be between 0 and 1");
            var g = new Digraph(v);
            for (var i = 0; i < v; i++)
                for (var j = 0; j < v; j++)
                    if (i != j)
                        if (StdRandom.Bernoulli(p))
                            g.AddEdge(i, j);
            return g;
        }

        /// <summary>
        /// Returns the complete digraph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <returns>the complete digraph on <tt>V</tt> vertices</returns>
        public static Digraph Complete(int v)
        {
            return Simple(v, v * (v - 1));
        }

        /// <summary>
        /// Returns a random simple DAG containing <tt>V</tt> vertices and <tt>E</tt> edges.
        /// Note: it is not uniformly selected at random among all such DAGs.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of vertices</param>
        /// <returns>a random simple DAG on <tt>V</tt> vertices, containing a total of <tt>E</tt> edges</returns>
        /// <exception cref="ArgumentException">if no such simple DAG exists</exception>
        public static Digraph Dag(int v, int e)
        {
            if (e > (long)v * (v - 1) / 2) throw new ArgumentException("Too many edges");
            if (e < 0) throw new ArgumentException("Too few edges");
            var g = new Digraph(v);
            var set = new SET<EdgeD>();
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            while (g.E < e)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var edge = new EdgeD(ve, we);
                if ((ve < we) && !set.Contains(edge))
                {
                    set.Add(edge);
                    g.AddEdge(vertices[ve], vertices[we]);
                }
            }
            return g;
        }


        /// <summary>
        /// Returns a random tournament digraph on <tt>V</tt> vertices. A tournament digraph
        /// is a DAG in which for every two vertices, there is one directed edge.
        /// A tournament is an oriented complete graph.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <returns>a random tournament digraph on <tt>V</tt> vertices</returns>
        public static Digraph Tournament(int v)
        {
            var g = new Digraph(v);
            for (var ve = 0; ve < g.V; ve++)
            {
                for (var we = ve + 1; we < g.V; we++)
                {
                    if (StdRandom.Bernoulli(0.5)) g.AddEdge(ve, we);
                    else g.AddEdge(we, ve);
                }
            }
            return g;
        }

        /// <summary>
        /// Returns a random rooted-in DAG on <tt>V</tt> vertices and <tt>E</tt> edges.
        /// A rooted in-tree is a DAG in which there is a single vertex
        /// reachable from every other vertex.
        /// The DAG returned is not chosen uniformly at random among all such DAGs.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <returns>a random rooted-in DAG on <tt>V</tt> vertices and <tt>E</tt> edges</returns>
        public static Digraph RootedInDag(int v, int e)
        {
            if (e > (long)v * (v - 1) / 2) throw new ArgumentException("Too many edges");
            if (e < v - 1) throw new ArgumentException("Too few edges");
            var g = new Digraph(v);
            var set = new SET<EdgeD>();

            // fix a topological order
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);

            // one edge pointing from each vertex, other than the root = vertices[V-1]
            for (var ve = 0; ve < v - 1; ve++)
            {
                var we = StdRandom.Uniform(ve + 1, v);
                var edge = new EdgeD(ve, we);
                set.Add(edge);
                g.AddEdge(vertices[ve], vertices[we]);
            }

            while (g.E < e)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var edge = new EdgeD(ve, we);
                if ((ve < we) && !set.Contains(edge))
                {
                    set.Add(edge);
                    g.AddEdge(vertices[ve], vertices[we]);
                }
            }
            return g;
        }

        /// <summary>
        /// Returns a random rooted-out DAG on <tt>V</tt> vertices and <tt>E</tt> edges.
        /// A rooted out-tree is a DAG in which every vertex is reachable from a
        /// single vertex.
        /// The DAG returned is not chosen uniformly at random among all such DAGs.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <returns>a random rooted-out DAG on <tt>V</tt> vertices and <tt>E</tt> edges</returns>
        public static Digraph RootedOutDag(int v, int e)
        {
            if (e > (long)v * (v - 1) / 2) throw new ArgumentException("Too many edges");
            if (e < v - 1) throw new ArgumentException("Too few edges");
            var g = new Digraph(v);
            var set = new SET<EdgeD>();

            // fix a topological order
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);

            // one edge pointing from each vertex, other than the root = vertices[V-1]
            for (var ve = 0; ve < v - 1; ve++)
            {
                var we = StdRandom.Uniform(ve + 1, v);
                var edge = new EdgeD(we, ve);
                set.Add(edge);
                g.AddEdge(vertices[we], vertices[ve]);
            }

            while (g.E < e)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var edge = new EdgeD(we, ve);
                if ((ve < we) && !set.Contains(edge))
                {
                    set.Add(edge);
                    g.AddEdge(vertices[we], vertices[ve]);
                }
            }
            return g;
        }

        /// <summary>
        /// Returns a random rooted-in tree on <tt>V</tt> vertices.
        /// A rooted in-tree is an oriented tree in which there is a single vertex
        /// reachable from every other vertex.
        /// The tree returned is not chosen uniformly at random among all such trees.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <returns>a random rooted-in tree on <tt>V</tt> vertices</returns>
        public static Digraph RootedInTree(int v)
        {
            return RootedInDag(v, v - 1);
        }

        /// <summary>
        /// Returns a random rooted-out tree on <tt>V</tt> vertices. A rooted out-tree
        /// is an oriented tree in which each vertex is reachable from a single vertex.
        /// It is also known as a <em>arborescence</em> or <em>branching</em>.
        /// The tree returned is not chosen uniformly at random among all such trees.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <returns>a random rooted-out tree on <tt>V</tt> vertices</returns>
        public static Digraph RootedOutTree(int v)
        {
            return RootedOutDag(v, v - 1);
        }

        /// <summary>
        /// Returns a path digraph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the path</param>
        /// <returns>a digraph that is a directed path on <tt>V</tt> vertices</returns>
        public static Digraph Path(int v)
        {
            var g = new Digraph(v);
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            for (var i = 0; i < v - 1; i++)
            {
                g.AddEdge(vertices[i], vertices[i + 1]);
            }
            return g;
        }

        /// <summary>
        /// Returns a complete binary tree digraph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the binary tree</param>
        /// <returns>a digraph that is a complete binary tree on <tt>V</tt> vertices</returns>
        public static Digraph BinaryTree(int v)
        {
            var g = new Digraph(v);
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            for (var i = 1; i < v; i++)
            {
                g.AddEdge(vertices[i], vertices[(i - 1) / 2]);
            }
            return g;
        }

        /// <summary>
        /// Returns a cycle digraph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the cycle</param>
        /// <returns>a digraph that is a directed cycle on <tt>V</tt> vertices</returns>
        public static Digraph Cycle(int v)
        {
            var g = new Digraph(v);
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            for (var i = 0; i < v - 1; i++)
            {
                g.AddEdge(vertices[i], vertices[i + 1]);
            }
            g.AddEdge(vertices[v - 1], vertices[0]);
            return g;
        }

        /// <summary>
        /// Returns an Eulerian cycle digraph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the cycle</param>
        /// <param name="e">E the number of edges in the cycle</param>
        /// <returns>a digraph that is a directed Eulerian cycle on <tt>V</tt> vertices and <tt>E</tt> edges</returns>
        /// <exception cref="ArgumentException">if either V &lt;= 0 or E &lt;= 0</exception>
        public static Digraph EulerianCycle(int v, int e)
        {
            if (e <= 0)
                throw new ArgumentException("An Eulerian cycle must have at least one edge");
            if (v <= 0)
                throw new ArgumentException("An Eulerian cycle must have at least one vertex");
            var g = new Digraph(v);
            var vertices = new int[e];
            for (var i = 0; i < e; i++)
                vertices[i] = StdRandom.Uniform(v);
            for (var i = 0; i < e - 1; i++)
            {
                g.AddEdge(vertices[i], vertices[i + 1]);
            }
            g.AddEdge(vertices[e - 1], vertices[0]);
            return g;
        }

        /// <summary>
        /// Returns an Eulerian path digraph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the path</param>
        /// <param name="e">E the number of edges in the path</param>
        /// <returns>a digraph that is a directed Eulerian path on <tt>V</tt> vertices and <tt>E</tt> edges</returns>
        /// <exception cref="ArgumentException">if either V &lt;= 0 or E &lt; 0</exception>
        public static Digraph EulerianPath(int v, int e)
        {
            if (e < 0)
                throw new ArgumentException("negative number of edges");
            if (v <= 0)
                throw new ArgumentException("An Eulerian path must have at least one vertex");
            var g = new Digraph(v);
            var vertices = new int[e + 1];
            for (var i = 0; i < e + 1; i++)
                vertices[i] = StdRandom.Uniform(v);
            for (var i = 0; i < e; i++)
            {
                g.AddEdge(vertices[i], vertices[i + 1]);
            }
            return g;
        }

        /// <summary>
        /// Returns a random simple digraph on <tt>V</tt> vertices, <tt>E</tt>
        /// edges and (at least) <tt>c</tt> strong components. The vertices are randomly
        /// assigned integer labels between <tt>0</tt> and <tt>c-1</tt> (corresponding to
        /// strong components). Then, a strong component is creates among the vertices
        /// with the same label. Next, random edges (either between two vertices with
        /// the same labels or from a vetex with a smaller label to a vertex with a
        /// larger label). The number of components will be equal to the number of
        /// distinct labels that are assigned to vertices.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <param name="c">c the (maximum) number of strong components</param>
        /// <returns>a random simple digraph on <tt>V</tt> vertices and <tt>E</tt> edges, with (at most) <tt>c</tt> strong components</returns>
        /// <exception cref="ArgumentException">if <tt>c</tt> is larger than <tt>V</tt></exception>
        public static Digraph Strong(int v, int e, int c)
        {
            if (c >= v || c <= 0)
                throw new ArgumentException("Number of components must be between 1 and V");
            if (e <= 2 * (v - c))
                throw new ArgumentException("Number of edges must be at least 2(V-c)");
            if (e > (long)v * (v - 1) / 2)
                throw new ArgumentException("Too many edges");

            // the digraph
            var g = new Digraph(v);

            // edges added to G (to avoid duplicate edges)
            var set = new SET<EdgeD>();

            var label = new int[v];
            for (var i = 0; i < v; i++)
                label[i] = StdRandom.Uniform(c);

            // make all vertices with label c a strong component by
            // combining a rooted in-tree and a rooted out-tree
            for (var i = 0; i < c; i++)
            {
                // how many vertices in component c
                var count = 0;
                for (var ii = 0; ii < g.V; ii++)
                {
                    if (label[ii] == i) count++;
                }

                // if (count == 0) System.err.println("less than desired number of strong components");

                var vertices = new int[count];
                var j = 0;
                for (var jj = 0; jj < v; jj++)
                {
                    if (label[jj] == i) vertices[j++] = jj;
                }
                StdRandom.Shuffle(vertices);

                // rooted-in tree with root = vertices[count-1]
                for (var ve = 0; ve < count - 1; ve++)
                {
                    var we = StdRandom.Uniform(ve + 1, count);
                    var edge = new EdgeD(we, ve);
                    set.Add(edge);
                    g.AddEdge(vertices[we], vertices[ve]);
                }

                // rooted-out tree with root = vertices[count-1]
                for (var ve = 0; ve < count - 1; ve++)
                {
                    var we = StdRandom.Uniform(ve + 1, count);
                    var edge = new EdgeD(ve, we);
                    set.Add(edge);
                    g.AddEdge(vertices[ve], vertices[we]);
                }
            }

            while (g.E < e)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var edge = new EdgeD(ve, we);
                if (!set.Contains(edge) && ve != we && label[ve] <= label[we])
                {
                    set.Add(edge);
                    g.AddEdge(ve, we);
                }
            }

            return g;
        }
    }
}
