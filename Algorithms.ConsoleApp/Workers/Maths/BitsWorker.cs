using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Maths;
using System;

namespace Algorithms.ConsoleApp.Workers.Maths
{
    [ConsoleCommand("Bits", "Bits manipulations")]
    public class BitsWorker : IWorker
    {
        private const string HR_LINE = "----------------------------------------";
        public void Run()
        {

            GetBits();
            SetBits();
            ClearBits();
            UpdateBits();
            IsEven();
            IsPositive();
            MultiplyByTwo();
            DivideByTwo();
            Console.ReadLine();
        }

        private void GetBits()
        {
            uint number = 0b_1110_1000;
            int bitPosition = 3;
            Console.WriteLine("Method - GetBits");
            Console.WriteLine(Convert.ToString(number, toBase: 2));
            Console.WriteLine($"{bitPosition} - i bit position = {Convert.ToString(Bits.GetBits(number, bitPosition), toBase: 2)}");
            Console.WriteLine(HR_LINE);
        }

        private void SetBits()
        {
            uint number = 0b_1110_1000;
            int bitPosition = 2;
            Console.WriteLine("Method - SetBits");
            Console.WriteLine(Convert.ToString(number, toBase: 2));
            Console.WriteLine($"{bitPosition} - i bit position = {Convert.ToString(Bits.SetBits(number, bitPosition), toBase: 2)}");
            Console.WriteLine(HR_LINE);
        }

        private void ClearBits()
        {
            uint number = 0b_1110_1000;
            int bitPosition = 3;
            Console.WriteLine("Method - ClearBits");
            Console.WriteLine(Convert.ToString(number, toBase: 2));
            Console.WriteLine($"{bitPosition} - i bit position = {Convert.ToString(Bits.ClearBits(number, bitPosition), toBase: 2)}");
            Console.WriteLine(HR_LINE);
        }

        private void UpdateBits()
        {
            uint number = 0b_1110_1000;
            int bitPosition = 3;
            bool bitValue = false;
            Console.WriteLine("Method - UpdateBits");
            Console.WriteLine(Convert.ToString(number, toBase: 2));
            Console.WriteLine($"{bitPosition} - i bit position = {Convert.ToString(Bits.UpdateBits(number, bitPosition, bitValue), toBase: 2)}");
            bitValue = true;
            Console.WriteLine($"{bitPosition} - i bit position = {Convert.ToString(Bits.UpdateBits(number, bitPosition, bitValue), toBase: 2)}");
            Console.WriteLine(HR_LINE);
        }

        private void IsEven()
        {
            int number5 = 0b_0101;
            int number4 = 0b_0100;
            Console.WriteLine("Method - IsEven");
            Console.WriteLine($"number5 : {Convert.ToString(number5, toBase: 10)}");
            Console.WriteLine($"number5 : {Convert.ToString(number5, toBase: 2)}");
            Console.WriteLine($"number5 - IsEven : {Convert.ToString(Bits.IsEven(number5))}");

            Console.WriteLine($"number4 : {Convert.ToString(number4, toBase: 10)}");
            Console.WriteLine($"number4 : {Convert.ToString(number4, toBase: 2)}");
            Console.WriteLine($"number4 - IsEven : {Convert.ToString(Bits.IsEven(number4))}");
            Console.WriteLine(HR_LINE);
        }

        private void IsPositive()
        {
            int number1 = 0b_0001;
            int numberminus1 = -0b_0001;
            Console.WriteLine("Method - IsPositive");
            Console.WriteLine($"number1 : {Convert.ToString(number1, toBase: 10)}");
            Console.WriteLine($"number1 : {Convert.ToString(number1, toBase: 2)}");
            Console.WriteLine($"number1 - IsPositive : {Convert.ToString(Bits.IsPositive(number1))}");

            Console.WriteLine($"numberminus1 : {Convert.ToString(numberminus1, toBase: 10)}");
            Console.WriteLine($"numberminus1 : {Convert.ToString(numberminus1, toBase: 2)}");
            Console.WriteLine($"numberminus1 - IsPositive : {Convert.ToString(Bits.IsPositive(numberminus1))}");
            Console.WriteLine(HR_LINE);
        }

        private void MultiplyByTwo()
        {
            int number = 0b_0101;
            Console.WriteLine("Method - MultiplyByTwo");
            Console.WriteLine($"Before the shift number : {Convert.ToString(number, toBase: 10)}");
            Console.WriteLine($"Before the shift number : {Convert.ToString(number, toBase: 2)}");
            

            int shift = Bits.MultiplyByTwo(number);
            Console.WriteLine($"After the shift number : {Convert.ToString(shift, toBase: 10)}");
            Console.WriteLine($"After the shift number : {Convert.ToString(shift, toBase: 2)}"); 
            Console.WriteLine(HR_LINE);
        }

        private void DivideByTwo()
        {
            int number = 0b_0101;
            Console.WriteLine("Method - MultiplyByTwo");
            Console.WriteLine($"Before the shift number : {Convert.ToString(number, toBase: 10)}");
            Console.WriteLine($"Before the shift number : {Convert.ToString(number, toBase: 2)}");


            int shift = Bits.DivideByTwo(number);
            Console.WriteLine($"After the shift number : {Convert.ToString(shift, toBase: 10)}");
            Console.WriteLine($"After the shift number : {Convert.ToString(shift, toBase: 2)}");
            Console.WriteLine(HR_LINE);
        }
    }
}
