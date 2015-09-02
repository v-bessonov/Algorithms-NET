using System;

namespace Algorithms.Core.QuickUnionUF
{
    /// <summary>
    /// The <tt>WeightedQuickUnionUF</tt> class represents a union-find data structure.
    /// It supports the <em>union</em> and <em>find</em> operations, along with
    /// methods for determinig whether two objects are in the same component
    /// and the total number of components.
    /// <p>
    /// This implementation uses weighted quick union by size (without path compression).
    /// Initializing a data structure with <em>N</em> objects takes linear time.
    /// Afterwards, <em>union</em>, <em>find</em>, and <em>connected</em> take
    /// logarithmic time (in the worst case) and <em>count</em> takes constant
    /// time.
    /// </p>
    /// </summary>
    public class WeightedQuickUnionUF
    {
        private readonly int[] _parent;   // parent[i] = parent of i
        private readonly int[] _size;     // size[i] = number of objects in subtree rooted at i
        private int _count;      // number of components

        /// <summary>
        /// Initializes an empty union-find data structure with N isolated components 0 through N-1.
        /// </summary>
        /// <param name="n">n the number of objects</param>
        public WeightedQuickUnionUF(int n)
        {
            _count = n;
            _parent = new int[n];
            _size = new int[n];
            for (var i = 0; i < n; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
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
        /// <returns>the component identifier for the component containing site <tt>p</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless 0 &lt;= p &lt; N</exception>
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
        /// <returns></returns>
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }


        /// <summary>
        /// Merges the component containing site<tt>p</tt> with the component
        /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// <param name="q">q the integer representing the other site</param>
        public void Union(int p, int q)
        {
            var rootP = Find(p);
            var rootQ = Find(q);
            if (rootP == rootQ) return;

            // make smaller root point to larger one
            if (_size[rootP] < _size[rootQ])
            {
                _parent[rootP] = rootQ;
                _size[rootQ] += _size[rootP];
            }
            else
            {
                _parent[rootQ] = rootP;
                _size[rootP] += _size[rootQ];
            }
            _count--;
        }
    }
}
