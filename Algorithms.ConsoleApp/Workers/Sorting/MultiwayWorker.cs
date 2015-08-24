using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Sorting;

namespace Algorithms.ConsoleApp.Workers.Sorting
{
    [ConsoleCommand("Multiway", "Provides a client for reading in several sorted text files and merging them together into a single sorted text stream.")]
    public class MultiwayWorker : IWorker
    {
        public void Run()
        {

            var filesList = new List<string> { "m1.txt", "m2.txt", "m3.txt" };
            var wordsList = new List<List<string>>();

            foreach (var fileName in filesList)
            {
                var @in = new In($"Files\\Sorting\\{fileName}");
                var words = @in.ReadAllStrings();
                wordsList.Add(words.ToList());
            }

            var multiway = new Multiway(wordsList);

            multiway.ShowWords();

            Console.ReadLine();
        }
    }
}
