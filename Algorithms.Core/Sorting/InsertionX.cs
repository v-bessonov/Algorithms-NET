using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>InsertionX</tt> class provides static methods for sorting a
    /// list of IComparable using an optimized version of insertion sort (with half exchanges
    /// and a sentinel).
    /// </summary>
    public class InsertionX : AbstractSort
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private InsertionX() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            var n = a.Count;
            // put smallest element in position to serve as sentinel
            for (var i = n - 1; i > 0; i--)
                if (Less(a[i], a[i - 1])) Exch(a, i, i - 1);

            // insertion sort with half-exchanges
            for (var i = 2; i < n; i++)
            {
                var v = a[i];
                var j = i;
                while (Less(v, a[j - 1]))
                {
                    a[j] = a[j - 1];
                    j--;
                }
                a[j] = v;
            }
        }

    }

    /// <summary>
    /// The <tt>InsertionX</tt> class provides static methods for sorting a
    /// list of entities type of T with IComparer<T> using an optimized version of insertion sort (with half exchanges
    /// and a sentinel).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InsertionX<T> : AbstractSort<T> where T : class
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private InsertionX() { }


        /// <summary>
        /// Rearranges the list in ascending order, using a comparator.
        /// </summary>
        /// <param name="a">list of entities type of T</param>
        /// <param name="comparator">comparator IComparer<T></param>
        public static void Sort(IList<T> a, IComparer<T> comparator)
        {

            var n = a.Count;
            // put smallest element in position to serve as sentinel
            for (var i = n - 1; i > 0; i--)
                if (Less(a[i], a[i - 1],comparator)) Exch(a, i, i - 1);

            // insertion sort with half-exchanges
            for (var i = 2; i < n; i++)
            {
                var v = a[i];
                var j = i;
                while (Less(v, a[j - 1],comparator))
                {
                    a[j] = a[j - 1];
                    j--;
                }
                a[j] = v;
            }
        }

    }
}
