using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("SparseVector", "Class represents d-dimensional mathematical vector")]
    public class SparseVectorWorker : IWorker
    {
        public void Run()
        {

            var a = new SparseVector(10);
            var b = new SparseVector(10);

            a.Put(3, 0.50);
            a.Put(9, 0.75);
            a.Put(6, 0.11);
            a.Put(6, 0.00);
            b.Put(3, 0.60);
            b.Put(4, 0.90);
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            Console.WriteLine("a dot b = " + a.Dot(b));
            Console.WriteLine("a + b   = " + a.Plus(b));

            Console.ReadLine();
        }
    }
}
