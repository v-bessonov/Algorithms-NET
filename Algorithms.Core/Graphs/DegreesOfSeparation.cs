using System;
using System.Collections.Generic;

namespace Algorithms.Core.Graphs
{
    /// <summary>
    /// The <tt>DegreesOfSeparation</tt> class provides a client for finding
    /// the degree of separation between one distinguished individual and
    /// every other individual in a social network.
    /// As an example, if the social network consists of actors in which
    /// two actors are connected by a link if they appeared in the same movie,
    /// and Kevin Bacon is the distinguished individual, then the client
    /// computes the Kevin Bacon number of every actor in the network.
    /// <p>
    /// The running time is proportional to the number of individuals and
    /// connections in the network. If the connections are given implicitly,
    /// as in the movie network example (where every two actors are connected
    /// if they appear in the same movie), the efficiency of the algorithm
    /// is improved by allowing both movie and actor vertices and connecting
    /// each movie to all of the actors that appear in that movie.
    /// </p>
    /// </summary>
    public class DegreesOfSeparation
    {
        private readonly SymbolGraph _sg;
        private readonly Graph _g;
        private readonly string _source;
        public DegreesOfSeparation(IList<string> lines, char delimiter, string source)
        {
            _sg = new SymbolGraph(lines, delimiter);
            _g = _sg.G;
            _source = source;
        }


        public void Find(string sink)
        {

            var s = _sg.Index(_source);
            var bfs = new BreadthFirstPaths(_g, s);

            if (_sg.Contains(sink))
            {
                var t = _sg.Index(sink);
                if (bfs.HasPathTo(t))
                {
                    foreach (int v in bfs.PathTo(t))
                    {
                        Console.WriteLine($"   {_sg.Name(v)}");
                    }
                }
                else
                {
                    Console.WriteLine("Not connected");
                }
            }
            else
            {
                Console.WriteLine("   Not in database.");
            }
        }

    }
}
