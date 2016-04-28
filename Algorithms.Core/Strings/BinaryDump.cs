using System;
using Algorithms.Core.InOut;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>BinaryDump</tt> class provides a client for displaying the contents
    /// of a binary file in binary.
    /// <p/>
    /// For more full-featured versions, see the Unix utilities
    /// <tt>od</tt> (octal dump) and <tt>hexdump</tt> (hexadecimal dump).
    /// <p/>
    /// </summary>
    public class BinaryDump
    {
        private readonly string _text;
        private readonly int _bitsPerLine;

        public BinaryDump(string text, int bitsPerLine)
        {
            _text = text;
            _bitsPerLine = bitsPerLine;
        }

        public void Dump()
        {
            var binaryOut = new BinaryOut(_bitsPerLine);
            binaryOut.ResetTotal();
            binaryOut.Write(_text);
            Console.WriteLine($"{binaryOut.TotalBits} bits");
        }
    }
}
