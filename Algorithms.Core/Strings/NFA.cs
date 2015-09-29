using Algorithms.Core.Collections;
using Algorithms.Core.Graphs;
using Algorithms.Core.Helpers;

namespace Algorithms.Core.Strings
{
    /// <summary>
    /// The <tt>NFA</tt> class provides a data type for creating a
    /// <em>nondeterministic finite state automaton</em> (NFA) from a regular
    /// expression and testing whether a given string is matched by that regular
    /// expression.
    /// It supports the following operations: <em>concatenation</em>,
    /// <em>closure</em>, <em>binary or</em>, and <em>parentheses</em>.
    /// It does not support <em>mutiway or</em>, <em>character classes</em>,
    /// <em>metacharacters</em> (either in the text or pattern),
    /// <em>capturing capabilities</em>, <em>greedy</em> or <em>relucantant</em>
    /// modifiers, and other features in industrial-strength implementations
    /// such as {@link java.util.regex.Pattern} and {@link java.util.regex.Matcher}.
    /// <p>
    /// This implementation builds the NFA using a digraph and a stack
    /// and simulates the NFA using digraph search (see the textbook for details).
    /// The constructor takes time proportional to <em>M</em>, where <em>M</em>
    /// is the number of characters in the regular expression.
    /// The <em>recognizes</em> method takes time proportional to <em>M N</em>,
    /// where <em>N</em> is the number of characters in the text.
    /// </p>
    /// </summary>
    public class NFA
    {
        private readonly Digraph _g;         // digraph of epsilon transitions
        private readonly string _regexp;     // regular expression
        private readonly int _m;             // number of characters in regular expression

        /// <summary>
        /// Initializes the NFA from the specified regular expression.
        /// </summary>
        /// <param name="regexp">regexp the regular expression</param>
        public NFA(string regexp)
        {
            _regexp = regexp;
            _m = regexp.Length;
            var ops = new Stack<Integer>();
            _g = new Digraph(_m + 1);
            for (var i = 0; i < _m; i++)
            {
                var lp = i;
                if (regexp[i] == '(' || regexp[i] == '|')
                    ops.Push(i);
                else if (regexp[i] == ')')
                {
                    int or = ops.Pop();

                    // 2-way or operator
                    if (regexp[or] == '|')
                    {
                        lp = ops.Pop();
                        _g.AddEdge(lp, or + 1);
                        _g.AddEdge(or, i);
                    }
                    else if (regexp[or] == '(')
                        lp = or;
                    //else assert false;
                }

                // closure operator (uses 1-character lookahead)
                if (i < _m - 1 && regexp[i + 1] == '*')
                {
                    _g.AddEdge(lp, i + 1);
                    _g.AddEdge(i + 1, lp);
                }
                if (regexp[i] == '(' || regexp[i] == '*' || regexp[i] == ')')
                    _g.AddEdge(i, i + 1);
            }
        }

        /// <summary>
        /// Does the NFA recognize txt? 
        /// Returns true if the text is matched by the regular expression.
        /// </summary>
        /// <param name="txt">txt the text</param>
        /// <returns><tt>true</tt> if the text is matched by the regular expression, <tt>false</tt> otherwise</returns>
        public bool Recognizes(string txt)
        {
            var dfs = new DirectedDFS(_g, 0);
            var pc = new Bag<Integer>();
            for (var v = 0; v < _g.V; v++)
                if (dfs.Marked(v)) pc.Add(v);

            // Compute possible NFA states for txt[i+1]
            for (var i = 0; i < txt.Length; i++)
            {
                var match = new Bag<Integer>();
                foreach (int v in pc)
                {
                    if (v == _m) continue;
                    if ((_regexp[v] == txt[i]) || _regexp[v] == '.')
                        match.Add(v + 1);
                }
                dfs = new DirectedDFS(_g, match);
                pc = new Bag<Integer>();
                for (var v = 0; v < _g.V; v++)
                    if (dfs.Marked(v)) pc.Add(v);

                // optimization if no states reachable
                if (pc.Size() == 0) return false;
            }

            // check for accept state
            foreach (int v in pc)
                if (v == _m) return true;
            return false;
        }

    }
}
