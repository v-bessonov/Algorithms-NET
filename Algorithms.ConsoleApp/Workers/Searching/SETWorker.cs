using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("SET", "Class represents an ordered set of comparable keys.")]
    public class SETWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinySET.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinySET.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Searching\\{fieName}");
            var keys = @in.ReadAllLines();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            //var listStrings = words.ToList();


            var set = new SET<string>();


            foreach (var key in keys)
            {
                
                set.Add(key);
            }
            // print results
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine(set.Contains("www.cs.princeton.edu"));
            Console.WriteLine(!set.Contains("www.harvardsucks.com"));
            Console.WriteLine(set.Contains("www.simpsons.com"));
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine();


            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("ceiling(www.simpsonr.com) = " + set.Ceiling("www.simpsonr.com"));
            Console.WriteLine("ceiling(www.simpsons.com) = " + set.Ceiling("www.simpsons.com"));
            Console.WriteLine("ceiling(www.simpsont.com) = " + set.Ceiling("www.simpsont.com"));
            Console.WriteLine("floor(www.simpsonr.com)   = " + set.Floor("www.simpsonr.com"));
            Console.WriteLine("floor(www.simpsons.com)   = " + set.Floor("www.simpsons.com"));
            Console.WriteLine("floor(www.simpsont.com)   = " + set.Floor("www.simpsont.com"));
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine(set.Max());
            Console.WriteLine(set.Min());
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
