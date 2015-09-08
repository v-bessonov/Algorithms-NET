using System;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>EdgeW</tt> class represents a weighted edge
    /// Each edge consists of two integers
    /// (naming the two vertices) and a real-value weight. The data type
    /// provides methods for accessing the two endpoints of the edge and
    /// the weight. The natural order for this data type is by
    /// ascending order of weight.
    /// </summary>
    public class EdgeW : Egde, IComparable<EdgeW>
    {
        /// <summary>
        /// Returns the weight of this edge.
        /// </summary>
        public double Weight { get; }

        /// <summary>
        /// Initializes an edge between vertices <tt>v</tt> and <tt>w</tt> of
        /// the given <tt>weight</tt>.
        /// </summary>
        /// <param name="v">v one vertex</param>
        /// <param name="w">w the other vertex</param>
        /// <param name="weight">weight the weight of this edge</param>
        /// <exception cref="IndexOutOfRangeException">if either <tt>v</tt> or <tt>w</tt> is a negative integer</exception>
        /// <exception cref="ArgumentException">if <tt>weight</tt> is <tt>NaN</tt></exception>
        public EdgeW(int v, int w, double weight)
        {
            if (v < 0) throw new IndexOutOfRangeException("Vertex name must be a nonnegative integer");
            if (w < 0) throw new IndexOutOfRangeException("Vertex name must be a nonnegative integer");
            if (double.IsNaN(weight)) throw new ArgumentException("Weight is NaN");
            V = v;
            W = w;
            Weight = weight;
        }

        /// <summary>
        /// Returns either endpoint of this edge.
        /// </summary>
        /// <returns>either endpoint of this edge</returns>
        public int Either()
        {
            return V;
        }

        /// <summary>
        /// Returns the endpoint of this edge that is different from the given vertex.
        /// </summary>
        /// <param name="vertex">vertex one endpoint of this edge</param>
        /// <returns>the other endpoint of this edge</returns>
        /// <exception cref="ArgumentException">if the vertex is not one of the endpoints of this edge</exception>
        public int Other(int vertex)
        {
            if (vertex == V) return W;
            if (vertex == W) return V;
            throw new ArgumentException("Illegal endpoint");
        }

        /// <summary>
        /// Compares two edges by weight.
        /// Note that <tt>compareTo()</tt> is not consistent with <tt>equals()</tt>,
        /// which uses the reference equality implementation inherited from <tt>Object</tt>.
        /// </summary>
        /// <param name="that">that the other edge</param>
        /// <returns>a negative integer, zero, or positive integer depending on whether the weight of this is less than, equal to, or greater than the argument edge</returns>
        public int CompareTo(EdgeW that)
        {
            if (Weight < that.Weight) return -1;
            if (Weight > that.Weight) return +1;
            return 0;
        }

        public override string ToString()
        {
            return $"{V}-{W} {Weight}";
        }
    }
}
