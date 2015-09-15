using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("KMP", "Reads in two strings, the pattern and the input text, and searches for the pattern in the input text using the KMP algorithm.")]
    public class KMPWorker : IWorker
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

            var kmp1 = new KMP(pat);
            var offset1 = kmp1.Search(txt);

            var kmp2 = new KMP(pattern, 256);
            var offset2 = kmp2.Search(text);

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
