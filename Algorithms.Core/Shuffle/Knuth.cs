using System;
using System.Collections.Generic;

namespace Algorithms.Core.Shuffle
{
    /// <summary>
    // The <tt>Knuth</tt> class provides a client for reading in a 
    /// sequence of strings and <em>shuffling</em> them using the Knuth (or Fisher-Yates)
    /// shuffling algorithm. This algorithm guarantees to rearrange the
    /// elements in uniformly random order, under
    /// the assumption that random.NextDouble() generates independent and
    /// uniformly distributed numbers between 0 and 1.
    /// </summary>
    public class Knuth
    {
        // this class should not be instantiated
        private Knuth() { }


        /// <summary>
        /// Rearranges an array of objects in uniformly random order
        /// (under the assumption that <tt>random.NextDouble()</tt> generates independent
        /// and uniformly distributed numbers between 0 and 1).
        /// </summary>
        /// <param name="a">a the array to be shuffled</param>
        public static void Shuffle(IList<object> a)
        {
            var random = new Random();
            var n = a.Count;
            for (var i = 0; i < n; i++)
            {
                // choose index uniformly in [i, N-1]
                var r = i + (int)(random.NextDouble() * (n - i));
                var swap = a[r];
                a[r] = a[i];
                a[i] = swap;
            }
        }
    }
}
