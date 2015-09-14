using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("BreadthFirstPaths", "Run breadth first search on an undirected graph. Runs in O(E + V) time.")]
    public class BreadthFirstPathsWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyCG.txt"); // Prompt
            Console.WriteLine("2 - mediumG.txt"); // Prompt
            Console.WriteLine("3 - largeG.zip"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyCG.txt";
                    break;
                case "2":
                    fieName = "mediumG.txt";
                    break;
                case "3":
                    fieName = "largeG.zip";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Graphs\\{fieName}");
            var lines = !fieName.EndsWith("zip") ? @in.ReadAllLines() : @in.ReadAllLinesFromZip();

            var lineIterator = 0;
            var v = 0;
            var e = 0;
            var edges = new List<EdgeU>();
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
                    var edge = new EdgeU(ve, we);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var graph = new Graph(v, e, edges);
            if (fileNumber != "3")
            {
                Console.WriteLine(graph);
            }


            const int s = 0;
            var bfs1 = new BreadthFirstPaths(graph, s);

            for (var vi = 0; vi < graph.V; vi++)
            {
                if (bfs1.HasPathTo(vi))
                {
                    Console.Write($"{s} to {vi}:  ");
                    foreach (int x in bfs1.PathTo(vi))
                    {
                        if (x == s) Console.Write(x);
                        else Console.Write($"-{x}");
                    }
                    Console.WriteLine();
                }

                else
                {
                    Console.WriteLine($"{s} to {v}:  not connected{Environment.NewLine}");
                }
                if (vi >= 1 && fileNumber == "3")
                {
                    break;
                }

            }

            //Console.WriteLine("------------------------------------------------");

            Console.ReadLine();
        }
    }
}
