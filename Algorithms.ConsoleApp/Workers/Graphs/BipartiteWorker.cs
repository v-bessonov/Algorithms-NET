using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("Bipartite", "Class represents a data type for determining whether an undirected graph is bipartite or whether it has an odd-length cycle.")]
    public class BipartiteWorker : IWorker
    {
        public void Run()
        {
            // create random bipartite graph with V vertices and E edges; then add F random edges
            const int vv = 20;
            const int e = 30;
            const int f = 5;

            var graph = new Graph(vv);
            var vertices = new int[vv];
            for (var i = 0; i < vv; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            for (var i = 0; i < e; i++)
            {
                var v = StdRandom.Uniform(vv / 2);
                var w = StdRandom.Uniform(vv / 2);
                graph.AddEdge(vertices[v], vertices[vv / 2 + w]);
            }

            // add F extra edges
            for (var i = 0; i < f; i++)
            {
                var v = StdRandom.Uniform(vv);
                var w = StdRandom.Uniform(vv);
                graph.AddEdge(v, w);
            }

            Console.WriteLine(graph);
            var b = new Bipartite(graph);
            if (b.IsBipartite())
            {
                Console.WriteLine("Graph is bipartite");
                for (var v = 0; v < graph.V; v++)
                {
                    Console.WriteLine(v + ": " + b.Color(v));
                }
            }
            else
            {
                Console.Write("Graph has an odd-length cycle: ");
                foreach (int x in b.OddCycle())
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
