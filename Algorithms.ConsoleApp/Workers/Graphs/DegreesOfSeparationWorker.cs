using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("DegreesOfSeparation", "Class provides a client for finding the degree of separation between one distinguished individual and every other individual in a social network.")]
    public class DegreesOfSeparationWorker : IWorker
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
            string source;
            List<string> names;
            switch (fileNumber)
            {
                case "1":
                    fileName = "routes.txt";
                    delimiter = '\u0020';
                    source = "JFK";
                    names = new List<string> { "LAS", "DFW", "EWR" };
                    break;
                case "2":
                    fileName = "movies.txt";
                    delimiter = '/';
                    source = "Bacon, Kevin";
                    names = new List<string> { "Kidman, Nicole", "Grant, Cary" };
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

            var degreesOfSeparation = new DegreesOfSeparation(lines, delimiter, source);

            Console.WriteLine($"{source}");
            foreach (var name in names)
            {
                Console.WriteLine($"{name}");
                degreesOfSeparation.Find(name);
            }
            Console.ReadLine();
        }
    }
}
