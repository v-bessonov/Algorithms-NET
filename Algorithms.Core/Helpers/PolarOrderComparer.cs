using System;
using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// compare other points relative to polar angle (between 0 and 2pi) they make with this Point
    /// </summary>
    public class PolarOrderComparer : IComparer<Point2D>
    {
        private readonly Point2D _p;
        public PolarOrderComparer(Point2D p)
        {
            _p = p;
        }
        public int Compare(Point2D q1, Point2D q2)
        {
            var dx1 = q1.X - _p.X;
            var dy1 = q1.Y - _p.Y;
            var dx2 = q2.X - _p.X;
            var dy2 = q2.Y - _p.Y;

            if (dy1 >= 0 && dy2 < 0) return -1;    // q1 above; q2 below
            if (dy2 >= 0 && dy1 < 0) return +1;    // q1 below; q2 above
            if (Math.Abs(dy1) < 0.0001 && Math.Abs(dy2) < 0.0001)
            {
                // 3-collinear and horizontal
                if (dx1 >= 0 && dx2 < 0) return -1;
                if (dx2 >= 0 && dx1 < 0) return +1;
                return 0;
            }
            return -_p.Ccw(_p, q1, q2);     // both above or below

            // Note: ccw() recomputes dx1, dy1, dx2, and dy2
        }
    }
}
