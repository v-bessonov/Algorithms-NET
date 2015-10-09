using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("TopologicalX", "Compute topological ordering of a DAG or edge-weighted DAG.")]
    public class TopologicalXWorker : IWorker
    {
        public void Run()
        {


            // create random DAG with V vertices and E edges; then add F random edges
            const int v = 50;
            const int e = 100;
            const int f = 0;
            var g = DigraphGenerator.Dag(v, e);

            // add F extra edges
            for (var i = 0; i < f; i++)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                g.AddEdge(ve, we);
            }

            Console.WriteLine(g);

            // find a directed cycle
            var topological = new TopologicalX(g);
            if (!topological.HasOrder())
            {
                Console.WriteLine("Not a DAG");
            }

            // or give topologial sort
            else
            {
                Console.Write("Topological order: ");
                foreach (int i in topological.Order())
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
