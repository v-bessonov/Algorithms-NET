using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Context;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Context
{
    [ConsoleCommand("BTree", "Class represents BTree")]
    public class BTreeWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyBTree.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyBTree.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Context\\{fieName}");
            var keyValues = @in.ReadAllLines();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            //var listStrings = words.ToList();


            var bree = new BTree<string,string>();


            foreach (var keyValue in keyValues)
            {
                var splittedKeyValue = keyValue.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                var key = splittedKeyValue[0];
                var value = splittedKeyValue[1];
                bree.Put(key,value);
            }

            Console.WriteLine("cs.princeton.edu:  " + bree.Get("www.cs.princeton.edu"));
            Console.WriteLine("hardvardsucks.com: " + bree.Get("www.harvardsucks.com"));
            Console.WriteLine("simpsons.com:      " + bree.Get("www.simpsons.com"));
            Console.WriteLine("apple.com:         " + bree.Get("www.apple.com"));
            Console.WriteLine("ebay.com:          " + bree.Get("www.ebay.com"));
            Console.WriteLine("dell.com:          " + bree.Get("www.dell.com"));
            Console.WriteLine();

            Console.WriteLine("size:    " + bree.Size());
            Console.WriteLine("height:  " + bree.Height());
            Console.WriteLine(bree);

            Console.ReadLine();
        }
    }
}
