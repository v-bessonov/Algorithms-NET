using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.Helpers;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("CC", "Compute connected components using depth first search.")]
    public class CCWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyG.txt"); // Prompt
            Console.WriteLine("2 - mediumG.txt"); // Prompt
            //Console.WriteLine("3 - largeG.zip"); // Prompt
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
                //case "3":
                //    fieName = "largeG.zip";
                //    break;
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

            var cc = new CC(graph);

            // number of connected components
            var m= cc.Count();
            Console.WriteLine($"{m} components");

            // compute list of vertices in each connected component
            var components = new Core.Collections.Queue<Integer>[m];
            for (var i = 0; i < m; i++)
            {
                components[i] = new Core.Collections.Queue<Integer>();
            }
            for (var vi = 0; vi < graph.V; vi++)
            {
                components[cc.Id(vi)].Enqueue(vi);
            }

            // print results
            for (var i = 0; i < m; i++)
            {
                foreach (int vi in components[i])
                {
                    Console.Write($"{vi} ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
