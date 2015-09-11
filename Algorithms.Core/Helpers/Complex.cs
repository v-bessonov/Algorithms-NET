using System;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// The <tt>Complex</tt> class represents a complex number.
    /// Complex numbers are immutable: their values cannot be changed after they
    /// are created.
    /// It includes methods for addition, subtraction, multiplication, division,
    ///  conjugation, and other common functions on complex numbers.
    /// </summary>
    public class Complex
    {

        private readonly double _re;   // the real part
        private readonly double _im;   // the imaginary part

        /// <summary>
        /// Initializes a complex number from the specified real and imaginary parts.
        /// </summary>
        /// <param name="real">real the real part</param>
        /// <param name="imag">imag the imaginary part</param>
        public Complex(double real, double imag)
        {
            _re = real;
            _im = imag;
        }

        /// <summary>
        /// Returns a string representation of this complex number.
        /// </summary>
        /// <returns>a string representation of this complex number, of the form 12 - 5i.</returns>
        public override string ToString()
        {
            if (Math.Abs(_im) < 0.0001) return _re + string.Empty;
            if (Math.Abs(_re) < 0.0001) return $"{_im}i";
            return _im < 0 ? $"{_re} - {(-_im)}i" : $"{_re} + {_im}i";
        }

        /// <summary>
        /// Returns the absolute value of this complex number.
        /// This quantity is also known as the <em>modulus</em> or <em>magnitude</em>.
        /// </summary>
        /// <returns>the absolute value of this complex number</returns>
        public double Abs()
        {
            return Math.Sqrt(Math.Pow(_re, 2) + Math.Pow(_im, 2));
        }

        /// <summary>
        /// Returns the phase of this complex number.
        /// This quantity is also known as the <em>ange</em> or <em>argument</em>.
        /// </summary>
        /// <returns>the phase of this complex number, a real number between -pi and pi</returns>
        public double Phase()
        {
            return Math.Atan2(_im, _re);
        }

        /// <summary>
        /// Returns the sum of this complex number and the specified complex number.
        /// </summary>
        /// <param name="that">that the other complex number</param>
        /// <returns>the sum of this complex number and that complex number</returns>
        public Complex Plus(Complex that)
        {
            var real = _re + that._re;
            var imag = _im + that._im;
            return new Complex(real, imag);
        }

        /// <summary>
        /// Returns the result of subtracting the specified complex number from
        /// this complex number.
        /// </summary>
        /// <param name="that">that the other complex number</param>
        /// <returns>the result of subtracting that complex number from this complex number</returns>
        public Complex Minus(Complex that)
        {
            var real = _re - that._re;
            var imag = _im - that._im;
            return new Complex(real, imag);
        }

        /// <summary>
        /// Returns the product of this complex number and the specified complex number.
        /// </summary>
        /// <param name="that">that the other complex number</param>
        /// <returns>the product of this complex number and that complex number</returns>
        public Complex Times(Complex that)
        {
            var real = _re * that._re - _im * that._im;
            var imag = _re * that._im + _im * that._re;
            return new Complex(real, imag);
        }

        /// <summary>
        /// eturns the product of this complex number and the specified scalar.
        /// </summary>
        /// <param name="alpha">alpha the scalar</param>
        /// <returns>the product of this complex number and the scalar <tt>alpha</tt></returns>
        public Complex Scale(double alpha)
        {
            return new Complex(alpha * _re, alpha * _im);
        }

        /// <summary>
        /// Returns the product of this complex number and the specified scalar.
        /// </summary>
        /// <param name="alpha">alpha the scalar</param>
        /// <returns>the product of this complex number and the scalar <tt>alpha</tt></returns>
        [Obsolete("Use Scale instead.")]
        public Complex Times(double alpha)
        {
            return new Complex(alpha * _re, alpha * _im);
        }

        /// <summary>
        /// Returns the complex conjugate of this complex number.
        /// </summary>
        /// <returns>the complex conjugate of this complex number</returns>
        public Complex Conjugate()
        {
            return new Complex(_re, -_im);
        }

        /// <summary>
        /// Returns the reciprocal of this complex number.
        /// </summary>
        /// <returns>the reciprocal of this complex number</returns>
        public Complex Reciprocal()
        {
            var scale = _re * _re + _im * _im;
            return new Complex(_re / scale, -_im / scale);
        }

        /// <summary>
        /// Returns the real part of this complex number.
        /// </summary>
        /// <returns>the real part of this complex number</returns>
        public double Re()
        {
            return _re;
        }

        /// <summary>
        /// Returns the imaginary part of this complex number.
        /// </summary>
        /// <returns>the imaginary part of this complex number</returns>
        public double Im()
        {
            return _im;
        }

        /// <summary>
        /// Returns the result of dividing the specified complex number into
        /// this complex number.
        /// </summary>
        /// <param name="that">that the other complex number</param>
        /// <returns>the result of dividing that complex number into this complex number</returns>
        public Complex Divides(Complex that)
        {
            return Times(that.Reciprocal());
        }

        /// <summary>
        /// Returns the complex exponential of this complex number.
        /// </summary>
        /// <returns>the complex exponential of this complex number</returns>
        public Complex Exp()
        {
            return new Complex(Math.Exp(_re) * Math.Cos(_im), Math.Exp(_re) * Math.Sin(_im));
        }

        /// <summary>
        /// Returns the complex sine of this complex number.
        /// </summary>
        /// <returns>the complex sine of this complex number</returns>
        public Complex Sin()
        {
            return new Complex(Math.Sin(_re) * Math.Cosh(_im), Math.Cos(_re) * Math.Sinh(_im));
        }

        /// <summary>
        /// Returns the complex cosine of this complex number.
        /// </summary>
        /// <returns>the complex cosine of this complex number</returns>
        public Complex Cos()
        {
            return new Complex(Math.Cos(_re) * Math.Cosh(_im), -Math.Sin(_re) * Math.Sinh(_im));
        }

        /// <summary>
        /// Returns the complex tangent of this complex number.
        /// </summary>
        /// <returns>the complex tangent of this complex number</returns>
        public Complex Tan()
        {
            return Sin().Divides(Cos());
        }


    }
}
