using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Insertion</tt> class provides static methods for sorting an
    /// list of IComparable using insertion sort.
    /// </summary>
    public class Insertion : AbstractSort
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Insertion() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            var n = a.Count;
            for (var i = 0; i < n; i++)
            {
                for (var j = i; j > 0 && Less(a[j], a[j - 1]); j--)
                {
                    Exch(a, j, j - 1);
                }
            }
        }


        /// <summary>
        /// Rearranges the subarray a[lo..hi] in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        /// <param name="lo">lo left endpoint</param>
        /// <param name="hi">hi right endpoint</param>
        public static void Sort(IList<IComparable> a, int lo, int hi)
        {
            for (var i = lo; i <= hi; i++)
            {
                for (var j = i; j > lo && Less(a[j], a[j - 1]); j--)
                {
                    Exch(a, j, j - 1);
                }
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

            for (var i = 0; i < n; i++)
                for (var j = i; j > 0 && Less(a[index[j]], a[index[j - 1]]); j--)
                    Exch(index, j, j - 1);

            return index;
        }

    }

    /// <summary>
    /// The <tt>Insertion</tt> class provides static methods for sorting an
    /// list of entities type of T with IComparer<T>  using insertion sort. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Insertion<T> : AbstractSort<T> where T : class
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Insertion() { }


        /// <summary>
        /// Rearranges the list in ascending order, using a comparator.
        /// </summary>
        /// <param name="a">list of entities type of T</param>
        /// <param name="comparator">comparator IComparer<T></param>
        public static void Sort(IList<T> a, IComparer<T> comparator)
        {
            var n = a.Count;
            for (var i = 0; i < n; i++)
            {
                for (var j = i; j > 0 && Less(a[j], a[j - 1], comparator); j--)
                {
                    Exch(a, j, j - 1);
                }
            }
        }

        /// <summary>
        /// Rearranges the subarray a[lo..hi] in ascending order, using a comparator.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="comparator"></param>
        public static void Sort(IList<T> a, int lo, int hi, IComparer<T> comparator)
        {
            for (var i = lo; i <= hi; i++)
            {
                for (var j = i; j > lo && Less(a[j], a[j - 1], comparator); j--)
                {
                    Exch(a, j, j - 1);
                }
            }
        }

    }
}
