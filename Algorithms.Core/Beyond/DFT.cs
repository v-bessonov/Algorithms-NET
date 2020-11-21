using System;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Beyond
{
    /// <summary>
    /// The <tt>FFT</tt> class provides methods for computing the 
    /// FFT (Fast-Fourier Transform), inverse FFT, linear convolution,
    /// and circular convolution of a complex array.
    /// <p>
    /// It is a bare-bones implementation that runs in <em>N</em> log <em>N</em> time,
    /// where <em>N</em> is the length of the complex array. For simplicity,
    /// <em>N</em> must be a power of 2.
    /// Our goal is to optimize the clarity of the code, rather than performance.
    /// It is not the most memory efficient implementation because it uses
    /// objects to represents complex numbers and it it re-allocates memory
    /// for the subarray, instead of doing in-place or reusing a single temporary array.
    /// 
    /// </summary>
    public class DFT
    {

        private static readonly Complex Zero = new Complex(0, 0);

        // Do not instantiate.
        private DFT() { }

        /// <summary>
        /// Returns the FFT of the specified complex array.
        /// </summary>
        /// <param name="x">x the complex array</param>
        /// <returns>he FFT of the complex array <tt>x</tt></returns>
        /// <exception cref="ArgumentException">if the length of <t>x</tt> is not a power of 2</exception>
        public static Complex[] Fft(Complex[] x)
        {
            var n = x.Length;

            // base case
            if (n == 1) return new[] { x[0] };

            // radix 2 Cooley-Tukey FFT
            if (n % 2 != 0)
            {
                throw new ArgumentException("N is not a power of 2");
            }

            // fft of even terms
            var even = new Complex[n / 2];
            for (var k = 0; k < n / 2; k++)
            {
                even[k] = x[2 * k];
            }
            var q = Fft(even);

            // fft of odd terms
            var odd = even;  // reuse the array
            for (var k = 0; k < n / 2; k++)
            {
                odd[k] = x[2 * k + 1];
            }
            var r = Fft(odd);

            // combine
            var y = new Complex[n];
            for (var k = 0; k < n / 2; k++)
            {
                var kth = -2 * k * Math.PI / n;
                var wk = new Complex(Math.Cos(kth), Math.Sin(kth));
                y[k] = q[k].Plus(wk.Times(r[k]));
                y[k + n / 2] = q[k].Minus(wk.Times(r[k]));
            }
            return y;
        }

        /// <summary>
        /// Returns the inverse FFT of the specified complex array.
        /// </summary>
        /// <param name="x">x the complex array</param>
        /// <returns>the inverse FFT of the complex array <tt>x</tt></returns>
        /// <exception cref="ArgumentException">if the length of <t>x</tt> is not a power of 2</exception>
        public static Complex[] Ifft(Complex[] x)
        {
            var n = x.Length;
            var y = new Complex[n];

            // take conjugate
            for (var i = 0; i < n; i++)
            {
                y[i] = x[i].Conjugate();
            }

            // compute forward FFT
            y = Fft(y);

            // take conjugate again
            for (var i = 0; i < n; i++)
            {
                y[i] = y[i].Conjugate();
            }

            // divide by N
            for (var i = 0; i < n; i++)
            {
                y[i] = y[i].Scale(1.0 / n);
            }

            return y;

        }


        /// <summary>
        /// Returns the circular convolution of the two specified complex arrays.
        /// </summary>
        /// <param name="x">x one complex array</param>
        /// <param name="y">y the other complex array</param>
        /// <returns>the circular convolution of <tt>x</tt> and <tt>y</tt></returns>
        /// <exception cref="ArgumentException">if the length of <tt>x</tt> does not equal the length of <tt>y</tt> or if the length is not a power of 2</exception>
        public static Complex[] Cconvolve(Complex[] x, Complex[] y)
        {

            // should probably pad x and y with 0s so that they have same length
            // and are powers of 2
            if (x.Length != y.Length)
            {
                throw new ArgumentException("Dimensions don't agree");
            }

            var n = x.Length;

            // compute FFT of each sequence
            var a = Fft(x);
            var b = Fft(y);

            // point-wise multiply
            var c = new Complex[n];
            for (var i = 0; i < n; i++)
            {
                c[i] = a[i].Times(b[i]);
            }

            // compute inverse FFT
            return Ifft(c);
        }

        /// <summary>
        /// Returns the linear convolution of the two specified complex arrays.
        /// </summary>
        /// <param name="x">x one complex array</param>
        /// <param name="y">y the other complex array</param>
        /// <returns>the linear convolution of <tt>x</tt> and <tt>y</tt></returns>
        /// <exception cref="ArgumentException">if the length of <tt>x</tt> does not equal the length of <tt>y</tt> or if the length is not a power of 2</exception>
        public static Complex[] Convolve(Complex[] x, Complex[] y)
        {
            var a = new Complex[2 * x.Length];
            for (var i = 0; i < x.Length; i++)
                a[i] = x[i];
            for (var i = x.Length; i < 2 * x.Length; i++)
                a[i] = Zero;

            var b = new Complex[2 * y.Length];
            for (var i = 0; i < y.Length; i++)
                b[i] = y[i];
            for (var i = y.Length; i < 2 * y.Length; i++)
                b[i] = Zero;

            return Cconvolve(a, b);
        }

        /// <summary>
        /// display an array of Complex numbers to standard output
        /// </summary>
        /// <param name="x"></param>
        /// <param name="title"></param>
        public static void Show(Complex[] x, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine("-------------------");
            foreach (var t in x)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine();
        }

    }
}
