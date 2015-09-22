using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DigraphGenerator", "Class provides static methods for creating various digraphs.")]
    public class DigraphGeneratorWorker : IWorker
    {
        public void Run()
        {
            const int v = 50;
            const int e = 100;
            Console.WriteLine("complete graph");
            Console.WriteLine(DigraphGenerator.Complete(v));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("simple");
            Console.WriteLine(DigraphGenerator.Simple(v, e));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("path");
            Console.WriteLine(DigraphGenerator.Path(v));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("cycle");
            Console.WriteLine(DigraphGenerator.Cycle(v));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("Eulierian path");
            Console.WriteLine(DigraphGenerator.EulerianPath(v, e));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("Eulierian cycle");
            Console.WriteLine(DigraphGenerator.EulerianCycle(v, e));
            Console.WriteLine();

            Console.WriteLine("binary tree");
            Console.WriteLine(DigraphGenerator.BinaryTree(v));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("tournament");
            Console.WriteLine(DigraphGenerator.Tournament(v));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("DAG");
            Console.WriteLine(DigraphGenerator.Dag(v, e));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("rooted-in DAG");
            Console.WriteLine(DigraphGenerator.RootedInDag(v, e));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("rooted-out DAG");
            Console.WriteLine(DigraphGenerator.RootedOutDag(v, e));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("rooted-in tree");
            Console.WriteLine(DigraphGenerator.RootedInTree(v));
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("rooted-out DAG");
            Console.WriteLine(DigraphGenerator.RootedOutTree(v));
            Console.WriteLine("---------------------------------------------------------------");


            Console.ReadLine();
        }
    }
}
