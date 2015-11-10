using System;
using System.Collections.Generic;
using System.Globalization;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("AcyclicLP", "Class represents a data type for solving the single-source longest paths problem in edge-weighted directed acyclic graphs (DAGs). The edge weights can be positive, negative, or zero.")]
    public class AcyclicLPWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyEWDAG.txt"); // Prompt
            //Console.WriteLine("2 - mediumEWD.txt"); // Prompt
            //Console.WriteLine("3 - mediumEWG.txt"); // Prompt

            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "tinyEWDAG.txt";
                    break;
                //case "2":
                //    fileName = "mediumEWD.txt";
                //    break;
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

            const int s = 5;

            // find shortest path from s to each other vertex in DAG
            var lp = new AcyclicLP(edgeWeightedDigraph, s);
            for (var vi = 0; vi < edgeWeightedDigraph.V; vi++)
            {
                if (lp.HasPathTo(vi))
                {
                    Console.Write($"{s} to {vi} {$"{lp.DistTo(vi):0.00}"}  ");
                    foreach (var ei in lp.PathTo(vi))
                    {
                        Console.Write($"{ei}   ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"{s} to {vi}         no path{Environment.NewLine}");
                }
            }
            Console.ReadLine();
        }
    }
}
