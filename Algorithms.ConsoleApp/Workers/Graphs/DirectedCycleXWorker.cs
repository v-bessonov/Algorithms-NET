using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DirectedCycleX", "Class represents a data type for determining whether a digraph has a directed cycle.")]
    public class DirectedCycleXWorker : IWorker
    {
        public void Run()
        {

            // create random DAG with V vertices and E edges; then add F random edges
            const int v = 20;
            const int e = 30;
            const int f = 10;
            var digraph = DigraphGenerator.Dag(v, e);

            // add F extra edges
            for (var i = 0; i < f; i++)
            {
                var ve = StdRandom.Uniform(v);
                var we = StdRandom.Uniform(v);
                digraph.AddEdge(ve, we);
            }

            Console.WriteLine(digraph);


            var finder = new DirectedCycleX(digraph);
            if (finder.HasCycle())
            {
                Console.Write("Directed cycle: ");
                foreach (int j in finder.Cycle())
                {
                    Console.Write($"{j} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No directed cycle");
            }

            Console.ReadLine();
        }
    }
}
