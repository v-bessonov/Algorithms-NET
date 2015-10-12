using System;
using System.Collections.Generic;
using System.Globalization;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("BoruvkaMST", "Class represents a <em>minimum spanning tree</em> in an edge-weighted graph.")]
    public class BoruvkaMSTWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyEWG.txt"); // Prompt
            Console.WriteLine("2 - mediumEWG.txt"); // Prompt
            //Console.WriteLine("3 - mediumEWG.txt"); // Prompt

            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "tinyEWG.txt";
                    break;
                case "2":
                    fileName = "mediumEWG.txt";
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
            var edges = new List<EdgeW>();
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
                    var edge = new EdgeW(ve, we, weight);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var edgeWeightedGraph = new EdgeWeightedGraph(v, e, edges);
            Console.WriteLine(edgeWeightedGraph);

            var mst = new BoruvkaMST(edgeWeightedGraph);
            foreach (var edge in mst.Edges())
            {
                Console.WriteLine(edge);
            }
            Console.Write($"{$"{mst.Weight():0.00000}"}{Environment.NewLine}");

            Console.ReadLine();
        }
    }
}
