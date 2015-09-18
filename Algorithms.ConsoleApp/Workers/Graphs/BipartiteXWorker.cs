using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("BipartiteX", "Class represents a data type for determining whether an undirected graph is bipartite or whether it has an odd-length cycle.")]
    public class BipartiteXWorker : IWorker
    {
        public void Run()
        {
            // create random bipartite graph with V vertices and E edges; then add F random edges
            const int v1 = 10;
            const int v2 = 10;
            const int e = 20;
            const int f = 5;

            // create random bipartite graph with V1 vertices on left side,
            // V2 vertices on right side, and E edges; then add F random edges
            var graph = GraphGenerator.Bipartite(v1, v2, e);
            for (var i = 0; i < f; i++)
            {
                var v = StdRandom.Uniform(v1 + v2);
                var w = StdRandom.Uniform(v1 + v2);
                graph.AddEdge(v, w);
            }

            Console.WriteLine(graph);


            var b = new BipartiteX(graph);
            if (b.IsBipartite())
            {
                Console.WriteLine("Graph is bipartite");
                for (var v = 0; v < graph.V; v++)
                {
                    Console.WriteLine($"{v}: {b.Color(v)}");
                }
            }
            else
            {
                Console.WriteLine("Graph has an odd-length cycle: ");
                foreach (int x in b.OddCycle())
                {
                    Console.Write($"{x} ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
