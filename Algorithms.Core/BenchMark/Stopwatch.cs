using System;

namespace Algorithms.Core.BenchMark
{
    public class Stopwatch
    {
        //Initialize a stopwatch object.
        private System.Diagnostics.Stopwatch _stopwatch;

        public void StartNew()
        {
            _stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
        }

        public void Stop()
        {
            _stopwatch.Stop();

        }
        
        /// <summary>
        /// Returns the elapsed time (in seconds)
        /// </summary>
        /// <returns></returns>
        public int ElapsedTime()
        {
            return (int)_stopwatch.Elapsed.TotalSeconds;
        }

        public string TimePassed()
        {
            var timeSpanTotal = new TimeSpan(0, 0, 0, (int)_stopwatch.Elapsed.TotalSeconds);
            return string.Format("{0:N0} дней, {1} часов, {2} минут, {3} секунд",
                timeSpanTotal.Days, timeSpanTotal.Hours,
                timeSpanTotal.Minutes, timeSpanTotal.Seconds);
        }
    }
}
