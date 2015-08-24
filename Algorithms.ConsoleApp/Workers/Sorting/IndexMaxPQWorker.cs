using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Sorting;
using StringComparer = Algorithms.Core.Sorting.StringComparer;

namespace Algorithms.ConsoleApp.Workers.Sorting
{
    [ConsoleCommand("IndexMaxPQ", "Represents an indexed priority queue of generic keys")]
    public class IndexMaxPQWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyPQ.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyIndexPQ.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Sorting\\{fieName}");
            var words = @in.ReadAllStrings();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            var listStrings = words.ToList();


            var pq = new IndexMaxPQ<string>(listStrings.Count);
            //Fill Priority Queue
            for (var i = 0; i < listStrings.Count; i++)
            {
                 pq.Insert(i, listStrings[i]);

            }
            // print results 
            foreach (var item in pq)
            {
                Console.WriteLine(pq.KeyOf(item));
            }


            Console.ReadLine();
        }
    }
}
