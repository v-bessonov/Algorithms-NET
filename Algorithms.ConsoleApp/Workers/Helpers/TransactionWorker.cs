using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;
using Algorithms.Core.Sorting;

namespace Algorithms.ConsoleApp.Workers.Helpers
{
    [ConsoleCommand("Transaction", "Data type to encapsulate a commercial transaction with a customer name, date, and amount.")]
    public class TransactionWorker : IWorker
    {
        public void Run()
        {

            var a = new Transaction[4];
            a[0] = new Transaction("Turing   6/17/1990  644.08");
            a[1] = new Transaction("Tarjan   3/26/2002 4121.85");
            a[2] = new Transaction("Knuth    6/14/1999  288.34");
            a[3] = new Transaction("Dijkstra 8/22/2007 2678.40");

            Console.WriteLine("Unsorted");
            foreach (var transaction in a)
                Console.WriteLine(transaction);
            Console.WriteLine();

            Console.WriteLine("Sort by date");
            Insertion<Transaction>.Sort(a, new WhenOrderComparer());
            Insertion<Transaction>.Show(a);
            Console.WriteLine();


            Console.WriteLine("Sort by customer");
            Insertion<Transaction>.Sort(a, new WhoOrderComparer());
            Insertion<Transaction>.Show(a);
            Console.WriteLine();

            Console.WriteLine("Sort by amount");
            Insertion<Transaction>.Sort(a, new HowMuchOrderComparer());
            Insertion<Transaction>.Show(a);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
