using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("Edge", "Class represents a weighted edge")]
    public class EdgeWorker : IWorker
    {
        public void Run()
        {
            var e = new Edge(12, 34, 5.67);
            Console.WriteLine(e);
            Console.ReadLine();
        }
    }
}
