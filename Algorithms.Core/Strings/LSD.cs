namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>LSD</tt> class provides static methods for sorting an
    /// array of W-character strings or 32-bit integers using LSD radix sort.
    /// </summary>
    public class LSD
    {
        private const int BITS_PER_BYTE = 8;

        // do not instantiate
        private LSD() { }

        /// <summary>
        /// Rearranges the array of W-character strings in ascending order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        /// <param name="w">w the number of characters per string</param>
        public static void Sort(string[] a, int w)
        {
            var n = a.Length;
            const int r = 256; // extend ASCII alphabet size
            var aux = new string[n];

            for (var d = w - 1; d >= 0; d--)
            {
                // sort by key-indexed counting on dth character

                // compute frequency counts
                var count = new int[r + 1];
                for (var i = 0; i < n; i++)
                    count[a[i][d] + 1]++;

                // compute cumulates
                for (var j = 0; j < r; j++)
                    count[j + 1] += count[j];

                // move data
                for (var i = 0; i < n; i++)
                    aux[count[a[i][d]]++] = a[i];

                // copy back
                for (var i = 0; i < n; i++)
                    a[i] = aux[i];
            }
        }

        /// <summary>
        /// Rearranges the array of 32-bit integers in ascending order.
        /// This is about 2-3x faster than Arrays.sort()
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(int[] a)
        {
            const int bits = 32; // each int is 32 bits 
            const int w = bits / BITS_PER_BYTE; // each int is 4 bytes
            const int r = 1 << BITS_PER_BYTE; // each bytes is between 0 and 255
            const int mask = r - 1; // 0xFF

            var n = a.Length;
            var aux = new int[n];

            for (var d = 0; d < w; d++)
            {

                // compute frequency counts
                var count = new int[r + 1];
                for (var i = 0; i < n; i++)
                {
                    var c = (a[i] >> BITS_PER_BYTE * d) & mask;
                    count[c + 1]++;
                }

                // compute cumulates
                for (var ri = 0; ri < r; ri++)
                    count[ri + 1] += count[ri];

                // for most significant byte, 0x80-0xFF comes before 0x00-0x7F
                if (d == w - 1)
                {
                    var shift1 = count[r] - count[r / 2];
                    var shift2 = count[r / 2];
                    for (var ri = 0; ri < r / 2; ri++)
                        count[ri] += shift1;
                    for (var ri = r / 2; ri < r; ri++)
                        count[ri] -= shift2;
                }

                // move data
                for (var i = 0; i < n; i++)
                {
                    var c = (a[i] >> BITS_PER_BYTE * d) & mask;
                    aux[count[c]++] = a[i];
                }

                // copy back
                for (var i = 0; i < n; i++)
                    a[i] = aux[i];
            }
        }
    }
}
