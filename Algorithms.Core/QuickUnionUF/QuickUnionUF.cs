using System;

namespace Algorithms.Core.QuickUnionUF
{
    /// <summary>
    /// The <tt>QuickUnionUF</tt> class represents a union-find data structure.
    /// It supports the <em>union</em> and <em>find</em> operations, along with
    /// methods for determinig whether two objects are in the same component
    /// and the total number of components.
    /// <p>
    /// This implementation uses quick union.
    /// Initializing a data structure with <em>N</em> objects takes linear time.
    /// Afterwards, <em>union</em>, <em>find</em>, and <em>connected</em> take
    /// time linear time (in the worst case) and <em>count</em> takes constant
    /// time.
    /// <p>
    /// </summary>
    public class QuickUnionUF
    {
        private readonly int[] _parent;  // parent[i] = parent of i
        private int _count;     // number of components
        
        /// <summary>
        /// Initializes an empty union-find data structure with N isolated components 0 through N-1.
        /// throws ArgumentException if N less than 0
        /// </summary>
        /// <param name="n">N the number of objects</param>
        public QuickUnionUF(int n)
        {
            if (n < 0) throw new ArgumentException();
            _parent = new int[n];
            _count = n;
            for (var i = 0; i < n; i++)
            {
                _parent[i] = i;
            }
        }

       /// <summary>
        /// Returns the number of components.
       /// </summary>
        /// <returns>the number of components (between 1 and N)</returns>
        public int Count()
        {
            return _count;
        }

        /// <summary>
        /// Returns the component identifier for the component containing site <tt>p</tt>.
        /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// throws IndexOutOfRangeException unless 0 <= p < N
        /// <returns>the component identifier for the component containing site <tt>p</tt></returns>
        public int Find(int p)
        {
            Validate(p);
            while (p != _parent[p])
                p = _parent[p];
            return p;
        }

        /// <summary>
        /// validate that p is a valid index
        /// </summary>
        /// <param name="p"></param>
        private void Validate(int p)
        {
            var n = _parent.Length;
            if (p < 0 || p >= n)
            {
                throw new IndexOutOfRangeException("index " + p + " is not between 0 and " + n);
            }
        }

       /// <summary>
        /// Are the two sites <tt>p</tt> and <tt>q</tt> in the same component?
       /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// <param name="q">q the integer representing the other site</param>
        /// throws IndexOutOfRangeException unless both 0 <= p < N and 0 <= q < N
        /// <returns><tt>true</tt> if the sites <tt>p</tt> and <tt>q</tt> are in the same component, and <tt>false</tt> otherwise</returns>
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }


        /// <summary>
        ///  Merges the component containing site<tt>p</tt> with the component
        /// containing site <tt>q</tt>.
        /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// <param name="q">q the integer representing the other site</param>
        /// throws IndexOutOfRangeException unless both 0 <= p < N and 0 <= q < N
        public void Union(int p, int q)
        {
            var rootP = Find(p);
            var rootQ = Find(q);
            if (rootP == rootQ) return;
            _parent[rootP] = rootQ;
            _count--;
        }

    }
}
