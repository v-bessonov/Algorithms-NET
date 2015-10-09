using System;

namespace Algorithms.Core.QuickUnionUF
{
    ///  The <tt>UF</tt> class represents a <em>union-find data type</em>
    ///  (also known as the <em>disjoint-sets data type</em>).
    ///  It supports the <em>union</em> and <em>find</em> operations,
    ///  along with a <em>connected</em> operation for determining whether
    ///  two sites are in the same component and a <em>count</em> operation that
    ///  returns the total number of components.
    ///  <p/>
    ///  The union-find data type models connectivity among a set of <em>N</em>
    ///  sites, named 0 through <em>N</em> &amp;ndash; 1.
    ///  The <em>is-connected-to</em> relation must be an 
    ///  <em>equivalence relation</em>:
    ///  <ul>
    ///  <p/><li> <em>Reflexive</em>: <em>p</em> is connected to <em>p</em>.</li>
    ///  <p/><li> <em>Symmetric</em>: If <em>p</em> is connected to <em>q</em>,
    ///          then <em>q</em> is connected to <em>p</em>.</li>
    ///  <p/><li> <em>Transitive</em>: If <em>p</em> is connected to <em>q</em>
    ///          and <em>q</em> is connected to <em>r</em>, then
    ///          <em>p</em> is connected to <em>r</em>.</li>
    ///  </ul>
    ///  <p/>
    ///  An equivalence relation partitions the sites into
    ///  <em>equivalence classes</em> (or <em>components</em>). In this case,
    ///  two sites are in the same component if and only if they are connected.
    ///  Both sites and components are identified with integers between 0 and
    ///  <em>N</em> &amp;ndash; 1. 
    ///  Initially, there are <em>N</em> components, with each site in its
    ///  own component.  The <em>component identifier</em> of a component
    ///  (also known as the <em>root</em>, <em>canonical element</em>, <em>leader</em>,
    ///  or <em>set representative</em>) is one of the sites in the component:
    ///  two sites have the same component identifier if and only if they are
    ///  in the same component.
    ///  <ul>
    ///  <p/><li><em>union</em>(<em>p</em>, <em>q</em>) adds a
    ///         connection between the two sites <em>p</em> and <em>q</em>.
    ///         If <em>p</em> and <em>q</em> are in different components,
    ///         then it replaces
    ///         these two components with a new component that is the union of
    ///         the two.</li>
    ///  <p/><li><em>find</em>(<em>p</em>) returns the component
    ///         identifier of the component containing <em>p</em>.</li>
    ///  <p/><li><em>connected</em>(<em>p</em>, <em>q</em>)
    ///         returns true if both <em>p</em> and <em>q</em>
    ///         are in the same component, and false otherwise.</li>
    ///  <p/><li><em>count</em>() returns the number of components.</li>
    ///  </ul>
    ///  <p/>
    ///  The component identifier of a component can change
    ///  only when the component itself changes during a call to
    ///  <em>union</em>&amp;mdash;it cannot change during a call
    ///  to <em>find</em>, <em>connected</em>, or <em>count</em>.
    ///  <p>
    ///  This implementation uses weighted quick union by rank with path compression
    ///  by halving.
    ///  Initializing a data structure with <em>N</em> sites takes linear time.
    ///  Afterwards, the <em>union</em>, <em>find</em>, and <em>connected</em> 
    ///  operations take logarithmic time (in the worst case) and the
    ///  <em>count</em> operation takes constant time.
    ///  Moreover, the amortized time per <em>union</em>, <em>find</em>,
    ///  and <em>connected</em> operation has inverse Ackermann complexity.
    ///  For alternate implementations of the same API, see
    ///  {@link QuickUnionUF}, {@link QuickFindUF}, and {@link WeightedQuickUnionUF}.
    ///  </p>
    public class UF
    {
        private readonly int[] _parent;  // parent[i] = parent of i
        private readonly byte[] _rank;   // rank[i] = rank of subtree rooted at i (never more than 31)
        private int _count;     // number of components

        /// <summary>
        /// Initializes an empty union-find data structure with <tt>N</tt> sites
        /// <tt>0</tt> through <tt>N-1</tt>. Each site is initially in its own 
        /// component.
        /// </summary>
        /// <param name="n">N the number of sites</param>
        /// <exception cref="ArgumentException">if <tt>N &lt; 0</tt></exception>
        public UF(int n)
        {
            if (n < 0) throw new ArgumentException();
            _count = n;
            _parent = new int[n];
            _rank = new byte[n];
            for (var i = 0; i < n; i++)
            {
                _parent[i] = i;
                _rank[i] = 0;
            }
        }

        /// <summary>
        /// Returns the component identifier for the component containing site <tt>p</tt>.
        /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// <returns>the component identifier for the component containing site <tt>p</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless <tt>0 &lt;le; p &lt; N</tt></exception>
        public int Find(int p)
        {
            Validate(p);
            while (p != _parent[p])
            {
                _parent[p] = _parent[_parent[p]];    // path compression by halving
                p = _parent[p];
            }
            return p;
        }

        /// <summary>
        /// Returns the number of components.
        /// </summary>
        /// <returns>the number of components (between <tt>1</tt> and <tt>N</tt>)</returns>
        public int Count()
        {
            return _count;
        }

        /// <summary>
        /// Returns true if the the two sites are in the same component.
        /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// <param name="q">q the integer representing the other site</param>
        /// <returns><tt>true</tt> if the two sites <tt>p</tt> and <tt>q</tt> are in the same component;  <tt>false</tt> otherwise</returns>
        /// <exception cref="IndexOutOfRangeException">unless both <tt>0 &lt;= p &lt; N</tt> and <tt>0 &lt;= q &lt; N</tt></exception>
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Merges the component containing site <tt>p</tt> with the 
        /// the component containing site <tt>q</tt>.
        /// </summary>
        /// <param name="p">p the integer representing one site</param>
        /// <param name="q">q the integer representing the other site</param>
        /// <exception cref="IndexOutOfRangeException">unless both <tt>0 &lt;= p &lt; N</tt> and <tt>0 &lt;= q &lt; N</tt></exception>
        public void Union(int p, int q)
        {
            var rootP = Find(p);
            var rootQ = Find(q);
            if (rootP == rootQ) return;

            // make root of smaller rank point to root of larger rank
            if (_rank[rootP] < _rank[rootQ]) _parent[rootP] = rootQ;
            else if (_rank[rootP] > _rank[rootQ]) _parent[rootQ] = rootP;
            else
            {
                _parent[rootQ] = rootP;
                _rank[rootP]++;
            }
            _count--;
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
                throw new IndexOutOfRangeException($"index {p} is not between 0 and {n-1}");
            }
        }
    }
}