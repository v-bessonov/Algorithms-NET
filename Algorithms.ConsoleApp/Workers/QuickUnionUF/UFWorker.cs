using System;
using System.Collections.Generic;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.QuickUnionUF
{
    [ConsoleCommand("UF", "Represents a union-find data structure")]
    public class UFWorker : IWorker
    {
        /// <summary>
        /// Reads in a sequence of pairs of integers (between 0 and N-1) from standard input, 
        /// where each integer represents some object;
        /// if the objects are in different components, merge the two components
        /// and print the pair to standard output.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - tinyUF.txt"); // Prompt
            Console.WriteLine("2 - mediumUF.txt"); // Prompt
            Console.WriteLine("3 - largeUF.txt"); // Prompt
            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fieName;
            switch (fileNumber)
            {
                case "1":
                    fieName = "tinyUF.txt";
                    break;
                case "2":
                    fieName = "mediumUF.txt";
                    break;
                case "3":
                    fieName = "largeUF.txt";
                    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\QuickUnionUF\\{fieName}");
            var lines = @in.ReadAllLines();
            var iterator = 0;
            var n = 0;
            var unionsList = new List<UnionUF>();
            foreach (var line in lines)
            {
                if (iterator == 0)
                {
                    n = Convert.ToInt32(line);
                }
                else
                {
                    var lineSplitted = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (lineSplitted.Length == 2)
                    {
                        var p = Convert.ToInt32(lineSplitted[0]);
                        var q = Convert.ToInt32(lineSplitted[1]);
                        unionsList.Add(new UnionUF { P = p, Q = q });
                    }
                }

                iterator++;
            }

            var uf = new Core.QuickUnionUF.UF(n);
            iterator = 0;
            foreach (var union in unionsList)
            {
                if (uf.Connected(union.P, union.Q)) continue;
                uf.Union(union.P, union.Q);
                //Console.WriteLine("{0} {1}", union.P, union.Q);
                iterator++;
                Console.WriteLine($"{ union.P} {union.Q}");
            }
            Console.WriteLine($"{uf.Count()} components");
            Console.ReadLine();
        }
    }
}
