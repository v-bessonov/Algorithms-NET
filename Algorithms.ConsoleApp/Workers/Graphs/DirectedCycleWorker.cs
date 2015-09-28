using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DirectedCycle", "Class represents a data type for determining whether a digraph has a directed cycle.")]
    public class DirectedCycleWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyDG.txt"); // Prompt
            Console.WriteLine("2 - tinyDAG.txt"); // Prompt
            //Console.WriteLine("3 - largeG.zip"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyDG.txt";
                    break;
                case "2":
                    fieName = "tinyDAG.txt";
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

            var graph = new Digraph(v, e, edges);
            if (fileNumber != "3")
            {
                Console.WriteLine(graph);
            }


            var finder = new DirectedCycle(graph);
            if (finder.HasCycle())
            {
                Console.Write("Directed cycle: ");
                foreach (int vi in finder.Cycle())
                {
                    Console.Write($"{vi} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No directed cycle");
            }


            Console.ReadLine();
        }
    }
}
