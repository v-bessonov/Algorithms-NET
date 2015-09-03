using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("BinarySearchST", "Class represents an ordered symbol table of generic key-value pairs.")]
    public class BinarySearchSTWorker : IWorker
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


            var st = new BinarySearchST<string,string>();


            foreach (var keyValue in keyValues)
            {
                var splittedKeyValue = keyValue.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var key = splittedKeyValue[0];
                var value = splittedKeyValue[1];
                st.Put(key,value);
            }
            // print results
            foreach (var item in st.Keys())
            {
                Console.WriteLine(st.Get(item));
            }
            Console.WriteLine(st.Max());
            Console.WriteLine(st.Min());
            Console.ReadLine();
        }
    }
}
