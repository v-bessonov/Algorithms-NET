using System;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.StdLib;

namespace Algorithms.ConsoleApp.Workers.StdLib
{
    [ConsoleCommand("StdRandom", " A library of static methods to generate pseudo-random numbers")]
    public class StdRandomWorker : IWorker
    {
        public void Run()
        {

            const int n = 10;
            double[] t = { .5, .3, .1, .1 };
            Console.WriteLine("seed = {0}", StdRandom.GetSeed());
            for (var i = 0; i < n; i++)
            {
                Console.WriteLine("{0:D2}", StdRandom.Uniform(100));
                Console.WriteLine("{0:F3}", StdRandom.Uniform(10.0, 99.0));
                Console.WriteLine("{0}", StdRandom.Bernoulli(.5));
                Console.WriteLine("{0:F5}", StdRandom.Gaussian(9.0, .2));
                Console.WriteLine("{0:D2}", StdRandom.Discrete(t));
                Console.WriteLine();
            }

            var a = "A B C D E F G".Split(new []{' '}).Cast<object>().ToArray();
            StdRandom.Shuffle(a);

            foreach (var s in a)
            {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }
    }
}
