using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("BoyerMoore", "Reads in two strings, the pattern and the input text, and searches for the pattern in the input text using the BoyerMoore algorithm.")]
    public class BoyerMooreWorker : IWorker
    {
        public void Run()
        {

            var txt = "abacadabrabracabracadabrabrabracad";
            var pats = new List<string> {"abracadabra", "rab", "bcara", "rabrabracad", "abacad"};
            foreach (var pat in pats)
            {
                SearchPattern(pat, txt);
                Console.WriteLine("---------------------------------------------");
            }

            

            Console.ReadLine();
        }

        private static void SearchPattern(string pat, string txt)
        {
            var pattern = pat.ToCharArray();
            var text = txt.ToCharArray();

            var boyerMoore1 = new BoyerMoore(pat);
            var offset1 = boyerMoore1.Search(txt);

            var boyerMoore2 = new BoyerMoore(pattern, 256);
            var offset2 = boyerMoore2.Search(text);

            // print results
            Console.WriteLine("text:    " + txt);

            Console.Write("pattern: ");
            for (var i = 0; i < offset1; i++)
                Console.Write(" ");
            Console.WriteLine(pat);

            Console.Write("pattern: ");
            for (var i = 0; i < offset2; i++)
                Console.Write(" ");
            Console.WriteLine(pat);
        }
    }
}
