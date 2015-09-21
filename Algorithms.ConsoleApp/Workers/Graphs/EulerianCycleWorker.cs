using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("EulerianCycle", "Find an Eulerian cycle in a graph, if one exists.")]
    public class EulerianCycleWorker : IWorker
    {
        public void Run()
        {


            const int vv = 20;
            const int e = 30;

            // Eulerian cycle
            var g1 = GraphGenerator.EulerianCycle(vv, e);
            EulerianCycle.UnitTest(g1, "Eulerian cycle");

            // Eulerian path
            var g2 = GraphGenerator.EulerianCycle(vv, e);
            EulerianCycle.UnitTest(g2, "Eulerian path");

            // empty graph
            var g3 = new Graph(vv);
            EulerianCycle.UnitTest(g3, "empty graph");

            // self loop
            var g4 = new Graph(vv);
            var v4 = StdRandom.Uniform(vv);
            g4.AddEdge(v4, v4);
            EulerianCycle.UnitTest(g4, "single self loop");




            // union of two disjoint cycles
            var h1 = GraphGenerator.EulerianCycle(vv / 2, e / 2);
            var h2 = GraphGenerator.EulerianCycle(vv - vv / 2, e - e / 2);
            var perm = new int[vv];
            for (var i = 0; i < vv; i++)
                perm[i] = i;
            StdRandom.Shuffle(perm);
            var g5 = new Graph(vv);
            for (var v = 0; v < h1.V; v++)
                foreach (int w in h1.Adj(v))
                    g5.AddEdge(perm[v], perm[w]);
            for (var v = 0; v < h2.V; v++)
                foreach (int w in h2.Adj(v))
                    g5.AddEdge(perm[vv / 2 + v], perm[vv / 2 + w]);
            EulerianCycle.UnitTest(g5, "Union of two disjoint cycles");

            // random digraph
            var g6 = GraphGenerator.Simple(vv, e);
            EulerianCycle.UnitTest(g6, "simple graph");

            Console.ReadLine();
        }
    }
}
