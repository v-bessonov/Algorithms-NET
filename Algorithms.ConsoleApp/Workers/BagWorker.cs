using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Collections;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers
{
    [ConsoleCommand("Bag", " A generic bag or multiset, implemented using a singly-linked list.")]
    public class BagWorker : IWorker
    {
        public void Run()
        {
            

            const string fieName = "tobe.txt";


            var @in = new In(string.Format("Files\\Collections\\{0}", fieName));
            var words = @in.ReadAllStrings();

            var bag = new Bag<string>();


            foreach (var word in words)
            {
                bag.Add(word);
            }
            Console.WriteLine("size of bag = {0}", bag.Size());
            foreach (var item in bag)
            {
                Console.WriteLine("item = {0}", item);
            }
            Console.ReadLine();
        }
    }
}
