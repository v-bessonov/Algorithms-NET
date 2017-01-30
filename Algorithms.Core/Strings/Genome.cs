using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Algorithms.Core.InOut;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>Genome</tt> class provides static methods for compressing
    /// and expanding a genomic sequence using a 2-bit code.
    /// <p/>
    /// </summary>
    public class Genome
    {
        private static readonly Alphabet DNA = new Alphabet("ACGT");
        private readonly BinaryOut _binaryOut;
        private int _length ;
        private List<byte> _bytes;

        public Genome()
        {
            _binaryOut = new BinaryOut(16);
        }


        /// <summary>
        /// Reads a sequence of 8-bit extended ASCII characters over the alphabet
        /// { A, C, T, G } from standard input; compresses them using two bits per
        /// character; and writes the results to standard output.
        /// </summary>
        /// <param name="genome"></param>
        public void Compress(string genome)
        {
            //var n = genome.Length;
            //_binaryOut.Write(n);
            // Write two-bit code for char.
            _length = genome.Length;
            var list = new List<byte>();
             _bytes = ToByteList(list, genome);
            _binaryOut.ResetTotal();
            _binaryOut.Write(_bytes.ToArray());
            Console.WriteLine();
            Console.WriteLine(_binaryOut.TotalBits);

            _binaryOut.ResetTotal();
            _binaryOut.Write(genome);
            Console.WriteLine(_binaryOut.TotalBits);

        }

        /// <summary>
        /// Reads a binary sequence from standard input; converts each two bits
        /// to an 8-bit extended ASCII character over the alphabet { A, C, T, G };
        /// and writes the results to standard output.
        /// </summary>
        public void Expand()
        {
            var sb = new StringBuilder();
            var lengthIterator = 0;
            foreach (var b in _bytes)
            {
                var bits = new BitArray(new []{b});
                var iterator = 0;
                
                var chBits = new BitArray(2);
                foreach (var bit in bits)
                {
                    chBits.Set(iterator, (bool)bit);
                    iterator++;
                    if (iterator != 2) continue;
                    var b1 = new byte[1];
                    chBits.CopyTo(b1, 0);
                    var ch = DNA.ToChar((int) b1[0]);
                    sb.Append(ch);
                    lengthIterator++;
                    if (lengthIterator >= _length)
                    {
                        break;
                    }
                    iterator = 0;
                    chBits = new BitArray(2);
                }
            }

            Console.WriteLine(sb.ToString());
        }

        public List<byte> ToByteList(List<byte> list, string genome)
        {
            var bits = new BitArray(8);
            var iterator = 0;
            var iteratorCh = 0;
            foreach (var ch in genome)
            {
                //if (ch == 'C')
                //{
                //    var tt = string.Empty;
                //    var tt1 = tt;
                //}
                var bools = GetBoolArrayByIndex(DNA.ToIndex(ch));
                if (bools == null) continue;
                foreach (var bl in bools)
                {
                    bits.Set(iterator, bl);
                    iterator++;
                    if (iterator != 8) continue;
                    var b = new byte[1];
                    bits.CopyTo(b, 0);
                    list.Add(b[0]);
                    iterator = 0;
                    bits = new BitArray(8);
                }
                if (iteratorCh == genome.Length - 1)
                {
                    var b = new byte[1];
                    bits.CopyTo(b, 0);
                    list.Add(b[0]);
                }
                iteratorCh++;
                //bool[] myBools = new bool[8] { true, false, true, true, false, true, false, false };
                //BitArray myBA4 = new BitArray(myBools);
                //byte[] b1 = new byte[1];
                //myBA4.CopyTo(b1, 0);

            }
            return list;
        }

        private static bool[] GetBoolArrayByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return new[] { false, false };
                case 1:
                    return new[] { true, false };
                case 2:
                    return new[] { false, true };
                case 3:
                    return new[] { true, true };
                default:
                    return null;
            }

        }
    }
}
