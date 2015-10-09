using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("TransitiveClosure", "Class represents a data type for computing the transitive closure of a digraph.")]
    public class TransitiveClosureWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyDG.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "tinyDG.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Graphs\\{fileName}");
            var lines = @in.ReadAllLines();

            var lineIterator = 0;
            var v = 0;
            var e = 0;
            var edges = new List<EdgeD>();
            foreach (var line in lines)
            {
                if (lineIterator == 0)
                {
                    v = Convert.ToInt32(line);
                }
                if (lineIterator == 1)
                {
                    e = Convert.ToInt32(line);
                }
                if (lineIterator > 1)
                {
                    var lineSplitted = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var ve = Convert.ToInt32(lineSplitted[0]);
                    var we = Convert.ToInt32(lineSplitted[1]);
                    var edge = new EdgeD(ve, we);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var digraph = new Digraph(v, e, edges);

            Console.WriteLine(digraph);

            var tc = new TransitiveClosure(digraph);

            // print header
            Console.Write("     ");
            for (var i = 0; i < digraph.V; i++)
                Console.Write($"{i,3}");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");

            // print transitive closure
            for (var i = 0; i < digraph.V; i++)
            {
                Console.Write($"{i,3}: ");
                for (var w = 0; w < digraph.V; w++)
                {
                    Console.Write(tc.Reachable(i, w) ? "  T" : "   ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
