using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DepthFirstOrder", "Class represents a data type for determining depth-first search ordering of the vertices in a digraph")]
    public class DepthFirstOrderWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyDAG.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "tinyDAG.txt";
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
                    var lineSplitted = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var ve = Convert.ToInt32(lineSplitted[0]);
                    var we = Convert.ToInt32(lineSplitted[1]);
                    var edge = new EdgeD(ve, we);
                    edges.Add(edge);
                }

                lineIterator++;
            }

            var digraph = new Digraph(v, e, edges);
            Console.WriteLine(digraph);

            var dfs = new DepthFirstOrder(digraph);
            Console.WriteLine("   v  pre post");
            Console.WriteLine("--------------");
            for (var vi = 0; vi < digraph.V; vi++)
            {
                Console.Write($"{vi} {dfs.Pre(vi)} {dfs.Post(vi)}{Environment.NewLine}");
            }

            Console.Write("Preorder:  ");
            foreach (int vi in dfs.Pre())
            {
                Console.Write($"{vi} ");
            }
            Console.WriteLine();

            Console.Write("Postorder: ");
            foreach (int vi in dfs.Post())
            {
                Console.Write($"{vi} ");
            }
            Console.WriteLine();

            Console.Write("Reverse postorder: ");
            foreach (int vi in dfs.ReversePost())
            {
                Console.Write($"{vi} ");
            }
            Console.ReadLine();
        }
    }
}
