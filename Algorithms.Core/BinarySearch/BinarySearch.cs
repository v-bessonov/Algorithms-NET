namespace Algorithms.Core.BinarySearch
{
    /// <summary>
    /// The <tt>BinarySearch</tt> class provides a static method for binary
    /// searching for an integer in a sorted array of integers.
    /// 
    /// The <em>rank</em> operations takes logarithmic time in the worst case.
    /// 
    /// </summary>
    public class BinarySearch
    {
        /// <summary>
        /// This class should not be instantiated.
        /// </summary>
        private BinarySearch() { }
        /// <summary>
        /// Searches for the integer key in the sorted array a[].
        /// </summary>
        /// <param name="key">key the search key</param>
        /// <param name="a">a the array of integers, must be sorted in ascending order</param>
        /// <returns>index of key in array a[] if present; -1 if not present</returns>
        public static int Rank(int key, int[] a)
        {
            var lo = 0;
            var hi = a.Length - 1;
            while (lo <= hi)
            {
                // Key is in a[lo..hi] or not present.
                var mid = lo + (hi - lo) / 2;
                if (key < a[mid]) hi = mid - 1;
                else if (key > a[mid]) lo = mid + 1;
                else return mid;
            }
            return -1;
        }
    }
}
