using System;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DirectedEdge</tt> class represents a weighted edge in an 
    /// {@link EdgeWeightedDigraph}. Each edge consists of two integers
    /// (naming the two vertices) and a real-value weight. The data type
    /// provides methods for accessing the two endpoints of the directed edge and
    /// the weight.
    /// </summary>
    public class DirectedEdge : Egde, IComparable<DirectedEdge>
    {
        /// <summary>
        /// Returns the weight of the directed edge.
        /// </summary>
        public double Weight { get; }


        /// <summary>
        /// Initializes a directed edge from vertex <tt>v</tt> to vertex <tt>w</tt> with
        /// the given <tt>weight</tt>.
        /// </summary>
        /// <param name="v">v the tail vertex</param>
        /// <param name="w">w the head vertex</param>
        /// <param name="weight">weight the weight of the directed edge</param>
        /// <exception cref="IndexOutOfRangeException">if either <tt>v</tt> or <tt>w</tt> is a negative integer</exception>
        /// <exception cref="ArgumentException">if <tt>weight</tt> is <tt>NaN</tt></exception>
        public DirectedEdge(int v, int w, double weight)
        {
            if (v < 0) throw new IndexOutOfRangeException("Vertex names must be nonnegative integers");
            if (w < 0) throw new IndexOutOfRangeException("Vertex names must be nonnegative integers");
            if (double.IsNaN(weight)) throw new ArgumentException("Weight is NaN");
            V = v;
            W = w;
            Weight = weight;
        }

        /// <summary>
        /// Returns the tail vertex of the directed edge.
        /// </summary>
        /// <returns>the tail vertex of the directed edge</returns>
        public int From()
        {
            return V;
        }

        /// <summary>
        /// Returns the head vertex of the directed edge.
        /// </summary>
        /// <returns>the head vertex of the directed edge</returns>
        public int To()
        {
            return W;
        }

        /// <summary>
        /// Compares two edges by weight.
        /// Note that <tt>compareTo()</tt> is not consistent with <tt>equals()</tt>,
        /// which uses the reference equality implementation inherited from <tt>Object</tt>.
        /// </summary>
        /// <param name="that">that the other edge</param>
        /// <returns>a negative integer, zero, or positive integer depending on whether the weight of this is less than, equal to, or greater than the argument edge</returns>
        public int CompareTo(DirectedEdge that)
        {
            if (Weight < that.Weight) return -1;
            if (Weight > that.Weight) return +1;
            return 0;
        }

        /// <summary>
        /// Returns a string representation of the directed edge.
        /// </summary>
        /// <returns>a string representation of the directed edge</returns>
        public override string ToString()
        {
            return $"{V}->{W} {Weight}";
        }
    }
}
