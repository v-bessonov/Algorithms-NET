using System;
using System.Collections.Generic;

namespace Algorithms.Core.Sorting
{
    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.Ordinal);
        }
    }
}
