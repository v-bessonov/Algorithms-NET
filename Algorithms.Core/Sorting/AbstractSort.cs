using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    public abstract class AbstractSort
    {
        
        #region Helper sorting functions



        /// <summary>
        /// is v less w ?
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        protected static bool Less(IComparable v, IComparable w)
        {
            return (v.CompareTo(w) < 0);
        }


        /// <summary>
        /// does v == w ?
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        protected static bool Eq(IComparable v, IComparable w)
        {
            return (v.CompareTo(w) == 0);
        }

        /// <summary>
        /// exchange a[i] and a[j]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        protected static void Exch(IList<IComparable> a, int i, int j)
        {
            var swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        /// <summary>
        /// exchange a[i] and a[j]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        protected static void Exch(IList<int> a, int i, int j)
        {
            var swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        /// <summary>
        /// is the list sorted
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool IsSorted(IList<IComparable> a)
        {
            return IsSorted(a, 0, a.Count - 1);
        }

        /// <summary>
        /// is the array sorted from a[lo] to a[hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        public static bool IsSorted(IList<IComparable> a, int lo, int hi)
        {
            for (var i = lo + 1; i <= hi; i++)
                if (Less(a[i], a[i - 1])) return false;
            return true;
        }

        /// <summary>
        /// is the array h-sorted?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        private static bool IsHsorted(IList<IComparable> a, int h)
        {
            for (var i = h; i < a.Count; i++)
                if (Less(a[i], a[i - h])) return false;
            return true;
        }

        /// <summary>
        /// print array to standard output
        /// </summary>
        /// <param name="a"></param>
        public static void Show(IList<IComparable> a)
        {
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
    }

    public abstract class AbstractSort<T> where T : class
    {
        #region Helper sorting functions
        /// <summary>
        /// is v less w ?
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="comparator"></param>
        /// <returns></returns>
        protected static bool Less(T v, T w, IComparer<T> comparator)
        {
            return (comparator.Compare(v, w) < 0);
        }

        /// <summary>
        /// exchange a[i] and a[j]  (for indirect sort)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        protected static void Exch(IList<T> a, int i, int j)
        {
            var swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        /// <summary>
        /// Is the list sorted from a[lo] to a[hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="comparator"></param>
        /// <returns></returns>
        protected static bool IsSorted(IList<T> a, int lo, int hi, IComparer<T> comparator)
        {
            for (var i = lo + 1; i <= hi; i++)
                if (Less(a[i], a[i - 1], comparator)) return false;
            return true;
        }

        /// <summary>
        /// is the array h-sorted?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="h"></param>
        /// <param name="comparator"></param>
        /// <returns></returns>
        protected static bool IsHsorted(IList<T> a, int h, IComparer<T> comparator)
        {
            for (var i = h; i < a.Count; i++)
                if (Less(a[i], a[i - h],comparator)) return false;
            return true;
        }

        /// <summary>
        /// Check if list is sorted
        /// </summary>
        /// <param name="a"></param>
        /// <param name="comparator"></param>
        /// <returns></returns>
        public static bool IsSorted(IList<T> a, IComparer<T> comparator)
        {
            return IsSorted(a, 0, a.Count - 1, comparator);
        }



        /// <summary>
        /// print list to standard output
        /// </summary>
        /// <param name="a"></param>
        public static void Show(IList<T> a)
        {
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
    }
}
