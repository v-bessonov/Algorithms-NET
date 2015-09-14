using System;
using System.Collections.Generic;
using Algorithms.Core.StdLib;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>Quick3string</tt> class provides static methods for sorting an
    /// array of strings using 3-way radix quicksort.
    /// </summary>
    public class Quick3string
    {
        /// <summary>
        /// cutoff to insertion sort
        /// </summary>
        private const int CUTOFF = 15;

        /// <summary>
        /// do not instantiate
        /// </summary>
        private Quick3string() { }

        /// <summary>
        /// Rearranges the array of strings in ascending order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(string[] a)
        {
            StdRandom.Shuffle((IList<IComparable>) a);
            Sort(a, 0, a.Length - 1, 0);
        }

        /// <summary>
        /// return the dth character of s, -1 if d = length of s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static int CharAt(string s, int d)
        {
            //assert d >= 0 && d <= s.length();
            if (d == s.Length) return -1;
            return s[d];
        }


        /// <summary>
        /// 3-way string quicksort a[lo..hi] starting at dth character
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        private static void Sort(string[] a, int lo, int hi, int d)
        {

            // cutoff to insertion sort for small subarrays
            if (hi <= lo + CUTOFF)
            {
                Insertion(a, lo, hi, d);
                return;
            }

            int lt = lo, gt = hi;
            var v = CharAt(a[lo], d);
            var i = lo + 1;
            while (i <= gt)
            {
                var t = CharAt(a[i], d);
                if (t < v) Exch(a, lt++, i++);
                else if (t > v) Exch(a, i, gt--);
                else i++;
            }

            // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
            Sort(a, lo, lt - 1, d);
            if (v >= 0) Sort(a, lt, gt, d + 1);
            Sort(a, gt + 1, hi, d);
        }

        /// <summary>
        /// sort from a[lo] to a[hi], starting at the dth character
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        private static void Insertion(string[] a, int lo, int hi, int d)
        {
            for (var i = lo; i <= hi; i++)
                for (var j = i; j > lo && Less(a[j], a[j - 1], d); j--)
                    Exch(a, j, j - 1);
        }

        /// <summary>
        /// exchange a[i] and a[j]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void Exch(string[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }


        /// <summary>
        /// is v less than w, starting at character d
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static bool Less(string v, string w, int d)
        {
            //assert v.substring(0, d).equals(w.substring(0, d));
            for (var i = d; i < Math.Min(v.Length, w.Length); i++)
            {
                if (v[i] < w[i]) return true;
                if (v[i] > w[i]) return false;
            }
            return v.Length < w.Length;
        }

        /// <summary>
        /// is the array sorted
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static bool IsSorted(string[] a)
        {
            for (var i = 1; i < a.Length; i++)
                if (string.Compare(a[i], a[i - 1], StringComparison.Ordinal) < 0) return false;
            return true;
        }
    }
}
