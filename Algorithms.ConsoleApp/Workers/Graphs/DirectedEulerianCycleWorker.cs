using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DirectedEulerianCycle", "Class represents a data type for finding an Eulerian cycle or path in a digraph.")]
    public class DirectedEulerianCycleWorker : IWorker
    {
        public void Run()
        {

            const int v = 50;
            const int e = 100;

            // Eulerian cycle
            var g1 = DigraphGenerator.EulerianCycle(v, e);
            DirectedEulerianCycle.UnitTest(g1, "Eulerian cycle");
            Console.WriteLine("---------------------------------------------------------------");

            // Eulerian path
            var g2 = DigraphGenerator.EulerianPath(v, e);
            DirectedEulerianCycle.UnitTest(g2, "Eulerian path");
            Console.WriteLine("---------------------------------------------------------------");

            // empty digraph
            var g3 = new Digraph(v);
            DirectedEulerianCycle.UnitTest(g3, "empty digraph");
            Console.WriteLine("---------------------------------------------------------------");

            // self loop
            var g4 = new Digraph(v);
            var v4 = StdRandom.Uniform(v);
            g4.AddEdge(v4, v4);
            DirectedEulerianCycle.UnitTest(g4, "single self loop");
            Console.WriteLine("---------------------------------------------------------------");

            // union of two disjoint cycles
            var h1 = DigraphGenerator.EulerianCycle(v / 2, e / 2);
            var h2 = DigraphGenerator.EulerianCycle(v - v / 2, e - e / 2);
            var perm = new int[v];
            for (var i = 0; i < v; i++)
                perm[i] = i;
            StdRandom.Shuffle(perm);
            var g5 = new Digraph(v);
            for (var vi = 0; vi < h1.V; vi++)
                foreach (int w in h1.Adj(vi))
                    g5.AddEdge(perm[vi], perm[w]);
            for (var vi = 0; vi < h2.V; vi++)
                foreach (int w in h2.Adj(vi))
                    g5.AddEdge(perm[v / 2 + vi], perm[v / 2 + w]);
            DirectedEulerianCycle.UnitTest(g5, "Union of two disjoint cycles");
            Console.WriteLine("---------------------------------------------------------------");

            // random digraph
            var g6 = DigraphGenerator.Simple(v, e);
            DirectedEulerianCycle.UnitTest(g6, "simple digraph");
            Console.WriteLine("---------------------------------------------------------------");


            Console.ReadLine();
        }
    }
}
