using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("SymbolDigraph", "Class represents a digraph, where the vertex names are arbitrary strings.")]
    public class SymbolDigraphWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - routes.txt"); // Prompt
            Console.WriteLine("2 - movies.txt"); // Prompt
            //Console.WriteLine("3 - largeG.zip"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            char delimiter;
            List<string> names;
            switch (fileNumber)
            {
                case "1":
                    fileName = "routes.txt";
                    delimiter = '\u0020';
                    names = new List<string> {"JFK", "ATL", "LAX"};
                    break;
                case "2":
                    fileName = "movies.txt";
                    delimiter = '/';
                    names = new List<string> { "Tin Men (1987)", "Bacon, Kevin" };
                    break;
                //case "3":
                //    fieName = "largeG.zip";
                //    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Graphs\\{fileName}");
            var lines = !fileName.EndsWith("zip") ? @in.ReadAllLines() : @in.ReadAllLinesFromZip();

            var sg = new SymbolDigraph(lines, delimiter);
            var digraph = sg.G;


            foreach (var name in names)
            {
                Console.WriteLine($"{name}");
                if (sg.Contains(name))
                {
                    var s = sg.Index(name);
                    foreach (int v in digraph.Adj(s))
                    {
                        Console.WriteLine($"   {sg.Name(v)}");
                    }
                }
                else
                {
                    Console.WriteLine($"input not contain '{name}'");
                }
            }
            Console.ReadLine();
        }
    }
}
