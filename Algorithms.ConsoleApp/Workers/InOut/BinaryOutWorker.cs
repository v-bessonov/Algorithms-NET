using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.InOut
{
    [ConsoleCommand("BinaryOut", "BinaryOut library")]
    public class BinaryOutWorker : IWorker
    {
        public void Run()
        {
            //Console.WriteLine("Choose file:"); // Prompt
            //Console.WriteLine("1 - tinyT.txt"); // Prompt
            //Console.WriteLine("2 - tinyW.txt"); // Prompt
            //Console.WriteLine("3 - largeT.txt"); // Prompt
            //Console.WriteLine("4 - largeW.txt"); // Prompt
            //Console.WriteLine("or quit"); // Prompt

            //var fileNumber = Console.ReadLine();
            //var fieName = string.Empty;
            //switch (fileNumber)
            //{
            //    case "1":
            //        fieName = "tinyT.txt";
            //        break;
            //    case "2":
            //        fieName = "tinyW.txt";
            //        break;
            //    case "3":
            //        fieName = "largeT.txt";
            //        break;
            //    case "4":
            //        fieName = "largeW.txt";
            //        break;
            //    case "quit":
            //        return;
            //    default:
            //        return;
            //}


            var binaryOut = new BinaryOut(16);

            binaryOut.Write((byte)6);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();


            binaryOut.Write(true);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();


            binaryOut.Write(false);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();


            binaryOut.Write(short.MaxValue);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();



            binaryOut.Write((short)5);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();



            binaryOut.Write(int.MaxValue);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

           


            binaryOut.Write((int)5);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            

            binaryOut.Write(long.MaxValue);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            binaryOut.Write((long)5);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            


            binaryOut.Write(float.MaxValue);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            


            binaryOut.Write((float)5.0);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();


            binaryOut.Write(double.MaxValue);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            


            binaryOut.Write((double)5.0);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

           


            binaryOut.Write(decimal.MaxValue);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            


            binaryOut.Write((decimal)5.0);
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            binaryOut.Write('r');
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            binaryOut.Write("test");
            Console.WriteLine(binaryOut.TotalBits);
            binaryOut.ResetTotal();
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
