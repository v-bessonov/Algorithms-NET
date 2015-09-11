using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Beyond;
using Algorithms.Core.Helpers;

namespace Algorithms.ConsoleApp.Beyond
{
    [ConsoleCommand("FFT", "Compute the FFT and inverse FFT of a length N complex sequence.")]
    public class FFTWorker : IWorker
    {
        public void Run()
        {


            const int n = 4;
            var x = new Complex[n];

            var random =  new Random();
            // original data
            for (var i = 0; i < n; i++)
            {
                x[i] = new Complex(i, 0);
                x[i] = new Complex(-2 * random.NextDouble() + 1, 0);
            }
            FFT.Show(x, "x");

            // FFT of original data
            var y = FFT.Fft(x);
            FFT.Show(y, "y = fft(x)");

            // take inverse FFT
            var z = FFT.Ifft(y);
            FFT.Show(z, "z = ifft(y)");

            // circular convolution of x with itself
            var c = FFT.Cconvolve(x, x);
            FFT.Show(c, "c = cconvolve(x, x)");

            // linear convolution of x with itself
            var d = FFT.Convolve(x, x);
            FFT.Show(d, "d = convolve(x, x)");


            Console.ReadLine();
        }
    }
}
