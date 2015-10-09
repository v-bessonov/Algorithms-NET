using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.Helpers;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("TarjanSCC", "Class represents a data type for determining the strong components in a digraph using Tarjan's algorithm.")]
    public class TarjanSCCWorker : IWorker
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

            var scc = new TarjanSCC(digraph);

            // number of connected components
            var m = scc.Count();
            Console.WriteLine($"{m} components");

            // compute list of vertices in each strong component
            var components = new Core.Collections.Queue<Integer>[m];
            for (var i = 0; i < m; i++)
            {
                components[i] = new Core.Collections.Queue<Integer>();
            }
            for (var i = 0; i < digraph.V; i++)
            {
                components[scc.Id(i)].Enqueue(i);
            }

            // print results
            for (var i = 0; i < m; i++)
            {
                foreach (int j in components[i])
                {
                    Console.Write($"{j} ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
