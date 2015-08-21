using System;
using System.Text;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// Implementation of a vector of real numbers.
    /// This class is implemented to be immutable: once the client program
    /// initialize a Vector, it cannot change any of its fields
    /// (N or data[i]) either directly or indirectly. Immutability is a
    /// very desirable feature of a data type.
    /// </summary>
    public class Vector
    {

        private readonly int _n;               // length of the vector
        private readonly double[] _data;       // array of vector's components


        /// <summary>
        /// Initializes a d-dimensional zero vector.
        /// </summary>
        /// <param name="d">d the dimension of the vector</param>
        public Vector(int d)
        {
            _n = d;
            _data = new double[_n];
        }

        /// <summary>
        /// Initializes a vector from either an array or a vararg list.
        /// The vararg syntax supports a constructor that takes a variable number of
        /// arugments such as Vector x = new Vector(1.0, 2.0, 3.0, 4.0).
        /// </summary>
        /// <param name="a">a the array or vararg list</param>
        public Vector(params double[] a)
        {
            _n = a.Length;

            // defensive copy so that client can't alter our copy of data[]
            _data = new double[_n];
            for (var i = 0; i < _n; i++)
                _data[i] = a[i];
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        /// <returns>the dimension of this vector</returns>
        public int Length()
        {
            return _n;
        }

        /// <summary>
        /// Returns the inner product of this vector with that vector.
        /// </summary>
        /// <param name="that">that the other vector</param>
        /// <returns>the dot product between this vector and that vector</returns>
        /// throws IllegalArgumentException if the lengths of the two vectors are not equal.
        public double Dot(Vector that)
        {
            if (_n != that._n) throw new ArgumentException("Dimensions don't agree");
            var sum = 0.0;
            for (var i = 0; i < _n; i++)
                sum = sum + (_data[i] * that._data[i]);
            return sum;
        }

        /// <summary>
        /// Returns the Euclidean norm of this vector.
        /// </summary>
        /// <returns>the Euclidean norm of this vector</returns>
        public double Magnitude()
        {
            return Math.Sqrt(Dot(this));
        }

        /// <summary>
        /// Returns the Euclidean distance between this vector and that vector.
        /// </summary>
        /// <param name="that">that the other vector</param>
        /// <returns>the Euclidean distance between this vector and that vector</returns>
        /// throws ArgumentException if the lengths of the two vectors are not equal.
        public double DistanceTo(Vector that)
        {
            if (_n != that._n) throw new ArgumentException("Dimensions don't agree");
            return Minus(that).Magnitude();
        }

        /// <summary>
        /// Returns the sum of this vector and that vector: this + that.
        /// </summary>
        /// <param name="that">that the vector to add to this vector</param>
        /// <returns>the sum of this vector and that vector</returns>
        /// throws ArgumentException if the lengths of the two vectors are not equal.
        public Vector Plus(Vector that)
        {
            if (_n != that._n) throw new ArgumentException("Dimensions don't agree");
            var c = new Vector(_n);
            for (var i = 0; i < _n; i++)
                c._data[i] = _data[i] + that._data[i];
            return c;
        }

        /// <summary>
        /// Returns the difference between this vector and that vector: this - that.
        /// </summary>
        /// <param name="that">that the vector to subtract from this vector</param>
        /// <returns>the difference between this vector and that vector</returns>
        /// throws ArgumentException if the lengths of the two vectors are not equal.
        public Vector Minus(Vector that)
        {
            if (_n != that._n) throw new ArgumentException("Dimensions don't agree");
            var c = new Vector(_n);
            for (var i = 0; i < _n; i++)
                c._data[i] = _data[i] - that._data[i];
            return c;
        }

        /// <summary>
        /// Returns the ith cartesian coordinate.
        /// </summary>
        /// <param name="i">i the coordinate index</param>
        /// <returns>the ith cartesian coordinate</returns>
        public double Cartesian(int i)
        {
            return _data[i];
        }

        /// <summary>
        /// Returns the product of this factor multiplied by the scalar factor: this * factor.
        /// </summary>
        /// <param name="factor">factor the multiplier</param>
        /// <returns>the scalar product of this vector and factor</returns>
        public Vector Times(double factor)
        {
            var c = new Vector(_n);
            for (var i = 0; i < _n; i++)
                c._data[i] = factor * _data[i];
            return c;
        }

        /// <summary>
        /// Returns a unit vector in the direction of this vector.
        /// </summary>
        /// <returns>a unit vector in the direction of this vector</returns>
        /// throws ArithmeticException if this vector is the zero vector.
        public Vector Direction()
        {
            if (Math.Abs(Magnitude()) < 0.0001) throw new ArithmeticException("Zero-vector has no direction");
            return Times(1.0 / Magnitude());
        }


        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>a string representation of this vector, which consists of the </returns>
        /// the vector entries, separates by single spaces
        public override string ToString()
        {
            var s = new StringBuilder();
            for (var i = 0; i < _n; i++)
                s.Append($"{_data[i]} ");
            return s.ToString();
        }

    }
}
