using System;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EdgeD</tt> class represents a edge for directed graph
    /// Each edge consists of two integers
    /// (naming the two vertices
    /// </summary>
    public class EdgeD : Egde, IComparable<EdgeD>
    {

        /// <summary>
        /// Initializes an edge between vertices <tt>v</tt> and <tt>w</tt>
        /// </summary>
        /// <param name="v">v one vertex</param>
        /// <param name="w">w the other vertex</param>
        /// <exception cref="IndexOutOfRangeException">if either <tt>v</tt> or <tt>w</tt> is a negative integer</exception>
        /// <exception cref="ArgumentException">if <tt>weight</tt> is <tt>NaN</tt></exception>
        public EdgeD(int v, int w)
        {
            if (v < 0) throw new IndexOutOfRangeException("Vertex name must be a nonnegative integer");
            if (w < 0) throw new IndexOutOfRangeException("Vertex name must be a nonnegative integer");
            V = v;
            W = w;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public int CompareTo(EdgeD that)
        {
            if (V < that.V) return -1;
            if (V > that.V) return +1;
            if (W < that.W) return -1;
            if (W > that.W) return +1;
            return 0;
        }

        public override string ToString()
        {
            return $"{V}-{W}";
        }
    }
}
