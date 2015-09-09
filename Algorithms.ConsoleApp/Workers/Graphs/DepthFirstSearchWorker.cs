using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DepthFirstSearch", "Class represents a data type for determining the vertices connected to a given source vertex <em>s</em> in an undirected graph.")]
    public class DepthFirstSearchWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyG.txt"); // Prompt
            Console.WriteLine("2 - mediumG.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyG.txt";
                    break;
                case "2":
                    fieName = "mediumG.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Graphs\\{fieName}");
            var lines = @in.ReadAllLines();

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
            Console.WriteLine(graph);
            var search1 = new DepthFirstSearch(graph, 0);
            for (var vg = 0; vg < graph.V; vg++)
            {
                if (search1.Marked(vg))
                    Console.WriteLine($"{vg} ");
            }

            Console.WriteLine("------------------------------------------------");

            var search2 = new DepthFirstSearch(graph, 9);
            for (var vg = 0; vg < graph.V; vg++)
            {
                if (search2.Marked(vg))
                    Console.WriteLine($"{vg} ");
            }

            Console.ReadLine();
        }
    }
}
