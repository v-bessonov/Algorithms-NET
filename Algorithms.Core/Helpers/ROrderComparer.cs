using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// compare points according to their polar radius
    /// </summary>
    public class ROrderComparer : IComparer<Point2D>
    {
       
        public int Compare(Point2D p, Point2D q)
        {
            var delta = (p.X * p.X + p.Y * p.Y) - (q.X * q.X + q.Y * q.Y);
            if (delta < 0) return -1;
            if (delta > 0) return +1;
            return 0;
        }
    }
}
