using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;

namespace Algorithms.ConsoleApp.Workers.Helpers
{
    [ConsoleCommand("Numeric", "Data type to operate with numerics")]
    public class NumericsWorker : IWorker
    {
        public void Run()
        {
            const long bound1 = 0L;
            const long bound2 = 100L;
            var primes = Numerics.Primes(bound1, bound2);

            foreach (var prime in primes)
            {
                Console.WriteLine(prime);
            }

            Console.WriteLine("-------------------------------------------------------------------");

            const long bound3 = 20000000L;
            const long bound4 = 30000000L;

            Console.WriteLine(Numerics.Prime(bound3, bound4));


            Console.WriteLine("-------------------------------------------------------------------");

            Console.WriteLine(Numerics.Prime());
            Console.WriteLine(Numerics.Prime());
            Console.WriteLine(Numerics.Prime());


            Console.ReadLine();
        }
    }
}
