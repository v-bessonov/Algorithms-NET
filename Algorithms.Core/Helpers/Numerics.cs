using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Algorithms.Core.Helpers
{
    public class Numerics
    {
        public static long Prime(long bound1, long bound2)
        {
            var randomLong = GetNextInt64(bound1, bound2);
            var prime = 0L;
            // bool isPrime = true;
            for (var i = randomLong; i <= bound2; i++)
            {
                var isPrime = true; // Move initialization to here
                for (long j = 2; j < i; j++) // you actually only need to check up to sqrt(i)
                {
                    if (i % j == 0) // you don't need the first condition
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    prime = i;
                    break;
                }
                // isPrime = true;
            }
            return prime;
        }


        public static List<long> Primes(long bound1, long bound2)
        {

            var primes = new List<long>();
            // bool isPrime = true;
            for (var i = bound1; i <= bound2; i++)
            {
                var isPrime = true; // Move initialization to here
                for (long j = 2; j < i; j++) // you actually only need to check up to sqrt(i)
                {
                    if (i % j == 0) // you don't need the first condition
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(i);
                }
                // isPrime = true;
            }
            return primes;
        }

        public static long GetNextInt64(long low, long hi)
        {
            var rng = new RNGCryptoServiceProvider();
            if (low >= hi)
                throw new ArgumentException("low must be < hi");
            var buf = new byte[8];

            //Generate a random double
            rng.GetBytes(buf);
            var num = Math.Abs(BitConverter.ToDouble(buf, 0));

            //We only use the decimal portion
            num = num - Math.Truncate(num);

            //Return a number within range
            return (long)(num * ((double)hi - (double)low) + low);
        }

        public static long Prime()
        {
            var bytes = new byte[sizeof(long)];
            var gen = new RNGCryptoServiceProvider();
            var prime = 0L;
            while (prime <= 0L)
            {
                gen.GetBytes(bytes);
                prime = BitConverter.ToInt64(bytes, 0);
            }
            return prime;
        }
    }
}
