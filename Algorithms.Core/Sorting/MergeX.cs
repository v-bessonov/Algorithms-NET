using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>MergeBU</tt> class provides static methods for sorting an
    /// list of IComparable using an optimized version of mergesort.
    /// </summary>
    public class MergeX : AbstractSort
    {
        private static readonly int CUTOFF = 7;  // cutoff to insertion sort

        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private MergeX() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        private static void Merge(IList<IComparable> src, IList<IComparable> dst, int lo, int mid, int hi)
        {

            // precondition: src[lo .. mid] and src[mid+1 .. hi] are sorted subarrays

            int i = lo, j = mid + 1;
            for (var k = lo; k <= hi; k++)
            {
                if (i > mid) dst[k] = src[j++];
                else if (j > hi) dst[k] = src[i++];
                else if (Less(src[j], src[i])) dst[k] = src[j++];   // to ensure stability
                else dst[k] = src[i++];
            }

            // postcondition: dst[lo .. hi] is sorted subarray
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void Sort(IList<IComparable> src, IList<IComparable> dst, int lo, int hi)
        {
            // if (hi <= lo) return;
            if (hi <= lo + CUTOFF)
            {
                InsertionSort(dst, lo, hi);
                return;
            }
            var mid = lo + (hi - lo) / 2;
            Sort(dst, src, lo, mid);
            Sort(dst, src, mid + 1, hi);

            // if (!less(src[mid+1], src[mid])) {
            //    for (int i = lo; i <= hi; i++) dst[i] = src[i];
            //    return;
            // }

            // using System.arraycopy() is a bit faster than the above loop
            if (!Less(src[mid + 1], src[mid]))
            {
                Array.Copy(src.ToArray(), lo, dst.ToArray(), lo, hi - lo + 1);
                return;
            }

            Merge(src, dst, lo, mid, hi);
        }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            var aux = a.ToArray().Clone() as IComparable[];
            Sort(aux, a, 0, a.Count - 1);
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
    }

    public class MergeX<T> : AbstractSort<T> where T : class
    {
        private static readonly int CUTOFF = 7;  // cutoff to insertion sort

        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private MergeX() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        /// <param name="comparator"></param>
        private static void Merge(IList<T> src, IList<T> dst, int lo, int mid, int hi, IComparer<T> comparator)
        {

            // precondition: src[lo .. mid] and src[mid+1 .. hi] are sorted subarrays

            int i = lo, j = mid + 1;
            for (var k = lo; k <= hi; k++)
            {
                if (i > mid) dst[k] = src[j++];
                else if (j > hi) dst[k] = src[i++];
                else if (Less(src[j], src[i], comparator)) dst[k] = src[j++];
                else dst[k] = src[i++];
            }

            // postcondition: dst[lo .. hi] is sorted subarray
        }

        /// <summary>
        /// Rearranges the list in ascending order, using a comparator.
        /// </summary>
        /// <param name="a">list of entities type of T</param>
        /// <param name="comparator">comparator IComparer<T></param>
        public static void Sort(IList<T> a, IComparer<T> comparator)
        {
            var aux = a.ToArray().Clone() as IList<T>;
            Sort(aux, a, 0, a.Count - 1, comparator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="comparator"></param>
        private static void Sort(IList<T> src, IList<T>  dst, int lo, int hi, IComparer<T> comparator)
        {
            // if (hi <= lo) return;
            if (hi <= lo + CUTOFF)
            {
                InsertionSort(dst, lo, hi, comparator);
                return;
            }
            var mid = lo + (hi - lo) / 2;
            Sort(dst, src, lo, mid, comparator);
            Sort(dst, src, mid + 1, hi, comparator);

            // using System.arraycopy() is a bit faster than the above loop
            if (!Less(src[mid + 1], src[mid], comparator))
            {
                Array.Copy(src.ToArray(), lo, dst.ToArray(), lo, hi - lo + 1);
                return;
            }

            Merge(src, dst, lo, mid, hi, comparator);
        }

        /// <summary>
        /// sort from a[lo] to a[hi] using insertion sort
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="comparator"></param>
        private static void InsertionSort(IList<T> a, int lo, int hi, IComparer<T> comparator)
        {
            for (var i = lo; i <= hi; i++)
                for (var j = i; j > lo && Less(a[j], a[j - 1], comparator); j--)
                    Exch(a, j, j - 1);
        }
    }
}
