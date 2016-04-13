using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Core.Graphs
{
    public class AdjEnumerator: IEnumerator<DirectedEdge>, IEnumerable<DirectedEdge>
    {
        private readonly int _v;
        private int _w;
        private readonly int _vv;
        private readonly DirectedEdge[][] _adj;

        public AdjEnumerator(int vv, int v, DirectedEdge[][] adj)
        {
            _v = v;
            _vv = vv;
            _adj = adj;
        }

        public void Dispose()
        {
        }

        public bool HasNext()
        {
            while (_w < _vv)
            {
                if (_adj[_v][_w] != null) return true;
                _w++;
            }
            return false;
        }

        public bool MoveNext()
        {
            if (!HasNext())
            {
                return false;
            }

            Current = _adj[_v][_w++];
            return true;

        }

        public void Reset()
        {
            Current = _adj[_v][0];
        }

        public DirectedEdge Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public IEnumerator<DirectedEdge> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
