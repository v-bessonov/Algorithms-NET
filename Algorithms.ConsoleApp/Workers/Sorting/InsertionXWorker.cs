using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Sorting;
using StringComparer = Algorithms.Core.Sorting.StringComparer;

namespace Algorithms.ConsoleApp.Workers.Sorting
{
    [ConsoleCommand("InsertionX", "Provides static methods for sorting a list of IComparable using an optimized version of insertion sort")]
    public class InsertionXWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tiny.txt"); // Prompt
            Console.WriteLine("2 - words3.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tiny.txt";
                    break;
                case "2":
                    fieName = "words3.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Sorting\\{fieName}");
            var words = @in.ReadAllStrings();

            var list = words.Select(word => new StringComparable(word)).ToList();

            var listComparable = list.Cast<IComparable>().ToList();
            var arrayComparable = list.Cast<IComparable>().ToArray();
            var listStrings = words.ToList();

            // sort list
            InsertionX.Sort(listComparable);
            // print results.
            AbstractSort.Show(listComparable);

            Console.WriteLine("-----------------------------------------------------");

            // sort array
            InsertionX.Sort(arrayComparable);
            // print results.
            AbstractSort.Show(arrayComparable);

            Console.WriteLine("-----------------------------------------------------");

            // sort list
            InsertionX<string>.Sort(listStrings,new StringComparer());
            // print results
            InsertionX<string>.Show(listStrings);

            Console.ReadLine();
        }
    }
}
