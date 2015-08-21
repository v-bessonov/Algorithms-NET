using System;
using System.Collections.Generic;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// Compares two transactions by customer name.
    /// </summary>
    public class WhoOrderComparer : IComparer<Transaction>
    {
        public int Compare(Transaction v, Transaction w)
        {
            return string.Compare(v.Who(), w.Who(), StringComparison.Ordinal);
        }
    }
}
