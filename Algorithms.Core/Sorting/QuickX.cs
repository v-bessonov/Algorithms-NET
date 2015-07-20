using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>QuickX</tt> class provides static methods for sorting an
    /// array using an optimized version of quicksort (using Bentley-McIlroy
    /// 3-way partitioning, Tukey's ninther, and cutoff to insertion sort).
    /// </summary>
    public class QuickX : AbstractSort
    {
        private const int CUTOFF = 8; // cutoff to insertion sort, must be >= 1

        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private QuickX() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">the array to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            Sort(a, 0, a.Count - 1);
        }

        private static void Sort(IList<IComparable> a, int lo, int hi)
        {
            var n = hi - lo + 1;

            // cutoff to insertion sort
            if (n <= CUTOFF)
            {
                InsertionSort(a, lo, hi);
                return;
            }

            // use median-of-3 as partitioning element
            if (n <= 40)
            {
                var m = Median3(a, lo, lo + n / 2, hi);
                Exch(a, m, lo);
            }

                // use Tukey ninther as partitioning element
            else
            {
                var eps = n / 8;
                var mid = lo + n / 2;
                var m1 = Median3(a, lo, lo + eps, lo + eps + eps);
                var m2 = Median3(a, mid - eps, mid, mid + eps);
                var m3 = Median3(a, hi - eps - eps, hi - eps, hi);
                var ninther = Median3(a, m1, m2, m3);
                Exch(a, ninther, lo);
            }

            // Bentley-McIlroy 3-way partitioning
            int i = lo, j = hi + 1;
            int p = lo, q = hi + 1;
            var v = a[lo];
            while (true)
            {
                while (Less(a[++i], v))
                    if (i == hi) break;
                while (Less(v, a[--j]))
                    if (j == lo) break;

                // pointers cross
                if (i == j && Eq(a[i], v))
                    Exch(a, ++p, i);
                if (i >= j) break;

                Exch(a, i, j);
                if (Eq(a[i], v)) Exch(a, ++p, i);
                if (Eq(a[j], v)) Exch(a, --q, j);
            }


            i = j + 1;
            for (var k = lo; k <= p; k++) Exch(a, k, j--);
            for (var k = hi; k >= q; k--) Exch(a, k, i++);

            Sort(a, lo, j);
            Sort(a, i, hi);
        }


        /// <summary>
        /// sort from a[lo] to a[hi] using insertion sort
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void InsertionSort(IList<IComparable> a, int lo, int hi)
        {
            for (var i = lo; i <= hi; i++)
                for (var j = i; j > lo && Less(a[j], a[j - 1]); j--)
                    Exch(a, j, j - 1);
        }


        /// <summary>
        /// return the index of the median element among a[i], a[j], and a[k]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static int Median3(IList<IComparable> a, int i, int j, int k)
        {
            return (Less(a[i], a[j]) ?
                   (Less(a[j], a[k]) ? j : Less(a[i], a[k]) ? k : i) :
                   (Less(a[k], a[j]) ? j : Less(a[k], a[i]) ? k : i));
        }


    }
}
