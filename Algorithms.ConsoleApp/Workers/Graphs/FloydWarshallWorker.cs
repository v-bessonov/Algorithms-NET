using System;
using System.Collections.Generic;
using System.Globalization;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("FloydWarshall", "Class represents a data type for solving the all-pairs shortest paths problem in edge-weighted digraphs with no negative cycles.")]
    public class FloydWarshallWorker : IWorker
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
                    var lineSplitted = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var ve = Convert.ToInt32(lineSplitted[0]);
                    var we = Convert.ToInt32(lineSplitted[1]);
                    var weight = Convert.ToDouble(lineSplitted[2], CultureInfo.InvariantCulture);
                    var edge = new DirectedEdge(ve, we, weight);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var adjMatrixEdgeWeightedDigraph = new AdjMatrixEdgeWeightedDigraph(v, e, edges);
            Console.WriteLine(adjMatrixEdgeWeightedDigraph);
            // run Floyd-Warshall algorithm
            var spt = new FloydWarshall(adjMatrixEdgeWeightedDigraph);

            // print all-pairs shortest path distances
            Console.Write("  ");
            for (var vv = 0; vv < adjMatrixEdgeWeightedDigraph.V; vv++)
            {
                Console.Write($"{vv} ");
            }
            Console.WriteLine();
            for (var vv = 0; vv < adjMatrixEdgeWeightedDigraph.V; vv++)
            {
                Console.Write($"{vv}: ");
                for (var w = 0; w < adjMatrixEdgeWeightedDigraph.V; w++)
                {
                    Console.Write(spt.HasPath(vv, w) ? $"{spt.Dist(vv, w):00} " : "  Inf ");
                }
                Console.WriteLine();
            }

            // print negative cycle
            if (spt.HasNegativeCycle)
            {
                Console.WriteLine("Negative cost cycle:");
                foreach (var edge in spt.NegativeCycle())
                    Console.WriteLine(edge);

                Console.WriteLine();
            }

            // print all-pairs shortest paths
            else
            {
                for (var vv = 0; vv < adjMatrixEdgeWeightedDigraph.V; vv++)
                {
                    for (var w = 0; w < adjMatrixEdgeWeightedDigraph.V; w++)
                    {
                        if (spt.HasPath(vv, w))
                        {
                            Console.Write($"{vv} to {w} {spt.Dist(vv, w):00}  ");
                            foreach (var edge in spt.Path(vv, w))
                                Console.Write($"{edge}  ");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write($"{vv} to {w} no path{Environment.NewLine}");
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
