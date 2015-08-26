using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// compare points according to their x-coordinate
    /// </summary>
    public class XOrderComparer : IComparer<Point2D>
    {
       
        public int Compare(Point2D p, Point2D q)
        {
            if (p.X < q.X) return -1;
            if (p.X > q.X) return +1;
            return 0;
        }
    }
}
