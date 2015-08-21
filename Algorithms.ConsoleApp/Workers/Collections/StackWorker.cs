using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Collections;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Collections
{
    [ConsoleCommand("Stack", " The Stack class represents a last-in-first-out (LIFO) stack of generic items")]
    public class StackWorker : IWorker
    {
        public void Run()
        {
            

            const string fieName = "tobe.txt";


            var @in = new In($"Files\\Collections\\{fieName}");
            var words = @in.ReadAllStrings();

            var stack = new Stack<string>();


            foreach (var word in words)
            {
                stack.Push(word);
            }
            Console.WriteLine("size of stack = {0}", stack.Size());
            foreach (var item in stack)
            {
                Console.WriteLine("item = {0}", item);
            }
            while (!stack.IsEmpty())
            {
                var item = stack.Pop();
                Console.WriteLine("item = {0}", item);
            }
            Console.ReadLine();
        }
    }
}
