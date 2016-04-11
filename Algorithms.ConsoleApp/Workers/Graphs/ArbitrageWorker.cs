using System;
using System.Collections.Generic;
using System.Globalization;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("Arbitrage", "Class provides a client that that finds an arbitrage opportunity in a currency exchange table")]
    public class ArbitrageWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - rates.txt"); // Prompt
            //Console.WriteLine("2 - mediumEWD.txt"); // Prompt
            //Console.WriteLine("3 - mediumEWG.txt"); // Prompt

            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "rates.txt";
                    break;
                //case "2":
                //    fileName = "mediumEWD.txt";
                //    break;
                //case "3":
                //    fileName = "largeEWG.zip";
                //    break;
                case "quit":
                    return;
                default:
                    return;
            }


            var @in = new In($"Files\\Graphs\\{fileName}");
            var lines = @in.ReadAllLines();



            var lineIterator = 0;
            var currencyIterator = 0;
            // number of jobs
            var n = 0;

            // source and sink
            var source = 0;
            var sink = 0;

            var v = 0;
            var e = 0;
            var edges = new List<DirectedEdge>();
            string[] names = {};
            foreach (var line in lines)
            {
                if (lineIterator == 0)
                {
                    v = Convert.ToInt32(line);
                    names = new string[v];

                }



                if (lineIterator > 0)
                {
                    var wordsIterator = 0;
                    var ratesIterator = 0;
                    var lineSplitted = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in lineSplitted)
                    {
                        if (wordsIterator == 0)
                        {
                            names[wordsIterator] = word;
                        }

                        if (wordsIterator > 0)
                        {
                            double rate = Convert.ToSingle(word,CultureInfo.InvariantCulture);
                            var edge = new DirectedEdge(currencyIterator, ratesIterator, -Math.Log(rate));
                            edges.Add(edge);
                            e++;
                            ratesIterator++;
                        }
                        wordsIterator ++;
                    }

                    currencyIterator++;

                }

                lineIterator++;

            }

            var edgeWeightedDigraph = new EdgeWeightedDigraph(v, e, edges);
            Console.WriteLine(edgeWeightedDigraph);


            // find shortest path from s to each other vertex in DAG
            var arbitrage = new Arbitrage(edgeWeightedDigraph,source);
            var spt = arbitrage.GetShotestPath();
            // print results
            Console.WriteLine(" job   start  finish");
            Console.WriteLine("--------------------");
            if (spt.HasNegativeCycle())
            {
                var stake = 1000.0;
                foreach (var edge in spt.NegativeCycle())
                {
                    Console.Write($"{stake:0.00000} {names[edge.From()]} ");
                    stake *= Math.Exp(-edge.Weight);
                    Console.Write($"= {stake:0.00000} {names[edge.To()]}{Environment.NewLine}");
                }
            }
            else
            {
                Console.WriteLine("No arbitrage opportunity");
            }

            Console.ReadLine();
        }
    }
}
