using System;
using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// compare points according to their distance to this point
    /// </summary>
    public class DistanceToOrderComparer : IComparer<Point2D>
    {
        private readonly Point2D _p;
        public DistanceToOrderComparer(Point2D p)
        {
            _p = p;
        }
        public int Compare(Point2D q1, Point2D q2)
        {
            var dist1 = _p.DistanceSquaredTo(q1);
            var dist2 = _p.DistanceSquaredTo(q2);
            if (dist1 < dist2) return -1;
            if (dist1 > dist2) return +1;
            return 0;
        }
    }
}
