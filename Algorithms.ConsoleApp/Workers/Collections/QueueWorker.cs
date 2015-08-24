using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Collections;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Collections
{
    [ConsoleCommand("Queue", " The Queue class represents a first-in-first-out (FIFO) queue of generic items")]
    public class QueueWorker : IWorker
    {
        public void Run()
        {
            

            const string fieName = "tobe.txt";


            var @in = new In($"Files\\Collections\\{fieName}");
            var words = @in.ReadAllStrings();

            var queue = new Queue<string>();


            foreach (var word in words)
            {
                queue.Enqueue(word);
            }
            Console.WriteLine("size of queue = {0}", queue.Size());
            foreach (var item in queue)
            {
                Console.WriteLine("item = {0}", item);
            }
            while (!queue.IsEmpty())
            {
                var item = queue.Dequeue();
                Console.WriteLine("item = {0}", item);
            }
            Console.ReadLine();
        }
    }
}
