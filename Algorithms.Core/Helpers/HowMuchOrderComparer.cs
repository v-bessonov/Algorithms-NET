using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// Compares two transactions by amount.
    /// </summary>
    public class HowMuchOrderComparer : IComparer<Transaction>
    {
        public int Compare(Transaction v, Transaction w)
        {
            if (v.Amount() < w.Amount()) return -1;
            if (v.Amount() > w.Amount()) return +1;
            return 0;
        }
    }
}
