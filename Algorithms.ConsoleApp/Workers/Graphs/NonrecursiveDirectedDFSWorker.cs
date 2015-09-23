using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.Helpers;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("NonrecursiveDirectedDFS", "Class represents a data type for determining the vertices reachable from a given source vertex <em>s</em> (or set of source vertices) in a digraph.")]
    public class NonrecursiveDirectedDFSWorker : IWorker
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
                    var lineSplitted = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var ve = Convert.ToInt32(lineSplitted[0]);
                    var we = Convert.ToInt32(lineSplitted[1]);
                    var edge = new EdgeD(ve, we);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var digraph = new Digraph(v, e, edges);
            Console.WriteLine(digraph);
            Console.WriteLine("------------------------------------------------------");


            var bag1 = new Core.Collections.Bag<Integer> { new Integer(1) };
            var bag2 = new Core.Collections.Bag<Integer> { new Integer(2) };
            var bag3 = new Core.Collections.Bag<Integer> { new Integer(6) };
            var listSources = new List<Core.Collections.Bag<Integer>> { bag1, bag2, bag3 };
            foreach (var sources in listSources)

            {

                foreach (var source in sources)
                {
                    // multiple-source reachability
                    var dfs = new NonrecursiveDirectedDFS(digraph, source.Value);
                    // print out vertices reachable from sources
                    for (var i = 0; i < digraph.V; i++)
                    {
                        if (dfs.Marked(i)) Console.Write($"{i} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------");
            }

            Console.ReadLine();
        }
    }
}
