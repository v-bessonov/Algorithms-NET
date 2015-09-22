using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("EulerianPath", "Find an Eulerian path in a graph, if one exists.")]
    public class EulerianPathWorker : IWorker
    {
        public void Run()
        {


            const int v = 20;
            const int e = 30;


            // Eulerian cycle
            var g1 = GraphGenerator.EulerianCycle(v, e);
            EulerianPath.UnitTest(g1, "Eulerian cycle");

            // Eulerian path
            var g2 = GraphGenerator.EulerianPath(v, e);
            EulerianPath.UnitTest(g2, "Eulerian path");

            // add one random edge
            var g3 = new Graph(g2);
            g3.AddEdge(StdRandom.Uniform(v), StdRandom.Uniform(v));
            EulerianPath.UnitTest(g3, "one random edge added to Eulerian path");

            // self loop
            var g4 = new Graph(v);
            var v4 = StdRandom.Uniform(v);
            g4.AddEdge(v4, v4);
            EulerianPath.UnitTest(g4, "single self loop");

            // single edge
            var g5 = new Graph(v);
            g5.AddEdge(StdRandom.Uniform(v), StdRandom.Uniform(v));
            EulerianPath.UnitTest(g5, "single edge");

            // empty graph
            var g6 = new Graph(v);
            EulerianPath.UnitTest(g6, "empty graph");

            // random graph
            var g7 = GraphGenerator.Simple(v, e);
            EulerianPath.UnitTest(g7, "simple graph");

            Console.ReadLine();
        }
    }
}
