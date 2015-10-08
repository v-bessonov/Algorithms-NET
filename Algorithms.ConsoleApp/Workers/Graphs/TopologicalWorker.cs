using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("Topological", "Compute topological ordering of a DAG or edge-weighted DAG.")]
    public class TopologicalWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - jobs.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            char delimiter;
            switch (fileNumber)
            {
                case "1":
                    fileName = "jobs.txt";
                    delimiter = '/';
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Graphs\\{fileName}");
            var lines = !fileName.EndsWith("zip") ? @in.ReadAllLines() : @in.ReadAllLinesFromZip();

            var sg = new SymbolDigraph(lines, delimiter);
            Console.WriteLine(sg.G);

            var topological = new Topological(sg.G);
            foreach (int v in topological.Order())
            {
                Console.WriteLine(sg.Name(v));
            }

            Console.ReadLine();
        }
    }
}
