using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DirectedEulerianPath", "Class represents a data type for finding an Eulerian path in a digraph.")]
    public class DirectedEulerianPathWorker : IWorker
    {
        public void Run()
        {


            const int v = 50;
            const int e = 100;


            // Eulerian cycle
            var g1 = DigraphGenerator.EulerianCycle(v, e);
            DirectedEulerianPath.UnitTest(g1, "Eulerian cycle");
            Console.WriteLine("---------------------------------------------------------------");

            // Eulerian path
            var g2 = DigraphGenerator.EulerianPath(v, e);
            DirectedEulerianPath.UnitTest(g2, "Eulerian path");
            Console.WriteLine("---------------------------------------------------------------");

            // add one random edge
            var g3 = new Digraph(g2);
            g3.AddEdge(StdRandom.Uniform(v), StdRandom.Uniform(v));
            DirectedEulerianPath.UnitTest(g3, "one random edge added to Eulerian path");
            Console.WriteLine("---------------------------------------------------------------");

            // self loop
            var g4 = new Digraph(v);
            var v4 = StdRandom.Uniform(v);
            g4.AddEdge(v4, v4);
            DirectedEulerianPath.UnitTest(g4, "single self loop");
            Console.WriteLine("---------------------------------------------------------------");

            // single edge
            var g5 = new Digraph(v);
            g5.AddEdge(StdRandom.Uniform(v), StdRandom.Uniform(v));
            DirectedEulerianPath.UnitTest(g5, "single edge");
            Console.WriteLine("---------------------------------------------------------------");

            // empty digraph
            var g6 = new Digraph(v);
            DirectedEulerianPath.UnitTest(g6, "empty digraph");
            Console.WriteLine("---------------------------------------------------------------");

            // random digraph
            var g7 = DigraphGenerator.Simple(v, e);
            DirectedEulerianPath.UnitTest(g7, "simple digraph");
            Console.WriteLine("---------------------------------------------------------------");


            Console.ReadLine();
        }
    }
}
