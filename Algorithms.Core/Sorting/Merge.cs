using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Insertion</tt> class provides static methods for sorting an
    /// list of IComparable using merge sort.
    /// </summary>
    public class Merge : AbstractSort
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Merge() { }

        /// <summary>
        /// stably merge a[lo .. mid] with a[mid+1 ..hi] using aux[lo .. hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aux"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        private static void MergeLists(IList<IComparable> a, IList<IComparable> aux, int lo, int mid, int hi)
        {
            // precondition: a[lo .. mid] and a[mid+1 .. hi] are sorted subarrays

            // copy to aux[]
            for (var k = lo; k <= hi; k++)
            {
                aux[k] = a[k];
            }

            // merge back to a[]
            int i = lo, j = mid + 1;
            for (var k = lo; k <= hi; k++)
            {
                if (i > mid) a[k] = aux[j++];   // this copying is unnecessary
                else if (j > hi) a[k] = aux[i++];
                else if (Less(aux[j], aux[i])) a[k] = aux[j++];
                else a[k] = aux[i++];
            }

            // postcondition: a[lo .. hi] is sorted
        }

        /// <summary>
        /// mergesort a[lo..hi] using auxiliary array aux[lo..hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aux"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void Sort(IList<IComparable> a, IList<IComparable> aux, int lo, int hi)
        {
            if (hi <= lo) return;
            var mid = lo + (hi - lo) / 2;
            Sort(a, aux, lo, mid);
            Sort(a, aux, mid + 1, hi);
            MergeLists(a, aux, lo, mid, hi);
        }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            IList<IComparable> aux = new List<IComparable>(new IComparable[a.Count]);
            Sort(a, aux, 0, a.Count - 1);
        }

        #region Index mergesort
        /// <summary>
        /// stably merge a[lo .. mid] with a[mid+1 .. hi] using aux[lo .. hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="index"></param>
        /// <param name="aux"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        private static void MergeLists(IList<IComparable> a, IList<int> index, IList<int> aux, int lo, int mid, int hi)
        {

            // copy to aux[]
            for (var k = lo; k <= hi; k++)
            {
                aux[k] = index[k];
            }

            // merge back to a[]
            int i = lo, j = mid + 1;
            for (var k = lo; k <= hi; k++)
            {
                if (i > mid) index[k] = aux[j++];
                else if (j > hi) index[k] = aux[i++];
                else if (Less(a[aux[j]], a[aux[i]])) index[k] = aux[j++];
                else index[k] = aux[i++];
            }
        }

        /// <summary>
        /// Returns a permutation that gives the elements in the list in ascending order.
        /// do not change the original list a
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        /// <returns>a permutation <tt>p[]</tt> such that <tt>a[p[0]]</tt>, <tt>a[p[1]]</tt>,..., <tt>a[p[N-1]]</tt> are in ascending order</returns>
        public static int[] IndexSort(IList<IComparable> a)
        {
            var n = a.Count;
            var index = new int[n];
            for (var i = 0; i < n; i++)
                index[i] = i;

            var aux = new int[n];
            Sort(a, index, aux, 0, n - 1);
            return index;
        }

        /// <summary>
        /// mergesort a[lo..hi] using auxiliary array aux[lo..hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="index"></param>
        /// <param name="aux"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void Sort(IList<IComparable> a, IList<int> index, IList<int> aux, int lo, int hi)
        {
            if (hi <= lo) return;
            var mid = lo + (hi - lo) / 2;
            Sort(a, index, aux, lo, mid);
            Sort(a, index, aux, mid + 1, hi);
            MergeLists(a, index, aux, lo, mid, hi);
        }
        #endregion
    }
}
