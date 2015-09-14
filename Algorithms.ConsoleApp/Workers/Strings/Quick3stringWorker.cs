using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("Quick3string", "Class provides static methods for sorting an array of strings using 3-way radix quicksort.")]
    public class Quick3stringWorker : IWorker
    {
        public void Run()
        {

            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - shells.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "shells.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }

            var @in = new In($"Files\\Strings\\{fieName}");
            var content = @in.ReadAllStrings();

            var n = content.Length;

            Quick3string.Sort(content);

            for (var i = 0; i < n; i++)
                Console.WriteLine(content[i]);

            Console.ReadLine();
        }
    }
}
