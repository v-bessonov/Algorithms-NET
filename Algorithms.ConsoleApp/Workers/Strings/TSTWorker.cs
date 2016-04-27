using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;
using Algorithms.Core.InOut;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("TST", "Symbol table with string keys, implemented using a ternary search trie (TST).")]
    public class TSTWorker : IWorker
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
            var st = new TST<Integer>();
            for (var i = 0; i< content.Length; i++)
            {
                st.Put(content[i], i);
            }

            // print results
            if (st.Size() < 100)
            {
                Console.WriteLine("keys(\"\"):");
                foreach (var key in st.Keys())
                {
                    Console.WriteLine($"{key} {st.Get(key).Value}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("longestPrefixOf(\"shellsort\"):");
            Console.WriteLine(st.LongestPrefixOf("shellsort"));
            Console.WriteLine();

            Console.WriteLine("longestPrefixOf(\"shell\"):");
            Console.WriteLine(st.LongestPrefixOf("shell"));
            Console.WriteLine();

            Console.WriteLine("keysWithPrefix(\"shor\"):");
            foreach (var s in st.KeysWithPrefix("shor"))
                Console.WriteLine(s);
            Console.WriteLine();

            Console.WriteLine("keysThatMatch(\".he.l.\"):");
            foreach (var s in st.KeysThatMatch(".he.l."))
                Console.WriteLine(s);


            Console.ReadLine();
        }
    }
}
