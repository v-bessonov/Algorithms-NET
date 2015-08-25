using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("BST", "Class represents an ordered symbol table of generic key-value pairs.")]
    public class BSTWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyST.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyST.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Searching\\{fieName}");
            var keyValues = @in.ReadAllLines();

            //var list = words.Select(word => new StringComparable(word)).ToList();

            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            //var listStrings = words.ToList();


            var bst = new BST<string,string>();


            foreach (var keyValue in keyValues)
            {
                var splittedKeyValue = keyValue.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var key = splittedKeyValue[0];
                var value = splittedKeyValue[1];
                bst.Put(key,value);
            }
            // print results
            foreach (var item in bst.LevelOrder())
            {
                Console.WriteLine(bst.Get(item));
            }
            Console.WriteLine(bst.Max());
            Console.WriteLine(bst.Min());
            Console.ReadLine();
        }
    }
}
