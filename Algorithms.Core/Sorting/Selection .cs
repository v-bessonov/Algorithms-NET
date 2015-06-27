using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Insertion</tt> class provides static methods for sorting an
    /// list of IComparable using selection sort.
    /// </summary>
    public class Selection : AbstractSort
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Selection() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a list of IComparable to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            var n = a.Count;
            for (var i = 0; i < n; i++)
            {
                var min = i;
                for (var j = i + 1; j < n; j++)
                {
                    if (Less(a[j], a[min])) min = j;
                }
                Exch(a, i, min);
            }
        }


    }

    /// <summary>
    /// The <tt>Insertion</tt> class provides static methods for sorting an
    /// list of entities type of T with IComparer<T>  using selection sort. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Selection<T> : AbstractSort<T> where T : class
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Selection() { }

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
                var min = i;
                for (var j = i + 1; j < n; j++)
                {
                    if (Less(a[j], a[min], comparator)) min = j;
                }
                Exch(a, i, min);
            }
        }
    }
}
