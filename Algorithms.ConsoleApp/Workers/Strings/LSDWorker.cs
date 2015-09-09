using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("LSD", "LSD radix sort, - Sort a String[] array of N extended ASCII strings (R = 256), each of length W, - Sort an int[] array of N 32-bit integers, treating each integer as a sequence of W = 4 bytes (R = 256). Uses extra space proportional to N + R.")]
    public class LSDWorker : IWorker
    {
        public void Run()
        {

            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - words3.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "words3.txt";
                    break;
                case "2":
                    fieName = "pi.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Strings\\{fieName}");
            var content = @in.ReadAllStrings();

            var n = content.Length;
            // check that strings have fixed length
            var w = content[0].Length;
            for (var i = 0; i < n; i++)
            {
                if (content[i].Length == w) continue;
                Console.WriteLine("Strings must have fixed length");
                Console.ReadLine();
                return;
            }

            LSD.Sort(content, w);

            for (var i = 0; i < n; i++)
                Console.WriteLine(content[i]);

            Console.ReadLine();
        }
    }
}
