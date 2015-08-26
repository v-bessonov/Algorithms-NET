using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// compare points according to their y-coordinate
    /// </summary>
    public class YOrderComparer : IComparer<Point2D>
    {
       
        public int Compare(Point2D p, Point2D q)
        {
            if (p.Y < q.Y) return -1;
            if (p.Y > q.Y) return +1;
            return 0;
        }
    }
}
