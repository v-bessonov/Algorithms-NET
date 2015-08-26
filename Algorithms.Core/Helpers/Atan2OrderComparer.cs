using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// compare other points relative to atan2 angle (bewteen -pi/2 and pi/2) they make with this Point
    /// </summary>
    public class Atan2OrderComparer : IComparer<Point2D>
    {
        private Point2D _p;
        public Atan2OrderComparer(Point2D p)
        {
            _p = p;
        }
        public int Compare(Point2D q1, Point2D q2)
        {
            var angle1 = _p.AngleTo(q1);
            var angle2 = _p.AngleTo(q2);
            if (angle1 < angle2) return -1;
            if (angle1 > angle2) return +1;
            return 0;
        }
    }
}
