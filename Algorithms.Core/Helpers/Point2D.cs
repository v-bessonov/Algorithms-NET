using System;
using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// The <tt>Point</tt> class is an immutable data type to encapsulate a
    /// two-dimensional point with real-value coordinates.
    /// Note: in order to deal with the difference behavior of double and 
    /// Double with respect to -0.0 and +0.0, the Point2D constructor converts
    /// any coordinates that are -0.0 to +0.0.
    /// </summary>
    public sealed class Point2D : IComparable<Point2D>
    {
        public double X { get; }
        public double Y { get; }


        // Compares two points by x-coordinate.
        public static IComparer<Point2D> XOrder = new XOrderComparer();

        // Compares two points by y-coordinate.
        public static IComparer<Point2D> YOrder = new YOrderComparer();

        // Compares two points by polar radius.
        public static IComparer<Point2D> ROrder = new ROrderComparer();

        /// <summary>
        /// Compares two points by polar angle (between 0 and 2pi) with respect to this point.
        /// </summary>
        /// <returns>the comparator</returns>
        public IComparer<Point2D> PolarOrder()
        {
            return new PolarOrderComparer(this);
        }

        /// <summary>
        /// Compares two points by atan2() angle (between -pi and pi) with respect to this point.
        /// </summary>
        /// <returns>the comparator</returns>
        public IComparer<Point2D> Atan2Order()
        {
            return new Atan2OrderComparer(this);
        }

        /// <summary>
        /// Compares two points by distance to this point.
        /// </summary>
        /// <returns>the comparator</returns>
        public IComparer<Point2D> DistanceToOrder()
        {
            return new DistanceToOrderComparer(this);
        }

        /// <summary>
        /// Initializes a new point (x, y).
        /// </summary>
        /// <param name="x">x the x-coordinate</param>
        /// <param name="y">y the y-coordinate</param>
        /// <exception cref="ArgumentException">ArgumentException if either <tt>x</tt> or <tt>y</tt> is <tt>Double.NaN</tt>, <tt>Double.POSITIVE_INFINITY</tt> or <tt>Double.NEGATIVE_INFINITY</tt></exception>
        public Point2D(double x, double y)
        {
            if (double.IsInfinity(x) || double.IsInfinity(y))
                throw new ArgumentException("Coordinates must be finite");
            if (double.IsNaN(x) || double.IsNaN(y))
                throw new ArgumentException("Coordinates cannot be NaN");
            X = Math.Abs(x) < 0.0001 ? 0.0 : x;

            Y = Math.Abs(y) < 0.0001 ? 0.0 : y;
        }

        /// <summary>
        /// Returns the polar radius of this point.
        /// </summary>
        /// <returns>the polar radius of this point in polar coordiantes: sqrt(x*x + y*y)</returns>
        public double R()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Returns the angle of this point in polar coordinates.
        /// </summary>
        /// <returns>the angle (in radians) of this point in polar coordiantes (between -pi/2 and pi/2)</returns>
        public double Theta()
        {
            return Math.Atan2(Y, X);
        }

        /// <summary>
        /// Returns the angle between this point and that point.
        /// </summary>
        /// <param name="that"></param>
        /// <returns>the angle in radians (between -pi and pi) between this point and that point (0 if equal)</returns>
        public double AngleTo(Point2D that)
        {
            var dx = that.X - X;
            var dy = that.Y - Y;
            return Math.Atan2(dy, dx);
        }


        /// <summary>
        /// Returns true if a->b->c is a counterclockwise turn.
        /// </summary>
        /// <param name="a">a first point</param>
        /// <param name="b">b second point</param>
        /// <param name="c">c third point</param>
        /// <returns>{ -1, 0, +1 } if a->b->c is a { clockwise, collinear; counterclocwise } turn.</returns>
        public int Ccw(Point2D a, Point2D b, Point2D c)
        {
            var area2 = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
            if (area2 < 0) return -1;
            if (area2 > 0) return +1;
            return 0;
        }

        /// <summary>
        /// Returns twice the signed area of the triangle a-b-c.
        /// </summary>
        /// <param name="a">a first point</param>
        /// <param name="b">b second point</param>
        /// <param name="c">c third point</param>
        /// <returns>twice the signed area of the triangle a-b-c</returns>
        public static double Area2(Point2D a, Point2D b, Point2D c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }

        /// <summary>
        /// Returns the Euclidean distance between this point and that point.
        /// </summary>
        /// <param name="that">that the other point</param>
        /// <returns>the Euclidean distance between this point and that point</returns>
        public double DistanceTo(Point2D that)
        {
            var dx = X - that.X;
            var dy = Y - that.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Returns the square of the Euclidean distance between this point and that point.
        /// </summary>
        /// <param name="that">that the other point</param>
        /// <returns>the square of the Euclidean distance between this point and that point</returns>
        public double DistanceSquaredTo(Point2D that)
        {
            var dx = X - that.X;
            var dy = Y - that.Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Compares two points by y-coordinate, breaking ties by x-coordinate.
        /// </summary>
        /// <param name="that">that the other point</param>
        /// <returns>the value <tt>0</tt> if this string is equal to the argument</returns>
        /// string (precisely when <tt>equals()</tt> returns <tt>true</tt>);
        /// a negative integer if this point is less than the argument
        /// point; and a positive integer if this point is greater than the
        /// argument point
        public int CompareTo(Point2D that)
        {

            if (Y < that.Y) return -1;
            if (Y > that.Y) return +1;
            if (X < that.X) return -1;
            if (X > that.X) return +1;
            return 0;
        }
        /// <summary>
        /// Compares this point to the specified point.
        /// </summary>
        /// <param name="other">other the other point</param>
        /// <returns><tt>true</tt> if this point equals <tt>other</tt>; <tt>false</tt> otherwise</returns>
        public override bool Equals(object other)
        {
            if (other == this) return true;
            if (other == null) return false;
            if (other.GetType() != this.GetType()) return false;
            var that = (Point2D)other;
            return Math.Abs(X - that.X) < 0.0001 && Math.Abs(Y - that.Y) < 0.0001;
        }

        /// <summary>
        /// Return a string representation of this point.
        /// </summary>
        /// <returns>a string representation of this point in the format (x, y)</returns>
        public override string ToString()
        {
            return $"({X},{Y})";
        }


        /// <summary>
        /// Returns an integer hash code for this point.
        /// </summary>
        /// <returns>an integer hash code for this point</returns>
        public override int GetHashCode()
        {
            var hashX = X.GetHashCode();
            var hashY = Y.GetHashCode();
            return 31 * hashX + hashY;
        }

        /// <summary>
        /// Plot this point using standard draw.
        /// </summary>
        public void Draw()
        {
            //StdDraw.point(x, y);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Plot a line from this point to that point using standard draw.
        /// </summary>
        /// <param name="that">that the other point</param>
        public void DrawTo(Point2D that)
        {
            //StdDraw.line(this.x, this.y, that.x, that.y);
            throw new NotImplementedException();
        }
    }
}
