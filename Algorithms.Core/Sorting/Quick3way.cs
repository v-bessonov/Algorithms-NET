using System;
using System.Collections.Generic;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Quick3way</tt> class provides static methods for sorting an
    /// list of IComparable using quick sort with 3-way partitioning.
    /// </summary>
    public class Quick3way : AbstractSort
    {

        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Quick3way() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a"> the array to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            StdRandom.Shuffle(a);
            Sort(a, 0, a.Count - 1);
        }

        /// <summary>
        /// quicksort the subarray a[lo .. hi] using 3-way partitioning
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void Sort(IList<IComparable> a, int lo, int hi)
        {
            if (hi <= lo) return;
            int lt = lo, gt = hi;
            var v = a[lo];
            var i = lo;
            while (i <= gt)
            {
                var cmp = a[i].CompareTo(v);
                if (cmp < 0) Exch(a, lt++, i++);
                else if (cmp > 0) Exch(a, i, gt--);
                else i++;
            }

            // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
            Sort(a, lo, lt - 1);
            Sort(a, gt + 1, hi);
        }

    }
}
