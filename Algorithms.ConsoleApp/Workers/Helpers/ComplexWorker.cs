using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;

namespace Algorithms.ConsoleApp.Workers.Helpers
{
    [ConsoleCommand("Complex", "Data type for complex numbers")]
    public class ComplexWorker : IWorker
    {
        public void Run()
        {

            var a = new Complex(5.0, 6.0);
            var b = new Complex(-3.0, 4.0);

            Console.WriteLine("a            = " + a);
            Console.WriteLine("b            = " + b);
            Console.WriteLine("Re(a)        = " + a.Re());
            Console.WriteLine("Im(a)        = " + a.Im());
            Console.WriteLine("b + a        = " + b.Plus(a));
            Console.WriteLine("a - b        = " + a.Minus(b));
            Console.WriteLine("a * b        = " + a.Times(b));
            Console.WriteLine("b * a        = " + b.Times(a));
            Console.WriteLine("a / b        = " + a.Divides(b));
            Console.WriteLine("(a / b) * b  = " + a.Divides(b).Times(b));
            Console.WriteLine("conj(a)      = " + a.Conjugate());
            Console.WriteLine("|a|          = " + a.Abs());
            Console.WriteLine("tan(a)       = " + a.Tan());

            Console.ReadLine();
        }
    }
}
