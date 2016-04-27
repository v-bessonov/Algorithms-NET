using System;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>GREP</tt> class provides a client for reading in a sequence of
    /// lines from standard input and printing to standard output those lines
    /// that contain a substring matching a specified regular expression.
    /// <p/>
    /// </summary>
    public class GREP
    {
        private readonly string[] _lines;
        private readonly NFA _nfa;

        public GREP (string regexp, string[] lines)
        {
            var regexp1 = string.Format($"(.*{regexp}.*)");
            _lines = lines;
            _nfa = new NFA(regexp1);
        }

        public void Match()
        {
            foreach (var line in _lines)
            {
                if (_nfa.Recognizes(line))
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
