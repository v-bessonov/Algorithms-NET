using System;
using System.Collections.Generic;
using System.Globalization;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DijkstraAllPairsSP", "Class represents a data type for solving the all-pairs shortest paths problem in edge-weighted digraphs where the edge weights are nonnegative.")]
    public class DijkstraAllPairsSPWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyEWD.txt"); // Prompt
            Console.WriteLine("2 - mediumEWD.txt"); // Prompt
            //Console.WriteLine("3 - mediumEWG.txt"); // Prompt

            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "tinyEWD.txt";
                    break;
                case "2":
                    fileName = "mediumEWD.txt";
                    break;
                //case "3":
                //    fileName = "largeEWG.zip";
                //    break;
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
            var edges = new List<DirectedEdge>();
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
                    var weight = Convert.ToDouble(lineSplitted[2], CultureInfo.InvariantCulture);
                    var edge = new DirectedEdge(ve, we, weight);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var edgeWeightedDigraph = new EdgeWeightedDigraph(v, e, edges);
            Console.WriteLine(edgeWeightedDigraph);

            const int s = 0;
            // compute shortest paths
            var sp = new DijkstraAllPairsSP(edgeWeightedDigraph);

            // print shortest path
            for (var t = 0; t < edgeWeightedDigraph.V; t++)
            {
                if (sp.HasPath(s,t))
                {
                    Console.Write($"{s} to {t} {$"{sp.Dist(s,t):0.00}"}  ");
                    foreach (var edge in sp.Path(s, t))
                    {
                        Console.Write($"{edge}   ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"{s} to {t}         no path{Environment.NewLine}");
                }
            }

            Console.ReadLine();
        }
    }
}
