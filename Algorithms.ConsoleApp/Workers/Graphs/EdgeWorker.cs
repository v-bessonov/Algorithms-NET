using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("Edge", "Class represents a edge")]
    public class EdgeWorker : IWorker
    {
        public void Run()
        {
            var eu = new EdgeU(100, 50);
            var ed = new EdgeD(22, 11);
            var ew = new EdgeW(12, 34, 5.67);
            var dew = new DirectedEdge(12, 34, 5.67);
            Console.WriteLine(eu);
            Console.WriteLine(ed);
            Console.WriteLine(ew);
            Console.WriteLine(dew);
            Console.ReadLine();
        }
    }
}
