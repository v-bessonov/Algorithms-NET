using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Heap</tt> class provides static methods for sorting an
    /// list of IComparable using heap sort.
    /// </summary>
    public class Heap : AbstractSort
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Heap() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="pq">the array to be sorted</param>
        public static void Sort(IList<IComparable> pq)
        {
            var n = pq.Count;
            for (var k = n / 2; k >= 1; k--)
                Sink(pq, k, n);
            while (n > 1)
            {
                Exch(pq, 1, n--);
                Sink(pq, 1, n);
            }
        }

        /***********************************************************************
         * Helper functions to restore the heap invariant.
         **********************************************************************/
        #region Helper
        /// <summary>
        /// Helper functions to restore the heap invariant.
        /// </summary>
        /// <param name="pq"></param>
        /// <param name="k"></param>
        /// <param name="n"></param>
        private static void Sink(IList<IComparable> pq, int k, int n)
        {
            while (2 * k <= n)
            {
                var j = 2 * k;
                if (j < n && Less(pq, j, j + 1)) j++;
                if (!Less(pq, k, j)) break;
                Exch(pq, k, j);
                k = j;
            }
        }


        private new static void Exch(IList<IComparable> pq, int i, int j)
        {
            var swap = pq[i - 1];
            pq[i - 1] = pq[j - 1];
            pq[j - 1] = swap;
        }

        /// <summary>
        /// Helper functions for comparisons and swaps.
        /// ndices are "off-by-one" to support 1-based indexing.
        /// </summary>
        /// <param name="pq"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static bool Less(IList<IComparable> pq, int i, int j)
        {
            return pq[i - 1].CompareTo(pq[j - 1]) < 0;
        }

        #endregion

    }
}
