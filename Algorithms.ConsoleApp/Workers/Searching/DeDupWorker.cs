using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("DeDup", "Provides a client that for reading in a sequence of words from standard input and printing each word, removing any duplicates.")]
    public class DeDupWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyTale.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyTale.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Searching\\{fieName}");
            var words = @in.ReadAllStrings();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            var listStrings = words.ToList();

            var deDup =  new DeDup(listStrings);

            deDup.ShowWords();


            Console.ReadLine();
        }
    }
}
