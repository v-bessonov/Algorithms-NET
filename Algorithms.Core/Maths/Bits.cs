﻿namespace Algorithms.Core.Maths
{
    /// <summary>
    /// Bits manipulations
    /// </summary>
    public class Bits
    {
        /// <summary>
        /// This method shifts the relevant bit to the zeroth position. 
        /// Then we perform AND operation with one which has bit pattern like 0001. 
        /// This clears all bits from the original number except the relevant one. 
        /// If the relevant bit is one, the result is 1, otherwise the result is 0.
        /// </summary>
        public static uint GetBits(uint number, int bitPosition)
        {
            return (number >> bitPosition) & 1;
        }

        /// <summary>
        /// This method shifts 1 over by bitPosition bits, creating a value 
        /// that looks like 00100. Then we perform OR operation that sets specific 
        /// bit into 1 but it does not affect on other bits of the number.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static uint SetBits(uint number, int bitPosition)
        {
            return number | (uint)(1 << bitPosition);
        }

        /// <summary>
        /// This method shifts 1 over by bitPosition bits, creating a value that looks like 00100. 
        /// Than it inverts this mask to get the number that looks like 11011. 
        /// Then AND operation is being applied to both the number and the mask. 
        /// That operation unsets the bit.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static uint ClearBits(uint number, int bitPosition)
        {
            var mask = ~(uint)(1 << bitPosition);
            return number & mask;
        }

        /// <summary>
        /// This method is a combination of "Clear Bit" and "Set Bit" methods.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bitPosition"></param>
        /// <param name="bitValue"></param>
        /// <returns></returns>
        public static uint UpdateBits(uint number, int bitPosition, bool bitValue)
        {
            // Normalized bit value.
            var bitValueNormalized = bitValue ? 1 : 0;

            // Init clear mask.
            var clearMask = ~(uint)(1 << bitPosition);

            // Clear bit value and then set it up to required value.
            return (number & clearMask) | (uint)(bitValueNormalized << bitPosition);
        }

        /// <summary>
        /// This method determines if the number provided is even. 
        /// It is based on the fact that odd numbers have their last right bit to be set to 1.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsEven(int number)
        {
            return (number & 1) == 0;
        }

        /// <summary>
        /// This method determines if the number is positive. 
        /// It is based on the fact that all positive numbers have their leftmost bit to be set to 0. 
        /// However, if the number provided is zero or negative zero, it should still return false.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPositive(int number)
        {
            // Zero is neither a positive nor a negative number.
            if (number == 0)
            {
                return false;
            }

            // The most significant 32nd bit can be used to determine whether the number is positive.
            return ((number >> 31) & 1) == 0;
        }

        /// <summary>
        /// This method shifts original number by one bit to the left. 
        /// Thus all binary number components (powers of two) are being multiplying by two 
        /// and thus the number itself is being multiplied by two.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int MultiplyByTwo(int number)
        {
            return number << 1;
        }

        /// <summary>
        /// This method shifts original number by one bit to the right. 
        /// Thus all binary number components (powers of two) are being divided by two 
        /// and thus the number itself is being divided by two without remainder.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int DivideByTwo(int number)
        {
            return number >> 1;
        }

        
    }
}
