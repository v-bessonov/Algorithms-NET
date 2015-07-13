using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Sorting;
using StringComparer = Algorithms.Core.Sorting.StringComparer;

namespace Algorithms.ConsoleApp.Workers.Sorting
{
    [ConsoleCommand("MaxPQ", "Generic max priority queue implementation with a binary heap.")]
    public class MaxPQWorker : IWorker
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
                    fieName = "tinyPQ.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In(string.Format("Files\\Sorting\\{0}", fieName));
            var words = @in.ReadAllStrings();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            var listStrings = words.ToList();


            var pq = new MaxPQ<string>(new StringComparer());
            //Fill Priority Queue
            foreach (var word in listStrings)
            {
                pq.Insert(word);
            }
            // print results
            foreach (var item in pq)
            {
                Console.WriteLine(item);
            }


            Console.ReadLine();
        }
    }
}
