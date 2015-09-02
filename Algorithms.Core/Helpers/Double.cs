using System;

namespace Algorithms.Core.Helpers
{
    public class Double : IComparable<Double>
    {
        public double Value { get; }

        public Double(double value)

        {
            Value = value;
        }

        public static implicit operator Double(double value)
        {
            return new Double(value);
        }

        public static implicit operator double (Double d)
        {
            return d.Value;
        }

        public static Double operator +(Double one, Double two)
        {
            return new Double(one.Value + two.Value);
        }

        

        public static double operator -(Double one, Double two)
        {
            return new Double(one.Value - two.Value);
        }

        public int CompareTo(Double that)
        {
            if (Value < that.Value) return -1;
            if (Value > that.Value) return +1;
            return 0;
        }
    }
}
