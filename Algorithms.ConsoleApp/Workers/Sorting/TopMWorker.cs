using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Sorting;

namespace Algorithms.ConsoleApp.Workers.Sorting
{
    [ConsoleCommand("TopM", "Provides a client that reads a sequence of transactions from standard input and prints the <em>M</em> largest ones")]
    public class TopMWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyBatch.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyBatch.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Sorting\\{fieName}");
            var words = @in.ReadAllLines();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            var listStrings = words.ToList();

            var topM =  new TopM(listStrings);

            topM.ShowTransactions();


            Console.ReadLine();
        }
    }
}
