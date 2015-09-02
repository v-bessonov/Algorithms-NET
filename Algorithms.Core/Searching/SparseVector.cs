using System;
using System.Text;
using Algorithms.Core.Helpers;
using Double = Algorithms.Core.Helpers.Double;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>SparseVector</tt> class represents a <em>d</em>-dimensional mathematical vector.
    /// Vectors are mutable: their values can be changed after they are created.
    /// It includes methods for addition, subtraction,
    /// dot product, scalar product, unit vector, and Euclidean norm.
    /// <p>
    /// The implementation is a symbol table of indices and values for which the vector
    /// coordinates are nonzero. This makes it efficient when most of the vector coordindates
    /// are zero.
    /// </p>
    /// </summary>
    public class SparseVector
    {

        private readonly int _dims;                   // dimension
        private readonly ST<Integer, Double> _st;  // the vector, represented by index-value pairs

        /// <summary>
        /// Initializes a d-dimensional zero vector.
        /// </summary>
        /// <param name="dims">the dimension of the vector</param>
        public SparseVector(int dims)
        {
            _dims = dims;
            _st = new ST<Integer, Double>();
        }

        /// <summary>
        /// Sets the ith coordinate of this vector to the specified value.
        /// </summary>
        /// <param name="i">i the index</param>
        /// <param name="value">value the new value</param>
        /// <exception cref="IndexOutOfRangeException">unless i is between 0 and d-1</exception>
        public void Put(int i, double value)
        {
            if (i < 0 || i >= _dims) throw new IndexOutOfRangeException("Illegal index");
            if (Math.Abs(value) < 0.0001) _st.Delete(i);
            else _st.Put(i, value);
        }

        /// <summary>
        /// Returns the ith coordinate of this vector.
        /// </summary>
        /// <param name="i">i the index</param>
        /// <returns>the value of the ith coordinate of this vector</returns>
        /// <exception cref="IndexOutOfRangeException">unless i is between 0 and d-1</exception>
        public double Get(int i)
        {
            if (i < 0 || i >= _dims) throw new IndexOutOfRangeException("Illegal index");
            if (_st.Contains(i)) return _st.Get(i);
            return 0.0;
        }

        /// <summary>
        /// Returns the number of nonzero entries in this vector.
        /// </summary>
        /// <returns>the number of nonzero entries in this vector</returns>
        public int Nnz()
        {
            return _st.Size();
        }

        /// <summary>
        /// Returns the dimension of this vector.
        /// </summary>
        /// <returns>the dimension of this vector</returns>
        [Obsolete("Replaced by {@link #dimension()}.")]
        public int Size()
        {
            return _dims;
        }

        /// <summary>
        /// Returns the dimension of this vector.
        /// </summary>
        /// <returns>the dimension of this vector</returns>
        public int Dimension()
        {
            return _dims;
        }

        /// <summary>
        /// Returns the inner product of this vector with the specified vector.
        /// </summary>
        /// <param name="that">that the other vector</param>
        /// <returns>the dot product between this vector and that vector</returns>
        /// <exception cref="ArgumentException">if the lengths of the two vectors are not equal</exception>
        public double Dot(SparseVector that)
        {
            if (_dims != that._dims) throw new ArgumentException("Vector lengths disagree");
            var sum = 0.0;

            // iterate over the vector with the fewest nonzeros
            if (_st.Size() <= that._st.Size())
            {
                foreach (int i in _st.Keys())
                    if (that._st.Contains(i)) sum += Get(i) * that.Get(i);
            }
            else
            {
                foreach (int i in that._st.Keys())
                    if (_st.Contains(i)) sum += Get(i) * that.Get(i);
            }
            return sum;
        }


        /// <summary>
        /// Returns the inner product of this vector with the specified array.
        /// </summary>
        /// <param name="that">that the array</param>
        /// <returns>the dot product between this vector and that array</returns>
        /// <exception cref="ArgumentException">if the dimensions of the vector and the array are not equal</exception>
        public double Dot(double[] that)
        {
            var sum = 0.0;
            foreach (int i in _st.Keys())
                sum += that[i] * Get(i);
            return sum;
        }

        /// <summary>
        /// Returns the magnitude of this vector.
        /// This is also known as the L2 norm or the Euclidean norm.
        /// </summary>
        /// <returns>the magnitude of this vector</returns>
        public double Magnitude()
        {
            return Math.Sqrt(Dot(this));
        }


        /// <summary>
        /// Returns the Euclidean norm of this vector.
        /// </summary>
        /// <returns>the Euclidean norm of this vector</returns>
       [Obsolete("Replaced by {@link #magnitude()}.")]
        public double Norm()
        {
            return Math.Sqrt(Dot(this));
        }

        /// <summary>
        /// Returns the scalar-vector product of this vector with the specified scalar.
        /// </summary>
        /// <param name="alpha">alpha the scalar</param>
        /// <returns>the scalar-vector product of this vector with the specified scalar</returns>
        public SparseVector Scale(double alpha)
        {
            SparseVector c = new SparseVector(_dims);
            foreach (int i in _st.Keys()) c.Put(i, alpha * Get(i));
            return c;
        }

        /// <summary>
        /// Returns the sum of this vector and the specified vector.
        /// </summary>
        /// <param name="that">that the vector to add to this vector</param>
        /// <returns>the sum of this vector and that vector</returns>
        /// <exception cref="ArgumentException">if the dimensions of the two vectors are not equal</exception>
        public SparseVector Plus(SparseVector that)
        {
            if (_dims != that._dims) throw new ArgumentException("Vector lengths disagree");
            var c = new SparseVector(_dims);
            foreach (int i in _st.Keys()) c.Put(i, Get(i));                // c = this
            foreach (int i in that._st.Keys()) c.Put(i, that.Get(i) + c.Get(i));     // c = c + that
            return c;
        }

        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>a string representation of this vector, which consists of the the vector entries, separates by commas, enclosed in parentheses</returns>
        public override string ToString()
        {
            var s = new StringBuilder();
            foreach (int i in _st.Keys())
            {
                s.Append($"({i}, {_st.Get(i).Value})");
            }
            return s.ToString();
        }

    }
}
