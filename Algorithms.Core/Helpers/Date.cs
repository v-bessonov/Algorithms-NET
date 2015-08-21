using System;

namespace Algorithms.Core.Helpers
{
    /// <summary>
    /// The <tt>Date</tt> class is an immutable data type to encapsulate a
    /// date (day, month, and year).
    /// </summary>
    public class Date : IComparable<Date>
    {

        private static readonly int[] Days = { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        private readonly int _month;   // month (between 1 and 12)
        private readonly int _day;     // day   (between 1 and DAYS[month]
        private readonly int _year;    // year

        /// <summary>
        /// Initializes a new date from the month, day, and year.
        /// </summary>
        /// <param name="month">month the month (between 1 and 12)</param>
        /// <param name="day">day the day (between 1 and 28-31, depending on the month)</param>
        /// <param name="year">year the year</param>
        public Date(int month, int day, int year)
        {
            if (!IsValid(month, day, year)) throw new ArgumentException("Invalid date");
            _month = month;
            _day = day;
            _year = year;
        }

        /// <summary>
        /// Initializes new date specified as a string in form MM/DD/YYYY.
        /// throws ArgumentException if this date is invalid
        /// </summary>
        /// <param name="date">date the string representation of this date</param>
        public Date(string date)
        {
            var fields = date.Split(new []{'/'},StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length != 3)
            {
                throw new ArgumentException("Invalid date");
            }
            _month = int.Parse(fields[0]);
            _day = int.Parse(fields[1]);
            _year = int.Parse(fields[2]);
            if (!IsValid(_month, _day, _year)) throw new ArgumentException("Invalid date");
        }

        /// <summary>
        /// Return the month.
        /// </summary>
        /// <returns>the month (an integer between 1 and 12)</returns>
        public int Month()
        {
            return _month;
        }

        /// <summary>
        /// Returns the day.
        /// </summary>
        /// <returns>the day (an integer between 1 and 31)</returns>
        public int Day()
        {
            return _day;
        }

        /// <summary>
        /// Returns the year.
        /// </summary>
        /// <returns>the year</returns>
        public int Year()
        {
            return _year;
        }


        /// <summary>
        /// is the given date valid?
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool IsValid(int m, int d, int y)
        {
            if (m < 1 || m > 12) return false;
            if (d < 1 || d > Days[m]) return false;
            if (m == 2 && d == 29 && !IsLeapYear(y)) return false;
            return true;
        }

        /// <summary>
        /// is y a leap year?
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool IsLeapYear(int y)
        {
            if (y % 400 == 0) return true;
            if (y % 100 == 0) return false;
            return y % 4 == 0;
        }

        /// <summary>
        /// Returns the next date in the calendar.
        /// </summary>
        /// <returns>a date that represents the next day after this day</returns>
        public Date Next()
        {
            if (IsValid(_month, _day + 1, _year)) return new Date(_month, _day + 1, _year);
            if (IsValid(_month + 1, 1, _year)) return new Date(_month + 1, 1, _year);
            return new Date(1, 1, _year + 1);
        }

        /// <summary>
        /// Compares two dates chronologically.
        /// </summary>
        /// <param name="that">that the other date</param>
        /// <returns><tt>true</tt> if this date is after that date; <tt>false</tt> otherwise</returns>
        public bool IsAfter(Date that)
        {
            return CompareTo(that) > 0;
        }

        /// <summary>
        /// Compares two dates chronologically.
        /// </summary>
        /// <param name="that">that the other date</param>
        /// <returns><tt>true</tt> if this date is before that date; <tt>false</tt> otherwise</returns>
        public bool IsBefore(Date that)
        {
            return CompareTo(that) < 0;
        }

        /// <summary>
        /// Compares two dates chronologically.
        /// </summary>
        /// <param name="that"></param>
        /// <returns>the value <tt>0</tt> if the argument date is equal to this date;</returns>
        /// a negative integer if this date is chronologically less than
        /// the argument date; and a positive ineger if this date is chronologically
        /// after the argument date
        public int CompareTo(Date that)
        {
            if (_year < that._year) return -1;
            if (_year > that._year) return +1;
            if (_month < that._month) return -1;
            if (_month > that._month) return +1;
            if (_day < that._day) return -1;
            if (_day > that._day) return +1;
            return 0;
        }

        /// <summary>
        /// Compares this date to the specified date.
        /// </summary>
        /// <param name="other">other the other date</param>
        /// <returns><tt>true</tt> if this date equals <tt>other</tt>; <tt>false</tt> otherwise</returns>
        public override bool Equals(object other)
        {
            if (other == this) return true;
            if (other == null) return false;
            if (other.GetType() != GetType()) return false;
            var that = (Date)other;
            return (_month == that._month) && (_day == that._day) && (_year == that._year);
        }

        /// <summary>
        /// Returns an integer hash code for this date.
        /// </summary>
        /// <returns>a hash code for this date</returns>
        public override int GetHashCode()
        {
            var hash = 17;
            hash = 31 * hash + _month;
            hash = 31 * hash + _day;
            hash = 31 * hash + _year;
            return hash;

        }
        /// <summary>
        /// Returns a string representation of this date.
        /// </summary>
        /// <returns>the string representation in the format MM/DD/YYYY</returns>
        public override string ToString()
        {
            return $"{_month}/{_day}/{_year}";
        }
    }
}
