using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.InOut
{
    public class BinaryOut
    {
        //private BufferedOutputStream out;  // the output stream
        //private int buffer;                // 8-bit buffer of bits to write out
        //private int n;                     // number of bits remaining in buffer
        private int _radix;
        public int TotalBits { get; private set; } 
        public BinaryOut()
        {
            TotalBits = 0;
            _radix = 8;
        }
        public BinaryOut(int radix)
        {
            TotalBits = 0;
            if (radix != 8 && radix != 16 && radix != 32 && radix != 64)
                _radix = 8;
            else _radix = radix;
        }
        public void ResetTotal()
        {
            TotalBits = 0;
        }

        /**
          * Writes the 8-bit byte to the binary output stream.
          * @param x the byte
          */
        //private void WriteByte(int x)
        //{
        //    //assert x >= 0 && x < 256;

        //    // optimized if byte-aligned
        //    if (n == 0)
        //    {
        //        try
        //        {
        //            //out.write(x);
        //            Console.Write(x);

        //        }
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        return;
        //    }

        //    // otherwise write one bit at a time
        //    for (var i = 0; i < 8; i++)
        //    {
        //        var bit = ((Zfrs(x ,(8 - i - 1))) & 1) == 1;
        //        WriteBit(bit);
        //    }
        //}

        //private static int Zfrs(int i, int j)
        //{
        //    var maskIt = (i < 0);
        //    i = i >> j;
        //    if (maskIt)
        //        i &= 0x7FFFFFFF;
        //    return i;
        //}

        public void WriteBit(bool x)
        {
            if (x) Console.Write(1);
            else Console.WriteLine(0);

        }

        public void Write(bool x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        /**
     * Writes the 8-bit byte to the binary output stream.
     * @param x the <tt>byte</tt> to write.
     */
        public void Write(byte x)
        {
            var bytes = new[] { x };
            Write(bytes);
        }



        /**
          * Writes the 32-bit int to the binary output stream.
          * @param x the <tt>int</tt> to write
          */
        public void Write(int x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        public void Write(short x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        public void Write(long x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        public void Write(float x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        public void Write(double x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        public void Write(decimal x)
        {
            //Load four 32 bit integers from the Decimal.GetBits function
            var bits = decimal.GetBits(x);
            //Create a temporary list to hold the bytes
            var bytes = new List<byte>();
            //iterate each 32 bit integer
            foreach (var i in bits)
            {
                //add the bytes of the current 32bit integer
                //to the bytes list
                bytes.AddRange(BitConverter.GetBytes(i));
            }
            //return the bytes list as an array
            Write(bytes.ToArray());
        }

        public void Write(char x)
        {
            var bytes = BitConverter.GetBytes(x);
            Write(bytes);
        }

        //public void Write(char x, int r)
        //{
        //    if (r == 16)
        //    {
        //        Write(x);
        //        return;
        //    }
        //    if (r < 1 || r > 16) throw new ArgumentException("Illegal value for r = " + r);
        //    //if (x >= (1 << r)) throw new ArgumentException("Illegal " + r + "-bit char = " + x);
        //    var buffer = new StringBuilder();
        //    for (var i = 0; i < r; i++)
        //    {
        //        var bit = ((Zfrs(x, (r - i - 1))) & 1) == 1;
        //        buffer.Append(bit ? "1" : "0");
        //        //WriteBit(bit);
        //    }
        //    Console.Write(Reverse(buffer.ToString()));
        //}


        public void Write(string x)
        {
            foreach (var ch in x)
            {
                var bytes = BitConverter.GetBytes(ch);
                Write(bytes);
            }

        }

        public void Write(byte[] bytes)
        {
            var sb = new StringBuilder();
            var bitsIterator = 0;
            foreach (var b in bytes)
            {
                //sb.Insert(0, Convert.ToString(b, 2).PadLeft(8, '0'));
                //sb.Append(Reverse(Convert.ToString(b, 2)).PadRight(8, '0'));
                sb.AppendFormat(Convert.ToString(b, 2).PadLeft(8, '0'));
                if (bitsIterator < _radix)
                {
                    
                    bitsIterator += 8;
                }
                if (bitsIterator == _radix)
                {

                    bitsIterator = 0;
                    sb.AppendFormat(Environment.NewLine);
                }

               
                TotalBits += 8;
            }
            Console.Write(sb.ToString());
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
