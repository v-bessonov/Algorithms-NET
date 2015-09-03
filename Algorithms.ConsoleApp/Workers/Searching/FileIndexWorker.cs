using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;
using Algorithms.Core.Searching;

namespace Algorithms.ConsoleApp.Workers.Searching
{
    [ConsoleCommand("FileIndex", "Provides a client that for indexing a set of files")]
    public class FileIndexWorker : IWorker
    {
        public void Run()
        {

            var fileIndex = new FileIndex();

            var fileNames = new List<string> { "ex1.txt", "ex2.txt", "ex3.txt", "ex4.txt" };

            foreach (var fileName in fileNames)
            {
                var @in = new In($"Files\\Searching\\{fileName}");
                var words = @in.ReadAllStrings();
                fileIndex.CreateIndex(fileName, words);
            }


            fileIndex.ShowFilesByQuery("was");
            Console.WriteLine("------------------------------------");
            fileIndex.ShowFilesByQuery("age");
            Console.WriteLine("------------------------------------");
            fileIndex.ShowFilesByQuery("wisdom");



            Console.ReadLine();
        }
    }
}
