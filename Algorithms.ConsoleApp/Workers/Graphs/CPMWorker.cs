using System;
using System.Collections.Generic;
using System.Globalization;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Graphs;
using Algorithms.Core.InOut;

namespace Algorithms.ConsoleApp.Workers.Graphs
{
    [ConsoleCommand("CPM", "Class provides a client that solves the parallel precedence-constrained job scheduling problem via the <em>critical path method</em>")]
    public class CPMWorker : IWorker
    {
        public void Run()
        {
            Console.WriteLine("Choose file:"); // Prompt
            Console.WriteLine("1 - jobsPC.txt"); // Prompt
            //Console.WriteLine("2 - mediumEWD.txt"); // Prompt
            //Console.WriteLine("3 - mediumEWG.txt"); // Prompt

            Console.WriteLine("or quit"); // Prompt

            var fileNumber = Console.ReadLine();
            string fileName;
            switch (fileNumber)
            {
                case "1":
                    fileName = "jobsPC.txt";
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
            var i = 0;
            // number of jobs
            var n = 0;

            // source and sink
            var source = 0;
            var sink = 0;

            var v = 0;
            var e = 0;
            var edges = new List<DirectedEdge>();
            foreach (var line in lines)
            {
                if (lineIterator == 0)
                {
                    n = Convert.ToInt32(line);
                    source = 2*n;
                    sink = 2*n + 1;
                    v = sink+1;
                }



                if (lineIterator > 0)
                {
                    var lineSplitted = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                    var duration = Convert.ToDouble(lineSplitted[0], CultureInfo.InvariantCulture);

                    edges.Add(new DirectedEdge(source, i, 0.0));
                    e++;
                    edges.Add(new DirectedEdge(i + n, sink, 0.0));
                    e++;
                    edges.Add(new DirectedEdge(i, i + n, duration));
                    e++;

                    // precedence constraints
                    var m = Convert.ToInt32(lineSplitted[1], CultureInfo.InvariantCulture);
                    for (var j = 0; j < m; j++)
                    {
                        var precedent = Convert.ToInt32(lineSplitted[1 + (j + 1)], CultureInfo.InvariantCulture);
                        edges.Add(new DirectedEdge(n + i, precedent, 0.0));
                        e++;
                    }

                    i++;
                }

                lineIterator++;

            }

            var edgeWeightedDigraph = new EdgeWeightedDigraph(v, e, edges);
            Console.WriteLine(edgeWeightedDigraph);


            // find shortest path from s to each other vertex in DAG
            var lp = new AcyclicLP(edgeWeightedDigraph, source);
            // print results
            Console.WriteLine(" job   start  finish");
            Console.WriteLine("--------------------");
            for (var k = 0; k < n; k++)
            {
                Console.Write($"{k} {lp.DistTo(k)} {lp.DistTo(k + n)}{Environment.NewLine}");
            }
            Console.Write($"Finish time: {lp.DistTo(sink)}{Environment.NewLine}");

            Console.ReadLine();
        }
    }
}
