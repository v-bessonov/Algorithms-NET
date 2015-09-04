using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("LookupIndex", "Provides a data-driven client for reading in a key-value pairs from a file; then, printing the values corresponding to the keys found on standard input.")]
    public class LookupIndexWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - aminol.csv"); // Prompt
            Console.WriteLine("2 - movies.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            var lineSeparator = ',';
            switch (fileNumber)
            {
                case "1":
                    fieName = "aminol.csv";
                    lineSeparator = ',';
                    break;
                case "2":
                    fieName = "movies.txt";
                    lineSeparator = '/';
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Searching\\{fieName}");
            var lines = !fieName.EndsWith("zip"  ) ? @in.ReadAllLines() : @in.ReadAllLinesFromZip();


            //var list = words.Select(word => new StringComparable(word)).ToList();
            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            //var listStrings = words.ToList();

            var lookupIndex =  new LookupIndex();


            foreach (var line in lines)
            {

                var lineSplitted = line.Split(new[] { lineSeparator }, StringSplitOptions.RemoveEmptyEntries);
                var key = string.Empty;
                var fields = new List<string>();
                var fieldIterator = 0;
                foreach (var field in lineSplitted)
                {
                    if (fieldIterator == 0)
                    {
                        key = field;
                    }
                    else
                    {
                        fields.Add(field);
                    }
                    fieldIterator++;
                }

                lookupIndex.CreateLookup(key, fields);
            }

            lookupIndex.Check("Alanine");
            lookupIndex.Check("Glycine");
            lookupIndex.Check("CGA");
            Console.WriteLine("----------------------------------------------------------------------");
            lookupIndex.Check("Amants du Pont-Neuf, Les (1991)");
            lookupIndex.Check("Bad Boys (1983)");
            lookupIndex.Check("Sheen, Martin");
            Console.WriteLine("----------------------------------------------------------------------");

            Console.ReadLine();
        }
    }
}
