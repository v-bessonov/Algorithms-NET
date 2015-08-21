using System;
using System.Globalization;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// The <tt>Transaction</tt> class is an immutable data type to encapsulate a
    /// commercial transaction with a customer name, date, and amount.
    /// </summary>
    public class Transaction : IComparable<Transaction>
    {
        private readonly string _who;      // customer
        private readonly Date _when;     // date
        private readonly double _amount;   // amount

        //    /**
        //* Initializes a new transaction from the given arguments.
        //*
        //* @param  who the person involved in this transaction
        //* @param  when the date of this transaction
        //* @param  amount the amount of this transaction
        //* @throws IllegalArgumentException if <tt>amount</tt> 
        //*         is <tt>Double.NaN</tt>, <tt>Double.POSITIVE_INFINITY</tt>,
        //*         or <tt>Double.NEGATIVE_INFINITY</tt>
        //*/
        /// <summary>
        /// Initializes a new transaction from the given arguments.
        /// </summary>
        /// <param name="who">who the person involved in this transaction</param>
        /// <param name="when">when the date of this transaction</param>
        /// <param name="amount">amount the amount of this transaction</param>
        /// throws IllegalArgumentException if <tt>amount</tt> 
        /// is <tt>Double.NaN</tt>, <tt>Double.POSITIVE_INFINITY</tt>,
        /// or <tt>Double.NEGATIVE_INFINITY</tt>
        public Transaction(string who, Date when, double amount)
        {
            if (double.IsNaN(amount) || double.IsInfinity(amount))
                throw new ArgumentException("Amount cannot be NaN or infinite");
            _who = who;
            _when = when;
            _amount = Math.Abs(amount) < 0.0001 ? 0.0 : amount;
        }

        /// <summary>
        /// Initializes a new transaction by parsing a string of the form NAME DATE AMOUNT.
        /// </summary>
        /// <param name="transaction">transaction the string to parse</param>
        /// throws IllegalArgumentException if <tt>amount</tt>
        /// is <tt>Double.NaN</tt>, <tt>Double.POSITIVE_INFINITY</tt>,
        /// or <tt>Double.NEGATIVE_INFINITY</tt>
        public Transaction(string transaction)
        {
            var a = transaction.Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _who = a[0];
            _when = new Date(a[1]);
            var value = double.Parse(a[2],CultureInfo.InvariantCulture);
            _amount = Math.Abs(value) < 0.0001 ? 0.0 : value;
            if (double.IsNaN(_amount) || double.IsInfinity(_amount))
                throw new ArgumentException("Amount cannot be NaN or infinite");
        }

        /// <summary>
        /// Returns the name of the customer involved in this transaction.
        /// </summary>
        /// <returns>the name of the customer involved in this transaction</returns>
        public string Who()
        {
            return _who;
        }

        /// <summary>
        /// Returns the date of this transaction.
        /// </summary>
        /// <returns>the date of this transaction</returns>
        public Date When()
        {
            return _when;
        }

        /// <summary>
        /// Returns the amount of this transaction.
        /// </summary>
        /// <returns>the amount of this transaction</returns>
        public double Amount()
        {
            return _amount;
        }

        /// <summary>
        /// Compares two transactions by amount.
        /// </summary>
        /// <param name="that">that the other transaction</param>
        /// <returns>{ a negative integer, zero, a positive integer}, depending</returns>
        /// on whether the amount of this transaction is { less than,
        /// equal to, or greater than } the amount of that transaction
        public int CompareTo(Transaction that)
        {
            if (_amount < that._amount) return -1;
            if (_amount > that._amount) return +1;
            return 0;
        }

        /// <summary>
        /// Compares this transaction to the specified object.
        /// </summary>
        /// <param name="other">other the other transaction</param>
        /// <returns>true if this transaction is equal to <tt>other</tt>; false otherwise</returns>
        public override bool Equals(object other)
        {
            if (other == this) return true;
            if (other == null) return false;
            if (other.GetType() != GetType()) return false;
            var that = (Transaction)other;
            return (Math.Abs(_amount - that._amount) < 0.0001) && (_who.Equals(that._who))
                                                && (_when.Equals(that._when));
        }

        /// <summary>
        /// Returns a hash code for this transaction.
        /// </summary>
        /// <returns>a hash code for this transaction</returns>
        public override int GetHashCode()
        {
            var hash = 17;
            hash = 31 * hash + _who.GetHashCode();
            hash = 31 * hash + _when.GetHashCode();
            hash = 31 * hash + _amount.GetHashCode();
            return hash;

        }
        /// <summary>
        /// Returns a string representation of this transaction.
        /// </summary>
        /// <returns>a string representation of this transaction</returns>
        public override string ToString()
        {
            return $"{_who} {_when} {_amount}";
        }
    }
}
