using System;

namespace Algorithms.Core.Sorting
{
    public class StringComparable : IComparable
    {
        public string Value { get; set; }

        public StringComparable(string value)
        {
            Value = value;
        }

        #region IComparable<StringComparable> Members

        public int CompareTo(StringComparable other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        #endregion

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherObj = obj as StringComparable;
            if (otherObj != null)
                return CompareTo(otherObj);
            throw new ArgumentException("Object is not a StringComparable");
        }

        public override string ToString()
        {
            return Value;

        }
    }
}
