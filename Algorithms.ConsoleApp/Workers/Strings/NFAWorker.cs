using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("NFA", "Class provides a data type for creating a <em>nondeterministic finite state automaton</em> (NFA) from a regular expression")]
    public class NFAWorker : IWorker
    {
        public void Run()
        {

            var regs = new List<string[]>
            {
                new[] {"((A*B|AC)D)", "AAAABD"},
                new[] {"((A*B|AC)D)", "AAAAC"},
                new[] {"((a|(bc)*d)*)", "abcbcd"},
                new[] { "((a|(bc)*d)*)", "abcbcbcdaaaabcbcdaaaddd" }
            };
            foreach (var reg in regs)
            {
                var regexp = reg[0];
                var txt = reg[1];
                if (txt.Contains("|"))
                {
                    throw new ArgumentException("| character in text is not supported");
                }
                var nfa = new NFA(regexp);
                Console.WriteLine($"{regexp} : {txt}");
                Console.WriteLine(nfa.Recognizes(txt));
                Console.WriteLine("---------------------------------------------");
            }

            Console.ReadLine();
        }

    }
}
