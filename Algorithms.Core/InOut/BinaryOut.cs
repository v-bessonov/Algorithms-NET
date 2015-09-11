using System;
using System.IO;

namespace Algorithms.Core.InOut
{
    public class BinaryOut
    {
        //private BufferedOutputStream out;  // the output stream
    private int buffer;                // 8-bit buffer of bits to write out
        private int n;                     // number of bits remaining in buffer

        /**
     * Writes the specified bit to the binary output stream.
     * @param x the bit
     */
        private void WriteBit(bool x)
        {
            // add bit to buffer
            buffer <<= 1;
            if (x) buffer |= 1;

            // if buffer is full (8 bits), write out as a single byte
            n++;
            if (n == 8) ClearBuffer();
        }

        /**
          * Writes the 8-bit byte to the binary output stream.
          * @param x the byte
          */
        private void WriteByte(int x)
        {
            //assert x >= 0 && x < 256;

            // optimized if byte-aligned
            if (n == 0)
            {
                try
                {
                    //out.write(x);
                    Console.Write(x);

                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            }

            // otherwise write one bit at a time
            for (var i = 0; i < 8; i++)
            {
                var bit = ((Zfrs(x ,(8 - i - 1))) & 1) == 1;
                WriteBit(bit);
            }
        }

        private static int Zfrs(int i, int j)
        {
            var maskIt = (i < 0);
            i = i >> j;
            if (maskIt)
                i &= 0x7FFFFFFF;
            return i;
        }

        // write out any remaining bits in buffer to the binary output stream, padding with 0s
        private void ClearBuffer()
        {
            if (n == 0) return;
            if (n > 0) buffer <<= (8 - n);
            try
            {
            //out.write(buffer);
                Console.Write(buffer);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                
            }
            n = 0;
            buffer = 0;
        }

        /**
          * Flushes the binary output stream, padding 0s if number of bits written so far
          * is not a multiple of 8.
          */
        //public void flush()
        //{
        //    clearBuffer();
        //    try
        //    {
        //    out.flush();
        //    }
        //    catch (IOException e)
        //    {
        //        e.printStackTrace();
        //    }
        //}

        /**
          * Closes and flushes the binary output stream.
          * Once it is closed, bits can no longer be written.
          */
        //public void close()
        //{
        //    flush();
        //    try
        //    {
        //    out.close();
        //    }
        //    catch (IOException e)
        //    {
        //        e.printStackTrace();
        //    }
        //}


        /**
          * Writes the specified bit to the binary output stream.
          * @param x the <tt>boolean</tt> to write
          */
        public void Write(bool x)
        {
            WriteBit(x);
        }

        /**
     * Writes the 8-bit byte to the binary output stream.
     * @param x the <tt>byte</tt> to write.
     */
        public void Write(byte x)
        {
            WriteByte(x & 0xff);
        }

        /**
          * Writes the 32-bit int to the binary output stream.
          * @param x the <tt>int</tt> to write
          */
        public void Write(int x)
        {
            WriteByte((Zfrs(x , 24)) & 0xff);
            WriteByte((Zfrs(x , 16)) & 0xff);
            WriteByte((Zfrs(x , 8)) & 0xff);
            WriteByte((Zfrs(x , 0)) & 0xff);
        }
    }
}
