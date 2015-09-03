using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("LookupCSV", "Provides a data-driven client for reading in a key-value pairs from a file; then, printing the values corresponding to the keys found on standard input.")]
    public class LookupCSVWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - ip.csv"); // Prompt
            Console.WriteLine("2 - DJIA.csv"); // Prompt
            Console.WriteLine("3 - amino.csv"); // Prompt
            Console.WriteLine("4 - UPC.zip"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            var fieName = string.Empty;
            switch (fileNumber)
            {
                case "1":
                    fieName = "ip.csv";
                    break;
                case "2":
                    fieName = "DJIA.csv";
                    break;
                case "3":
                    fieName = "amino.csv";
                    break;
                case "4":
                    fieName = "UPC.zip";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Searching\\{fieName}");
            var lines = !fieName.EndsWith("zip"  ) ? @in.ReadAllLines() : @in.ReadAllLinesFromZip();


            //var list = words.Select(word => new StringComparable(word)).ToList();
            //var listComparable = list.Cast<IComparable>().ToList();
            //var arrayComparable = list.Cast<IComparable>().ToArray();
            //var listStrings = words.ToList();

            var lookupCSV =  new LookupCSV();

            foreach (var line in lines)
            {
                var lineSplitted = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (lineSplitted.Length >= 2)
                {
                    var key = lineSplitted[0];
                    var value = lineSplitted[1];
                    lookupCSV.InsertItem(key, value);
                }
            }

            lookupCSV.Check("cnzz.com");
            lookupCSV.Check("americanexpress.com");
            lookupCSV.Check("www.1gl.ru");
            Console.WriteLine("----------------------------------------------------------------------");
            lookupCSV.Check("26-Oct-28");
            lookupCSV.Check("22-Oct-28");
            lookupCSV.Check("32-Oct-28");
            Console.WriteLine("----------------------------------------------------------------------");
            lookupCSV.Check("CAA");
            lookupCSV.Check("ACT");
            lookupCSV.Check("GRR");
            Console.WriteLine("----------------------------------------------------------------------");
            lookupCSV.Check("0000000010344");
            lookupCSV.Check("0000000492515");
            lookupCSV.Check("5024425379607");

            Console.ReadLine();
        }
    }
}
