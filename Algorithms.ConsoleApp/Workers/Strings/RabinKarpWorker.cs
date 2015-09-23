using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("RabinKarp", "Reads in two strings, the pattern and the input text, and searches for the pattern in the input text using the RabinKarp algorithm.")]
    public class RabinKarpWorker : IWorker
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
            //var pattern = pat.ToCharArray();
            //var text = txt.ToCharArray();

            var rabinKarp1 = new RabinKarp(pat);
            var offset1 = rabinKarp1.Search(txt);

            

            // print results
            Console.WriteLine("text:    " + txt);

            Console.Write("pattern: ");
            for (var i = 0; i < offset1; i++)
                Console.Write(" ");
            Console.WriteLine(pat);

            //Console.Write("pattern: ");
            //for (var i = 0; i < offset2; i++)
            //    Console.Write(" ");
            Console.WriteLine(pat);
        }
    }
}
