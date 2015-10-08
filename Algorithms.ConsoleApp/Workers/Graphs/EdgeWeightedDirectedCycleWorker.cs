using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("EdgeWeightedDirectedCycle", "Finds a directed cycle in an edge-weighted digraph.")]
    public class EdgeWeightedDirectedCycleWorker : IWorker
    {
        public void Run()
        {

            // create random DAG with V vertices and E edges; then add F random edges
            const int vv = 50;
            const int e = 100;
            const int f = 20;
            var digraph = new EdgeWeightedDigraph(vv);
            var vertices = new int[vv];
            for (var i = 0; i < vv; i++)
                vertices[i] = i;
            StdRandom.Shuffle(vertices);
            for (var i = 0; i < e; i++)
            {
                int v, w;
                do
                {
                    v = StdRandom.Uniform(vv);
                    w = StdRandom.Uniform(vv);
                } while (v >= w);
                var weight = StdRandom.Uniform();
                digraph.AddEdge(new DirectedEdge(v, w, weight));
            }

            // add F extra edges
            for (var i = 0; i < f; i++)
            {
                var v = StdRandom.Uniform(vv);
                var w = StdRandom.Uniform(vv);
                var weight = StdRandom.Uniform(0.0, 1.0);
                digraph.AddEdge(new DirectedEdge(v, w, weight));
            }

            Console.WriteLine(digraph);

            // find a directed cycle
            var finder = new EdgeWeightedDirectedCycle(digraph);
            if (finder.HasCycle())
            {
                Console.Write("Cycle: ");
                foreach (var edge in finder.Cycle())
                {
                    Console.Write(edge + " ");
                }
                Console.WriteLine();
            }

            // or give topologial sort
            else
            {
                Console.WriteLine("No directed cycle");
            }


            Console.ReadLine();
        }
    }
}
