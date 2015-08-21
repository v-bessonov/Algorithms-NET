using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// Compares two transactions by date.
    /// </summary>
    public class WhenOrderComparer : IComparer<Transaction>
    {
        public int Compare(Transaction v, Transaction w)
        {
            return v.When().CompareTo(w.When());
        }
    }
}
