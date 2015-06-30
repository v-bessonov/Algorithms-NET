using System;
using System.Collections.Generic;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Insertion</tt> class provides static methods for sorting an
    /// list of IComparable using merge sort.
    /// </summary>
    public class Quick : AbstractSort
    {

        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Quick() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            StdRandom.Shuffle(a);
            Sort(a, 0, a.Count - 1);
        }

        /// <summary>
        /// quicksort the subarray from a[lo] to a[hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void Sort(IList<IComparable> a, int lo, int hi)
        {
            if (hi <= lo) return;
            var j = Partition(a, lo, hi);
            Sort(a, lo, j - 1);
            Sort(a, j + 1, hi);
        }

        /// <summary>
        ///  partition the subarray a[lo..hi] so that a[lo..j-1] <= a[j] <= a[j+1..hi]
        ///  and return the index j.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private static int Partition(IList<IComparable> a, int lo, int hi)
        {
            var i = lo;
            var j = hi + 1;
            var v = a[lo];
            while (true)
            {

                // find item on lo to swap
                while (Less(a[++i], v))
                    if (i == hi) break;

                // find item on hi to swap
                while (Less(v, a[--j]))
                    if (j == lo) break;      // redundant since a[lo] acts as sentinel

                // check if pointers cross
                if (i >= j) break;

                Exch(a, i, j);
            }

            // put partitioning item v at a[j]
            Exch(a, lo, j);

            // now, a[lo .. j-1] <= a[j] <= a[j+1 .. hi]
            return j;
        }

        /// <summary>
        /// Rearranges the array so that a[k] contains the kth smallest key;
        /// a[0] through a[k-1] are less than (or equal to) a[k]; and
        /// a[k+1] through a[N-1] are greater than (or equal to) a[k].
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IComparable Select(IList<IComparable> a, int k)
        {
            if (k < 0 || k >= a.Count)
            {
                throw new IndexOutOfRangeException("Selected element out of bounds");
            }
            StdRandom.Shuffle(a);
            int lo = 0, hi = a.Count - 1;
            while (hi > lo)
            {
                var i = Partition(a, lo, hi);
                if (i > k) hi = i - 1;
                else if (i < k) lo = i + 1;
                else return a[i];
            }
            return a[lo];
        }

    }
}
