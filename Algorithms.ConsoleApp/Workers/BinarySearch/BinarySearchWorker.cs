using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.BinarySearch
{
    [ConsoleCommand("BinarySearch", "Binary searching for an integer in a sorted array of integers.")]
    public class BinarySearchWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyT.txt"); // Prompt
            Console.WriteLine("2 - tinyW.txt"); // Prompt
            Console.WriteLine("3 - largeT.txt"); // Prompt
            Console.WriteLine("4 - largeW.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyT.txt";
                    break;
                case "2":
                    fieName = "tinyW.txt";
                    break;
                case "3":
                    fieName = "largeT.txt";
                    break;
                case "4":
                    fieName = "largeW.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In(string.Format("Files\\BinarySearch\\{0}", fieName));
            var whitelist = @in.ReadAllInts();

            // sort the array
            Array.Sort(whitelist);

            // Loop indefinitely
            // read integer key from standard input; print if not in whitelist

            while (true) 
            {
                Console.WriteLine("Enter input:"); // Prompt
                var line = Console.ReadLine(); // Get string from user
                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("input is empty");
                    continue;
                }
                if (line == "quit") // Check quit
                {
                    break;
                }
                int key;
                try
                {
                    key = Int32.Parse(line);
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                    continue;
                }
                var rank = Core.BinarySearch.BinarySearch.Rank(key, whitelist);
                Console.WriteLine("key: {0}, rank: {1}", key, rank);
            }
        }
    }
}
