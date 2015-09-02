using System;
using System.IO;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("FrequencyCounter", "Provides a client that for reading in a sequence of words and printing a word (exceeding a given length) that occurs most frequently.")]
    public class FrequencyCounterWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyTale.txt"); // Prompt
            Console.WriteLine("2 - tale.txt"); // Prompt
            Console.WriteLine("3 - leipzig1M.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyTale.txt";
                    break;
                case "2":
                    fieName = "tale.txt";
                    break;
                case "3":
                    fieName = "leipzig1M.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var file = $"Files\\Searching\\{fieName}";
            if (string.IsNullOrWhiteSpace(file))
            {
                Console.Error.WriteLine("file name is empty");
                throw new Exception();
            }

            var fileReader = new StreamReader(file);
            string line;

            var frequencyCounter = new FrequencyCounter();

            var lineCounter = 0;
            while ((line = fileReader.ReadLine()) != null)
            {
                var words = line.Split(new[] { " ", "\t", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    frequencyCounter.InsertWord(word);
                }
                lineCounter++;
                if (lineCounter == 1000000)
                {
                    Console.WriteLine($"#{lineCounter} line processed");
                }
                Console.WriteLine($"#{lineCounter} line processed");
            }

            fileReader.Close();

            frequencyCounter.SetMax();
            frequencyCounter.ShowStats();

            Console.ReadLine();
        }
    }
}
