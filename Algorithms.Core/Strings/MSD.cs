using System;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>MSD</tt> class provides static methods for sorting an
    /// array of extended ASCII strings or integers using MSD radix sort.
    /// </summary>
    public class MSD
    {
        private const int BITS_PER_BYTE = 8;
        private const int BITS_PER_INT = 32; // each Java int is 32 bits 
        private const int R = 256; // extended ASCII alphabet size
        private const int CUTOFF = 15; // cutoff to insertion sort

        // do not instantiate
        private MSD() { }

        /// <summary>
        /// Rearranges the array of extended ASCII strings in ascending order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(string[] a)
        {
            var n = a.Length;
            var aux = new string[n];
            Sort(a, 0, n - 1, 0, aux);
        }

        /// <summary>
        /// return dth character of s, -1 if d = length of string
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
        /// sort from a[lo] to a[hi], starting at the dth character
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        /// <param name="aux"></param>
        private static void Sort(string[] a, int lo, int hi, int d, string[] aux)
        {

            // cutoff to insertion sort for small subarrays
            if (hi <= lo + CUTOFF)
            {
                Insertion(a, lo, hi, d);
                return;
            }

            // compute frequency counts
            var count = new int[R + 2];
            for (var i = lo; i <= hi; i++)
            {
                var c = CharAt(a[i], d);
                count[c + 2]++;
            }

            // transform counts to indicies
            for (var r = 0; r < R + 1; r++)
                count[r + 1] += count[r];

            // distribute
            for (var i = lo; i <= hi; i++)
            {
                var c = CharAt(a[i], d);
                aux[count[c + 1]++] = a[i];
            }

            // copy back
            for (var i = lo; i <= hi; i++)
                a[i] = aux[i - lo];


            // recursively sort for each character (excludes sentinel -1)
            for (var r = 0; r < R; r++)
                Sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1, aux);
        }


        /// <summary>
        /// insertion sort a[lo..hi], starting at dth character
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
            // assert v.substring(0, d).equals(w.substring(0, d));
            for (var i = d; i < Math.Min(v.Length, w.Length); i++)
            {
                if (v[i] < w[i]) return true;
                if (v[i] > w[i]) return false;
            }
            return v.Length < w.Length;
        }

        /// <summary>
        /// Rearranges the array of 32-bit integers in ascending order.
        /// Currently assumes that the integers are nonnegative.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(int[] a)
        {
            var n = a.Length;
            var aux = new int[n];
            Sort(a, 0, n - 1, 0, aux);
        }

        /// <summary>
        /// MSD sort from a[lo] to a[hi], starting at the dth byte
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        /// <param name="aux"></param>
        private static void Sort(int[] a, int lo, int hi, int d, int[] aux)
        {

            // cutoff to insertion sort for small subarrays
            if (hi <= lo + CUTOFF)
            {
                Insertion(a, lo, hi, d);
                return;
            }

            // compute frequency counts (need R = 256)
            var count = new int[R + 1];
            var mask = R - 1;   // 0xFF;
            var shift = BITS_PER_INT - BITS_PER_BYTE * d - BITS_PER_BYTE;
            for (var i = lo; i <= hi; i++)
            {
                var c = (a[i] >> shift) & mask;
                count[c + 1]++;
            }

            // transform counts to indicies
            for (var r = 0; r < R; r++)
                count[r + 1] += count[r];

            // distribute
            for (var i = lo; i <= hi; i++)
            {
                var c = (a[i] >> shift) & mask;
                aux[count[c]++] = a[i];
            }

            // copy back
            for (var i = lo; i <= hi; i++)
                a[i] = aux[i - lo];

            // no more bits
            if (d == 4) return;

            // recursively sort for each character
            if (count[0] > 0)
                Sort(a, lo, lo + count[0] - 1, d + 1, aux);
            for (var r = 0; r < R; r++)
                if (count[r + 1] > count[r])
                    Sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1, aux);
        }

        /// <summary>
        /// insertion sort a[lo..hi], starting at dth character
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        private static void Insertion(int[] a, int lo, int hi, int d)
        {
            for (var i = lo; i <= hi; i++)
                for (var j = i; j > lo && a[j] < a[j - 1]; j--)
                    Exch(a, j, j - 1);
        }

        /// <summary>
        /// exchange a[i] and a[j]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void Exch(int[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
    }
}
