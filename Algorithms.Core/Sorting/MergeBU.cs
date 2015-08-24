using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>MergeBU</tt> class provides static methods for sorting an
    /// list of IComparable using bottom-up mergesort.
    /// </summary>
    public class MergeBU : AbstractSort
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private MergeBU() { }

        /// <summary>
        /// stably merge a[lo..mid] with a[mid+1..hi] using aux[lo..hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aux"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        private static void Merge(IList<IComparable> a, IList<IComparable> aux, int lo, int mid, int hi)
        {

            // copy to aux[]
            for (var k = lo; k <= hi; k++)
            {
                aux[k] = a[k];
            }

            // merge back to a[]
            int i = lo, j = mid + 1;
            for (var k = lo; k <= hi; k++)
            {
                if (i > mid) a[k] = aux[j++];  // this copying is unneccessary
                else if (j > hi) a[k] = aux[i++];
                else if (Less(aux[j], aux[i])) a[k] = aux[j++];
                else a[k] = aux[i++];
            }

        }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            var count = a.Count;
            var aux = new IComparable[count];
            for (var n = 1; n < count; n = n + n)
            {
                for (var i = 0; i < count - n; i += n + n)
                {
                    var lo = i;
                    var m = i + n - 1;
                    var hi = Math.Min(i + n + n - 1, count - 1);
                    Merge(a, aux, lo, m, hi);
                }
            }
        }
    }
}
