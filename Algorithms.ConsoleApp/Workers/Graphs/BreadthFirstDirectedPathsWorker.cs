using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("BreadthFirstDirectedPaths", "Class represents a data type a data type for finding shortest paths (number of edges) from a source vertex <em>s</em> (or set of source vertices) to every other vertex in the digraph.")]
    public class BreadthFirstDirectedPathsWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyDG.txt"); // Prompt
            Console.WriteLine("2 - mediumDG.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "tinyDG.txt";
                    break;
                case "2":
                    fileName = "mediumDG.txt";
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


            const int s = 3;
            var bfs = new BreadthFirstDirectedPaths(digraph, s);

            for (var i = 0; i < digraph.V; i++)
            {
                if (bfs.HasPathTo(i))
                {
                    Console.Write($"{s} to {i} ({bfs.DistTo(i)}):  ");
                    foreach (int x in bfs.PathTo(i))
                    {
                        if (x == s) Console.Write(x);
                        else Console.Write("->" + x);
                    }
                    Console.WriteLine();
                }

                else
                {
                    Console.Write($"{s} to {i} (-):  not connected{Environment.NewLine}");
                }

            }

            Console.ReadLine();
        }
    }
}
