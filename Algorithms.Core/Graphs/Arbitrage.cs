namespace Algorithms.Core.Graphs
{
    /// <summary>
    ///   The <tt>Arbitrage</tt> class provides a client that finds an arbitrage
    ///   opportunity in a currency exchange table by constructing a
    ///   complete-digraph representation of the exchange table and then finding
    ///   a negative cycle in the digraph.
    ///   <p/>
    ///   This implementation uses the Bellman-Ford algorithm to find a
    ///   negative cycle in the complete digraph.
    ///   The running time is proportional to <em>V</em><sup>3</sup> in the
    ///   worst case, where <em>V</em> is the number of currencies.
    ///   <p/>
    /// </summary>
    public class Arbitrage
    {
        private EdgeWeightedDigraph _g;
        private readonly int _source;
        public Arbitrage(EdgeWeightedDigraph g, int source)
        {
            _g = g;
            _source = source;

        }

        public BellmanFordSP GetShotestPath()
        {
            // compute shotest path
            return new BellmanFordSP(_g, _source);
        }
    }
}
