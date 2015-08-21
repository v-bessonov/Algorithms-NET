using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;

namespace Algorithms.ConsoleApp.Workers.Helpers
{
    [ConsoleCommand("Vector", "Data type to encapsulate a date (day, month, and year).")]
    public class VectorWorker : IWorker
    {
        public void Run()
        {

            double[] xdata = { 1.0, 2.0, 3.0, 4.0 };
            double[] ydata = { 5.0, 2.0, 4.0, 1.0 };
            var x = new Vector(xdata);
            var y = new Vector(ydata);

            Console.WriteLine("   x       = " + x);
            Console.WriteLine("   y       = " + y);

            var z = x.Plus(y);
            Console.WriteLine("   z       = " + z);

            z = z.Times(10.0);
            Console.WriteLine(" 10z       = " + z);

            Console.WriteLine("  |x|      = " + x.Magnitude());
            Console.WriteLine(" <x, y>    = " + x.Dot(y));
            Console.WriteLine("dist(x, y) = " + x.DistanceTo(y));
            Console.WriteLine("dir(x)     = " + x.Direction());


            Console.ReadLine();
        }
    }
}
