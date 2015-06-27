using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Shuffle;

namespace Algorithms.ConsoleApp.Workers.Shuffle
{
    [ConsoleCommand("Knuth", "Rearranges an array of objects in uniformly random order")]
    public class KnuthWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - cards.txt"); // Prompt
            Console.WriteLine("2 - cardsUnicode.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "cards.txt";
                    break;
                case "2":
                    fieName = "cardsUnicode.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In(string.Format("Files\\Shuffle\\{0}", fieName));
            var cards = @in.ReadAllStrings();

            // shuffle the array
            Knuth.Shuffle(cards.Cast<object>().ToArray());

            // print results.
            foreach (var card in cards)
                Console.WriteLine(card);

            Console.ReadLine();
        }
    }
}
