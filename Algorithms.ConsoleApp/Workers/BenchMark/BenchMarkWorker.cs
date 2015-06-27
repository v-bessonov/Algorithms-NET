using System;
using System.Threading;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.BenchMark;

namespace Algorithms.ConsoleApp.Workers.BenchMark
{
    [ConsoleCommand("BenchMark", "BenchMark for estimate results.")]
    public class BenchMarkWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("StopWatchTest"); // Prompt
            var stopwatch = new Stopwatch();
            Console.WriteLine("Start stopwatch"); // Prompt
            stopwatch.StartNew();

            Console.WriteLine("Start long process"); // Prompt
            Thread.Sleep(3000);
            Console.WriteLine("Passed {0}", stopwatch.TimePassed()); // Prompt

            Console.WriteLine("Stop stopwatch"); // Prompt
            stopwatch.Stop();
            Console.ReadKey();

        }
    }
}
