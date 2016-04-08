namespace Algorithms.Core.Graphs
{
    ///  The <tt>CPM</tt> class provides a client that solves the
    ///  parallel precedence-constrained job scheduling problem
    ///  via the <em>critical path method</em>. It reduces the problem
    ///  to the longest-paths problem in edge-weighted DAGs.
    ///  It builds an edge-weighted digraph (which must be a DAG)
    ///  from the job-scheduling problem specification,
    ///  finds the longest-paths tree, and computes the longest-paths
    ///  lengths (which are precisely the start times for each job).
    ///  <p>
    ///  This implementation uses {@link AcyclicLP} to find a longest
    ///  path in a DAG.
    ///  The running time is proportional to <em>V</em> + <em>E</em>,
    ///  where <em>V</em> is the number of jobs and <em>E</em> is the
    ///  number of precedence constraints.
    ///  </p>
    public class CPM
    {
        private EdgeWeightedDigraph _g;
        private readonly int _source;
        public CPM(EdgeWeightedDigraph g, int source)
        {
            _g = g;
            _source = source;

        }

        public AcyclicLP GetLongestPath()
        {
            // compute longest path
            return new AcyclicLP(_g, _source);
        }
    }
}
