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
    }
}
