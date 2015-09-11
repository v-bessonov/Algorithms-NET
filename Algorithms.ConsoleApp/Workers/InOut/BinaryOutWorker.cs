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


            var binaryOut = new BinaryOut();
            binaryOut.Write(1005);
            Console.ReadLine();
        }
    }
}
