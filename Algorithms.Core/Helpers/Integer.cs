using System;

namespace Algorithms.Core.Helpers
{
    public class Integer : IComparable<Integer>
    {
        public int Value { get; }

        public Integer(int value)

        {
            Value = value;
        }

        public static implicit operator Integer(int value)
        {
            return new Integer(value);
        }

        public static implicit operator int (Integer integer)
        {
            return integer.Value;
        }

        public static Integer operator +(Integer one, Integer two)
        {
            return new Integer(one.Value + two.Value);
        }

        

        public static int operator -(Integer one, Integer two)
        {
            return new Integer(one.Value - two.Value);
        }

        public int CompareTo(Integer that)
        {
            if (Value < that.Value) return -1;
            if (Value > that.Value) return +1;
            return 0;
        }
    }
}
