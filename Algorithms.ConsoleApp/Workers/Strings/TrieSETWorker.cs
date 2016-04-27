using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;
using Algorithms.Core.InOut;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("TrieSET", "An set for extended ASCII strings, implemented  using a 256-way trie.")]
    public class TrieSETWorker : IWorker
    {
        public void Run()
        {

            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - shellsST.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "shellsST.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Strings\\{fieName}");
            var content = @in.ReadAllStrings();
            var st = new TrieSET<string>();
            foreach (var word in content)
            {
                st.Add(word);
            }

            // print results
            if (st.Size() < 100)
            {
                Console.WriteLine("keys(\"\"):");
                foreach (var key in st)
                {
                    Console.WriteLine($"{key}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("longestPrefixOf(\"shellsort\"):");
            Console.WriteLine(st.LongestPrefixOf("shellsort"));
            Console.WriteLine();


            Console.WriteLine("longestPrefixOf(\"xshellsort\"):");
            Console.WriteLine(st.LongestPrefixOf("xshellsort"));
            Console.WriteLine();

            Console.WriteLine("keysWithPrefix(\"shor\"):");
            foreach (var s in st.KeysWithPrefix("shor"))
                Console.WriteLine(s);
            Console.WriteLine();

            Console.WriteLine("keysWithPrefix(\"shortening\"):");
            foreach (var s in st.KeysWithPrefix("shortening"))
                Console.WriteLine(s);
            Console.WriteLine();

            Console.WriteLine("keysThatMatch(\".he.l.\"):");
            foreach (var s in st.KeysThatMatch(".he.l."))
                Console.WriteLine(s);

            Console.ReadLine();
        }
    }
}
