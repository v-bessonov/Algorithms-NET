using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("GraphGenerator", "Class provides static methods for creating various graphs.")]
    public class GraphGeneratorWorker : IWorker
    {
        public void Run()
        {

            const int v = 50;
            const int e = 100;
            const int v1 = v / 2;
            const int v2 = v - v1;

            Console.WriteLine("complete graph");
            var g1 = GraphGenerator.Complete(v);
            Console.WriteLine(g1);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("simple");
            var g2 = GraphGenerator.Simple(v,e);
            Console.WriteLine(g2);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("Erdos-Renyi");
            var p = e / (v * (v - 1) / 2.0);
            var g3 = GraphGenerator.Simple(v, p);
            Console.WriteLine(g3);
            Console.WriteLine("---------------------------------------------------------------");


            Console.WriteLine("complete bipartite");
            var g4 = GraphGenerator.CompleteBipartite(v1,v2);
            Console.WriteLine(g4);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("bipartite");
            var g5 = GraphGenerator.Bipartite(v1, v2,e);
            Console.WriteLine(g5);
            Console.WriteLine("---------------------------------------------------------------");


            Console.WriteLine("Erdos Renyi bipartite");
            var q = (double)e / (v1 * v2);
            var g6 = GraphGenerator.Bipartite(v1, v2, q);
            Console.WriteLine(g6);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("path");
            var g7 = GraphGenerator.Path(v);
            Console.WriteLine(g7);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("cycle");
            var g8 = GraphGenerator.Cycle(v);
            Console.WriteLine(g8);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("binary tree");
            var g9 = GraphGenerator.BinaryTree(v);
            Console.WriteLine(g9);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("tree");
            var g10 = GraphGenerator.Tree(v);
            Console.WriteLine(g10);
            Console.WriteLine("---------------------------------------------------------------");


            Console.WriteLine("4-regular");
            var g11 = GraphGenerator.Regular(v,4);
            Console.WriteLine(g11);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("star");
            var g12 = GraphGenerator.Star(v);
            Console.WriteLine(g12);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("wheel");
            var g13 = GraphGenerator.Wheel(v);
            Console.WriteLine(g13);
            Console.WriteLine("---------------------------------------------------------------");

            Console.ReadLine();
        }
    }
}
