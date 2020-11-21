using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;
using System;

namespace Algorithms.ConsoleApp.Workers.Maths
{
    [ConsoleCommand("DFT", "Discrete Fourier Transform")]
    public class DFTWorker : IWorker
    {
        public void Run()
        {
            var xn = GetXn();
            for (int i = 0; i < xn.Length; i++)
            {
                Console.WriteLine($"x{i}={xn[i]}");
            }
            var Xn = new Complex[xn.Length];

            for (int n = 0; n < xn.Length; n++)
            {
                for (int m = 0; m < xn.Length; m++)
                {
                    var w = W(n, m, xn.Length);


                    if (Xn[n] == null)
                    {
                        Xn[n] = new Complex(xn[m], 0).Times(w);

                    }
                    else
                    {
                        Xn[n] = Xn[n].Plus(new Complex(xn[m], 0).Times(w));
                    }

                    //Console.WriteLine($"x{i}={xn[i]}");
                }
                Console.WriteLine($"Xn={Xn[n]}");
            }

            var xninverse = new Complex[xn.Length];
            for (int m = 0; m < xn.Length; m++)
            {
                for (int n = 0; n < xn.Length; n++)
                {

                    var winverse = WInverse(n, m, xn.Length);

                    if (xninverse[m] == null)
                    {
                        xninverse[m] = Xn[n].Times(winverse);
                    }
                    else
                    {
                        xninverse[m] = xninverse[m].Plus(Xn[n].Times(winverse));
                    }

                    //Console.WriteLine($"xninverse{n}={xn[i]}");
                }
                Console.WriteLine($"xninverse={xninverse[m].Re()/xn.Length}");
            }




            //var a = new Complex(5.0, 6.0);
            //var b = new Complex(-3.0, 4.0);

            //Console.WriteLine("a            = " + a);
            //Console.WriteLine("a            = " + a.ToExponentials());
            //Console.WriteLine("b            = " + b);
            //Console.WriteLine("b            = " + b.ToExponentials());
            //Console.WriteLine("Re(a)        = " + a.Re());
            //Console.WriteLine("Im(a)        = " + a.Im());
            //Console.WriteLine("b + a        = " + b.Plus(a));
            //Console.WriteLine("a - b        = " + a.Minus(b));
            //Console.WriteLine("a * b        = " + a.Times(b));
            //Console.WriteLine("b * a        = " + b.Times(a));
            //Console.WriteLine("a / b        = " + a.Divides(b));
            //Console.WriteLine("(a / b) * b  = " + a.Divides(b).Times(b));
            //Console.WriteLine("conj(a)      = " + a.Conjugate());
            //Console.WriteLine("|a|          = " + a.Abs());
            //Console.WriteLine("tan(a)       = " + a.Tan());

            Console.ReadLine();
        }

        private Complex W(int n, int m, int N)
        {
            var phase = -1.0 * 2.0 * Math.PI / N * n * m;
            var re = Math.Cos(phase);
            var im = Math.Sin(phase);
            return new Complex(re, im);
        }

        private Complex WInverse(int n, int m, int N)
        {
            var phase = 2.0 * Math.PI / N * n * m;
            var re = Math.Cos(phase);
            var im = Math.Sin(phase);
            return new Complex(re, im);
        }

        private double[] GetXn()
        {
            var x = new double[4] { 0.0, 1.0, 2.0, 3.0 };
            var y = new double[4];
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = 5.0 + 2 * Math.Cos(Math.PI / 2 * x[i] - Math.PI / 2) + 3.0 * Math.Cos(Math.PI * x[i]);

            }

            return y;
        }
    }
}
