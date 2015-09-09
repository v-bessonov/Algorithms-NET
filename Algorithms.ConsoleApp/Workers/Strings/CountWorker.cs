using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("Count", "Computes the frequency of occurrence of each character")]
    public class CountWorker : IWorker
    {
        public void Run()
        {

            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - abra.txt"); // Prompt
            Console.WriteLine("2 - pi.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var alpha = string.Empty;
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "abra.txt";
                    alpha = "ABCDR";
                    break;
                case "2":
                    fieName = "pi.txt";
                    alpha = "0123456789";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Strings\\{fieName}");
            var content = @in.ReadAll();

            var count = new Count(alpha);
            count.Run(content);


            Console.ReadLine();
        }
    }
}
