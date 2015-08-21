using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    /// <summary>
    /// The <tt>Shell</tt> class provides static methods for sorting an
    /// list of IComparable using Shellsort with Knuth's increment sequence (1, 4, 13, 40, ...).
    /// </summary>
    public class Shell : AbstractSort
    {

        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private Shell() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(IList<IComparable> a)
        {
            var n = a.Count;

            // 3x+1 increment sequence:  1, 4, 13, 40, 121, 364, 1093, ... 
            var h = 1;
            while (h < n / 3) h = 3 * h + 1;

            while (h >= 1)
            {
                // h-sort the array
                for (var i = h; i < n; i++)
                {
                    for (var j = i; j >= h && Less(a[j], a[j - h]); j -= h)
                    {
                        Exch(a, j, j - h);
                    }
                }
                h /= 3;
            }
        }

    }
}
