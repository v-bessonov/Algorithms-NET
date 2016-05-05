using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;
using Algorithms.Core.InOut;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("Genome", "Compress or expand a genomic sequence using a 2-bit code.")]
    public class GenomeWorker : IWorker
    {
        public void Run()
        {

            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - genomeTiny.txt"); // Prompt
            Console.WriteLine("2 - genomeVirus.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "genomeTiny.txt";
                    break;
                case "2":
                    fieName = "genomeVirus.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Strings\\{fieName}");
            var content = @in.ReadAll();
            var genome = new Genome();
            genome.Compress(content);


            var list = new List<byte>();
            var bytes = genome.ToByteList(list, content);
            genome.Expand(bytes.ToArray());


            Console.ReadLine();
        }
    }
}
