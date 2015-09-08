using System;
using Algorithms.Core.Helpers;
using Algorithms.Core.Searching;
using Algorithms.Core.Sorting;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>GraphGenerator</tt> class provides static methods for creating
    /// various graphs, including Erdos-Renyi random graphs, random bipartite
    /// graphs, random k-regular graphs, and random rooted trees.
    /// </summary>
    public class GraphGenerator
    {
        /// <summary>
        /// Returns a random simple graph containing <tt>V</tt> vertices and <tt>E</tt> edges.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="e">E the number of edges</param>
        /// <returns> random simple graph on <tt>V</tt> vertices, containing a total of <tt>E</tt> edges</returns>
        /// <exception cref="ArgumentException">if no such simple graph exists</exception>
        public static Graph Simple(int v, int e)
        {
            if (e > (long)v * (v - 1) / 2) throw new ArgumentException("Too many edges");
            if (e < 0) throw new ArgumentException("Too few edges");
            var g = new Graph(v);
            var set = new SET<EdgeU>();
            while (g.E < e)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                var edge = new EdgeU(ve, we);
                if ((ve == we) || set.Contains(edge)) continue;
                set.Add(edge);
                g.AddEdge(ve, we);
            }
            return g;
        }


        /// <summary>
        /// Returns a random simple graph on <tt>V</tt> vertices, with an 
        /// edge between any two vertices with probability <tt>p</tt>. This is sometimes
        /// referred to as the Erdos-Renyi random graph model.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <param name="p">p the probability of choosing an edge</param>
        /// <returns>a random simple graph on <tt>V</tt> vertices, with an edge between any two vertices with probability <tt>p</tt></returns>
        /// <exception cref="ArgumentException">if probability is not between 0 and 1</exception>
        public static Graph Simple(int v, double p)
        {
            if (p < 0.0 || p > 1.0)
                throw new ArgumentException("Probability must be between 0 and 1");
            var g = new Graph(v);
            for (var vi = 0; vi < v; vi++)
                for (var wi = vi + 1; wi < v; wi++)
                    if (StdRandom.Bernoulli(p))
                        g.AddEdge(vi, wi);
            return g;
        }

        /// <summary>
        /// Returns the complete graph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices</param>
        /// <returns>the complete graph on <tt>V</tt> vertices</returns>
        public static Graph Complete(int v)
        {
            return Simple(v, 1.0);
        }

        /// <summary>
        /// Returns a random simple bipartite graph on <tt>V1</tt> and <tt>V2</tt> vertices
        /// with <tt>E</tt> edges.
        /// </summary>
        /// <param name="v1">V1 the number of vertices in one partition</param>
        /// <param name="v2">V2 the number of vertices in the other partition</param>
        /// <param name="e">E the number of edges</param>
        /// <returns>a random simple bipartite graph on <tt>V1</tt> and <tt>V2</tt> vertices, containing a total of <tt>E</tt> edges</returns>
        /// <exception cref="ArgumentException">if no such simple bipartite graph exists</exception>
        public static Graph Bipartite(int v1, int v2, int e)
        {
            if (e > (long)v1 * v2) throw new ArgumentException("Too many edges");
            if (e < 0) throw new ArgumentException("Too few edges");
            var g = new Graph(v1 + v2);

            var vertices = new int[v1 + v2];
            for (var i = 0; i < v1 + v2; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);

            var set = new SET<EdgeU>();
            while (g.E < e)
            {
                var i = StdRandom.Uniform(v1);
                var j = v1 + StdRandom.Uniform(v2);
                var edge = new EdgeU(vertices[i], vertices[j]);
                if (set.Contains(edge)) continue;
                set.Add(edge);
                g.AddEdge(vertices[i], vertices[j]);
            }
            return g;
        }

        /// <summary>
        /// Returns a random simple bipartite graph on <tt>V1</tt> and <tt>V2</tt> vertices,
        /// containing each possible edge with probability <tt>p</tt>.
        /// </summary>
        /// <param name="v1">V1 the number of vertices in one partition</param>
        /// <param name="v2">V2 the number of vertices in the other partition</param>
        /// <param name="p">p the probability that the graph contains an edge with one endpoint in either side</param>
        /// <returns>a random simple bipartite graph on <tt>V1</tt> and <tt>V2</tt> vertices, containing each possible edge with probability <tt>p</tt></returns>
        /// <exception cref="ArgumentException">if probability is not between 0 and 1</exception>
        public static Graph Bipartite(int v1, int v2, double p)
        {
            if (p < 0.0 || p > 1.0)
                throw new ArgumentException("Probability must be between 0 and 1");
            var vertices = new int[v1 + v2];
            for (var i = 0; i < v1 + v2; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            var g = new Graph(v1 + v2);
            for (var i = 0; i < v1; i++)
                for (var j = 0; j < v2; j++)
                    if (StdRandom.Bernoulli(p))
                        g.AddEdge(vertices[i], vertices[v1 + j]);
            return g;
        }

        /// <summary>
        /// Returns a complete bipartite graph on <tt>V1</tt> and <tt>V2</tt> vertices.
        /// </summary>
        /// <param name="v1">V1 the number of vertices in one partition</param>
        /// <param name="v2">V2 the number of vertices in the other partition</param>
        /// <returns>a complete bipartite graph on <tt>V1</tt> and <tt>V2</tt> vertices</returns>
        /// <exception cref="ArgumentException">if probability is not between 0 and 1</exception>
        public static Graph CompleteBipartite(int v1, int v2)
        {
            return Bipartite(v1, v2, v1 * v2);
        }



        /// <summary>
        /// Returns a path graph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the path</param>
        /// <returns>a path graph on <tt>V</tt> vertices</returns>
        public static Graph Path(int v)
        {
            var g = new Graph(v);
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
        /// Returns a complete binary tree graph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the binary tree</param>
        /// <returns>a complete binary tree graph on <tt>V</tt> vertices</returns>
        public static Graph BinaryTree(int v)
        {
            var g = new Graph(v);
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
        /// Returns a cycle graph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the cycle</param>
        /// <returns>a cycle graph on <tt>V</tt> vertices</returns>
        public static Graph Cycle(int v)
        {
            var g = new Graph(v);
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
        /// Returns a wheel graph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the wheel</param>
        /// <returns>a wheel graph on <tt>V</tt> vertices: a single vertex connected to every vertex in a cycle on <tt>V-1</tt> vertices</returns>
        public static Graph Wheel(int v)
        {
            if (v <= 1) throw new ArgumentException("Number of vertices must be at least 2");
            var g = new Graph(v);
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);

            // simple cycle on V-1 vertices
            for (var i = 1; i < v - 1; i++)
            {
                g.AddEdge(vertices[i], vertices[i + 1]);
            }
            g.AddEdge(vertices[v - 1], vertices[1]);

            // connect vertices[0] to every vertex on cycle
            for (var i = 1; i < v; i++)
            {
                g.AddEdge(vertices[0], vertices[i]);
            }

            return g;
        }

        /// <summary>
        /// Returns a star graph on <tt>V</tt> vertices.
        /// </summary>
        /// <param name="v">V the number of vertices in the star</param>
        /// <returns>a star graph on <tt>V</tt> vertices: a single vertex connected to every other vertex</returns>
        public static Graph Star(int v)
        {
            if (v <= 0) throw new ArgumentException("Number of vertices must be at least 1");
            var g = new Graph(v);
            var vertices = new int[v];
            for (var i = 0; i < v; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);

            // connect vertices[0] to every other vertex
            for (var i = 1; i < v; i++)
            {
                g.AddEdge(vertices[0], vertices[i]);
            }

            return g;
        }


        /// <summary>
        /// Returns a uniformly random <tt>k</tt>-regular graph on <tt>V</tt> vertices
        /// (not necessarily simple). The graph is simple with probability only about e^(-k^2/4),
        /// which is tiny when k = 14.
        /// </summary>
        /// <param name="v">V the number of vertices in the graph</param>
        /// <param name="k"></param>
        /// <returns>a uniformly random <tt>k</tt>-regular graph on <tt>V</tt> vertices.</returns>
        public static Graph Regular(int v, int k)
        {
            if (v * k % 2 != 0) throw new ArgumentException("Number of vertices * k must be even");
            var g = new Graph(v);

            // create k copies of each vertex
            var vertices = new int[v * k];
            for (var vi = 0; vi < v; vi++)
            {
                for (var j = 0; j < k; j++)
                {
                    vertices[vi + v * j] = vi;
                }
            }

            // pick a random perfect matching
            StdRandom.Shuffle(vertices);
            for (var i = 0; i < v * k / 2; i++)
            {
                g.AddEdge(vertices[2 * i], vertices[2 * i + 1]);
            }
            return g;
        }

        /// <summary>
        /// Returns a uniformly random tree on <tt>V</tt> vertices.
        /// This algorithm uses a Prufer sequence and takes time proportional to <em>V log V</em>.
        /// http://www.proofwiki.org/wiki/Labeled_Tree_from_Prüfer_Sequence
        /// http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.36.6484&rep=rep1&type=pdf
        /// </summary>
        /// <param name="v">V the number of vertices in the tree</param>
        /// <returns>a uniformly random tree on <tt>V</tt> vertices</returns>
        public static Graph Tree(int v)
        {
            var g = new Graph(v);

            // special case
            if (v == 1) return g;

            // Cayley's theorem: there are V^(V-2) labeled trees on V vertices
            // Prufer sequence: sequence of V-2 values between 0 and V-1
            // Prufer's proof of Cayley's theorem: Prufer sequences are in 1-1
            // with labeled trees on V vertices
            var prufer = new int[v - 2];
            for (var i = 0; i < v - 2; i++)
                prufer[i] = StdRandom.Uniform(v);

            // degree of vertex v = 1 + number of times it appers in Prufer sequence
            var degree = new int[v];
            for (var vi = 0; vi < v; vi++)
                degree[vi] = 1;
            for (var i = 0; i < v - 2; i++)
                degree[prufer[i]]++;

            // pq contains all vertices of degree 1
            var pq = new MinPQ<Integer>();
            for (var vi = 0; vi < v; vi++)
                if (degree[vi] == 1) pq.Insert(vi);

            // repeatedly delMin() degree 1 vertex that has the minimum index
            for (var i = 0; i < v - 2; i++)
            {
                int vmin = pq.DelMin();
                g.AddEdge(vmin, prufer[i]);
                degree[vmin]--;
                degree[prufer[i]]--;
                if (degree[prufer[i]] == 1) pq.Insert(prufer[i]);
            }
            g.AddEdge(pq.DelMin(), pq.DelMin());
            return g;
        }
    }
}
