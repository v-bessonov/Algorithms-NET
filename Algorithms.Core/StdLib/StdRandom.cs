using System;
using System.Collections.Generic;

namespace Algorithms.Core.StdLib
{
    /// <summary>
    /// A library of static methods to generate pseudo-random numbers from
    /// different distributions (bernoulli, uniform, gaussian, discrete,
    /// and exponential). Also includes a method for shuffling an array.
    /// </summary>
    public sealed class StdRandom
    {

        private static Random _random; // pseudo-random number generator
        private static int _seed; // pseudo-random number generator seed

        static StdRandom()
        {
            _random = new Random();
        }

        /**
    * Sets the seed of the psedurandom number generator.
    */
        public static void SetSeed(int s)
        {
            _seed = s;
            _random = new Random(_seed);
        }

        /// <summary>
        /// Returns the seed of the psedurandom number generator.
        /// </summary>
        /// <returns></returns>
        public static long GetSeed()
        {
            return _seed;
        }


        /// <summary>
        /// Return real number uniformly in [0, 1).
        /// </summary>
        /// <returns></returns>
        public static double Uniform()
        {
            return _random.NextDouble();
        }

        /// <summary>
        /// Returns an integer uniformly between 0 (inclusive) and N (exclusive).
        /// throws ArgumentException if <tt>N <= 0</tt>
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Uniform(int n)
        {
            if (n <= 0) throw new ArgumentException("Parameter N must be positive");
            return _random.Next(n);
        }

        /// <summary>
        /// Returns an integer uniformly in [a, b).
        /// throws ArgumentException if <tt>b <= a</tt>
        /// throws ArgumentException if <tt>b - a >= Integer.MAX_VALUE</tt>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Uniform(int a, int b)
        {
            if (b <= a) throw new ArgumentException("Invalid range");
            if ((long)b - a >= Int32.MaxValue) throw new ArgumentException("Invalid range");
            return a + Uniform(b - a);
        }

        /// <summary>
        /// Returns a real number uniformly in [a, b).
        /// throws ArgumentException unless <tt>a < b</tt>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Uniform(double a, double b)
        {
            if (!(a < b)) throw new ArgumentException("Invalid range");
            return a + Uniform() * (b - a);
        }

        /// <summary>
        /// Returns a boolean, which is true with probability p, and false otherwise.
        /// throws ArgumentException unless <tt>p >= 0.0</tt> and <tt>p <= 1.0</tt>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Bernoulli(double p)
        {
            if (!(p >= 0.0 && p <= 1.0))
                throw new ArgumentException("Probability must be between 0.0 and 1.0");
            return Uniform() < p;
        }

        /// <summary>
        /// Returns a boolean, which is true with probability .5, and false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool Bernoulli()
        {
            return Bernoulli(0.5);
        }

        /// <summary>
        /// Returns a real number with a standard Gaussian distribution.
        /// </summary>
        /// <returns></returns>
        public static double Gaussian()
        {
            // use the polar form of the Box-Muller transform
            double r, x;
            do
            {
                x = Uniform(-1.0, 1.0);
                var y = Uniform(-1.0, 1.0);
                r = x * x + y * y;
            } while (r >= 1 || Math.Abs(r) < 0.0001);
            return x * Math.Sqrt(-2 * Math.Log(r) / r);

            // Remark:  y * Math.sqrt(-2 * Math.log(r) / r)
            // is an independent random gaussian
        }

        /// <summary>
        /// Returns a real number from a gaussian distribution with given mean and stddev
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="stddev"></param>
        /// <returns></returns>
        public static double Gaussian(double mean, double stddev)
        {
            return mean + stddev * Gaussian();
        }

        /// <summary>
        /// Returns an integer with a geometric distribution with mean 1/p.
        /// throws ArgumentException unless <tt>p >= 0.0</tt> and <tt>p <= 1.0</tt>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int Geometric(double p)
        {
            if (!(p >= 0.0 && p <= 1.0))
                throw new ArgumentException("Probability must be between 0.0 and 1.0");
            // using algorithm given by Knuth
            return (int)Math.Ceiling(Math.Log(Uniform()) / Math.Log(1.0 - p));
        }

        /// <summary>
        /// Return an integer with a Poisson distribution with mean lambda.
        /// throws ArgumentException unless <tt>lambda > 0.0</tt> and not infinite
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static int Poisson(double lambda)
        {
            if (!(lambda > 0.0))
                throw new ArgumentException("Parameter lambda must be positive");
            if (Double.IsInfinity(lambda))
                throw new ArgumentException("Parameter lambda must not be infinite");
            // using algorithm given by Knuth
            // see http://en.wikipedia.org/wiki/Poisson_distribution
            var k = 0;
            var p = 1.0;
            var l = Math.Exp(-lambda);
            do
            {
                k++;
                p *= Uniform();
            } while (p >= l);
            return k - 1;
        }

        /// <summary>
        /// Returns a real number with a Pareto distribution with parameter alpha.
        /// throws ArgumentException unless <tt>alpha > 0.0</tt>
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static double Pareto(double alpha)
        {
            if (!(alpha > 0.0))
                throw new ArgumentException("Shape parameter alpha must be positive");
            return Math.Pow(1 - Uniform(), -1.0 / alpha) - 1.0;
        }

        /// <summary>
        /// Returns a real number with a Cauchy distribution.
        /// </summary>
        /// <returns></returns>
        public static double Cauchy()
        {
            return Math.Tan(Math.PI * (Uniform() - 0.5));
        }

        /// <summary>
        /// Returns a number from a discrete distribution: i with probability a[i].
        /// throws ArgumentException if sum of array entries is not (very nearly) equal to <tt>1.0</tt>
        /// throws ArgumentException unless <tt>a[i] >= 0.0</tt> for each index <tt>i</tt>
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int Discrete(double[] a)
        {
            const double epsilon = 1E-14;
            var sum = 0.0;
            for (var i = 0; i < a.Length; i++)
            {
                if (!(a[i] >= 0.0)) throw new ArgumentException("array entry " + i + " must be nonnegative: " + a[i]);
                sum = sum + a[i];
            }
            if (sum > 1.0 + epsilon || sum < 1.0 - epsilon)
                throw new ArgumentException("sum of array entries does not approximately equal 1.0: " + sum);

            // the for loop may not return a value when both r is (nearly) 1.0 and when the
            // cumulative sum is less than 1.0 (as a result of floating-point roundoff error)
            while (true)
            {
                var r = Uniform();
                sum = 0.0;
                for (var i = 0; i < a.Length; i++)
                {
                    sum = sum + a[i];
                    if (sum > r) return i;
                }
            }
        }

        /// <summary>
        /// Returns a real number from an exponential distribution with rate lambda.
        /// throws ArgumentException unless <tt>lambda > 0.0</tt>
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static double Exp(double lambda)
        {
            if (!(lambda > 0.0))
                throw new ArgumentException("Rate lambda must be positive");
            return -Math.Log(1 - Uniform()) / lambda;
        }

        /// <summary>
        /// Rearrange the elements of an array in random order.
        /// </summary>
        /// <param name="a"></param>
        public static void Shuffle(Object[] a)
        {
            var n = a.Length;
            for (var i = 0; i < n; i++)
            {
                var r = i + Uniform(n - i);     // between i and N-1
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// Rearrange the elements of List of IComparable in random order.
        /// </summary>
        /// <param name="a"></param>
        public static void Shuffle(IList<IComparable> a)
        {
            var n = a.Count;
            for (var i = 0; i < n; i++)
            {
                var r = i + Uniform(n - i);     // between i and N-1
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// Rearrange the elements of a double array in random order.
        /// </summary>
        /// <param name="a"></param>
        public static void Shuffle(double[] a)
        {
            var n = a.Length;
            for (var i = 0; i < n; i++)
            {
                var r = i + Uniform(n - i);     // between i and N-1
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// Rearrange the elements of an int array in random order.
        /// </summary>
        /// <param name="a"></param>
        public static void Shuffle(int[] a)
        {
            var n = a.Length;
            for (var i = 0; i < n; i++)
            {
                var r = i + Uniform(n - i);     // between i and N-1
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }


        /// <summary>
        /// Rearrange the elements of the subarray a[lo..hi] in random order.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        public static void Shuffle(Object[] a, int lo, int hi)
        {
            if (lo < 0 || lo > hi || hi >= a.Length)
            {
                throw new IndexOutOfRangeException("Illegal subarray range");
            }
            for (var i = lo; i <= hi; i++)
            {
                var r = i + Uniform(hi - i + 1);     // between i and hi
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// Rearrange the elements of the subarray a[lo..hi] in random order.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        public static void Shuffle(double[] a, int lo, int hi)
        {
            if (lo < 0 || lo > hi || hi >= a.Length)
            {
                throw new IndexOutOfRangeException("Illegal subarray range");
            }
            for (var i = lo; i <= hi; i++)
            {
                var r = i + Uniform(hi - i + 1);     // between i and hi
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// Rearrange the elements of the subarray a[lo..hi] in random order.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        public static void Shuffle(int[] a, int lo, int hi)
        {
            if (lo < 0 || lo > hi || hi >= a.Length)
            {
                throw new IndexOutOfRangeException("Illegal subarray range");
            }
            for (var i = lo; i <= hi; i++)
            {
                var r = i + Uniform(hi - i + 1);     // between i and hi
                var temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static long CurrentTimeMillis()
        {
            var jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)((DateTime.UtcNow - jan1St1970).TotalMilliseconds);
        }
    }
}
