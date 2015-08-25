using System;
using System.Collections.Generic;

namespace Algorithms.Core.Searching
{
    /// <summary>
    /// The <tt>BST</tt> class represents an ordered symbol table of generic
    /// key-value pairs.
    /// It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
    /// <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
    /// It also provides ordered methods for finding the <em>minimum</em>,
    /// <em>maximum</em>, <em>floor</em>, and <em>ceiling</em>.
    /// It also provides a <em>keys</em> method for iterating over all of the keys.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p>
    /// This implementation uses a left-leaning red-black BST. It requires that
    /// the key type implements the <tt>Comparable</tt> interface and calls the
    /// <tt>compareTo()</tt> and method to compare two keys. It does not call either
    /// <tt>equals()</tt> or <tt>hashCode()</tt>.
    /// The <em>put</em>, <em>contains</em>, <em>remove</em>, <em>minimum</em>,
    /// <em>maximum</em>, <em>ceiling</em>, and <em>floor</em> operations each take
    /// logarithmic time in the worst case, if the tree becomes unbalanced.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class RedBlackBST<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {
        private const bool RED = true;
        private const bool BLACK = false;
        private Node<TKey, TValue> _root;             // root of BST



        #region Node helper methods.
        /// <summary>
        /// is node x red; false if x is null ?
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool IsRed(Node<TKey, TValue> x)
        {
            return x?.Color == RED;
        }

        /// <summary>
        /// number of node in subtree rooted at x; 0 if x is null
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Size(Node<TKey, TValue> x)
        {
            return x?.N ?? 0;
        }


        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns>the number of key-value pairs in this symbol table</returns>
        public int Size()
        {
            return Size(_root);
        }

        /// <summary>
        /// Is this symbol table empty?
        /// </summary>
        /// <returns><tt>true</tt> if this symbol table is empty and <tt>false</tt> otherwise</returns>
        public bool IsEmpty()
        {
            return _root == null;
        }
        #endregion


        #region Standard BST search.
        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TValue Get(TKey key)
        {
            return Get(_root, key);
        }

        /// <summary>
        /// value associated with the given key in subtree rooted at x; null if no such key
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private TValue Get(Node<TKey, TValue> x, TKey key)
        {
            while (x != null)
            {
                var cmp = key.CompareTo(x.Key);
                if (cmp < 0) x = x.Left;
                else if (cmp > 0) x = x.Right;
                else return x.Value;
            }
            return null;
        }

        /// <summary>
        /// Does this symbol table contain the given key?
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this symbol table contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public bool Contains(TKey key)
        {
            return Get(key) != null;
        }
        #endregion


        #region Red-black tree insertion.
        /// <summary>
        /// Inserts the key-value pair into the symbol table, overwriting the old value
        /// with the new value if the key is already in the symbol table.
        /// If the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <param name="val">val the value</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Put(TKey key, TValue val)
        {
            _root = Put(_root, key, val);
            _root.Color = BLACK;
            // assert check();
        }

        /// <summary>
        /// insert the key-value pair in the subtree rooted at h
        /// </summary>
        /// <param name="h"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Put(Node<TKey, TValue> h, TKey key, TValue val)
        {
            if (h == null) return new Node<TKey, TValue>(key, val, RED, 1);

            var cmp = key.CompareTo(h.Key);
            if (cmp < 0) h.Left = Put(h.Left, key, val);
            else if (cmp > 0) h.Right = Put(h.Right, key, val);
            else h.Value = val;

            // fix-up any right-leaning links
            if (IsRed(h.Right) && !IsRed(h.Left)) h = RotateLeft(h);
            if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);
            if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);
            h.N = Size(h.Left) + Size(h.Right) + 1;

            return h;
        }
        #endregion

        #region Red-black tree deletion.

        /// <summary>
        /// Removes the smallest key and associated value from the symbol table.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("BST underflow");

            // if both children of root are black, set root to red
            if (!IsRed(_root.Left) && !IsRed(_root.Right))
                _root.Color = RED;

            _root = DeleteMin(_root);
            if (!IsEmpty()) _root.Color = BLACK;
            // assert check();
        }

        /// <summary>
        /// delete the key-value pair with the minimum key rooted at h
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> DeleteMin(Node<TKey, TValue> h)
        {
            if (h.Left == null)
                return null;

            if (!IsRed(h.Left) && !IsRed(h.Left.Left))
                h = MoveRedLeft(h);

            h.Left = DeleteMin(h.Left);
            return Balance(h);
        }

        /// <summary>
        /// Removes the largest key and associated value from the symbol table.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public void DeleteMax()
        {
            if (IsEmpty()) throw new InvalidOperationException("BST underflow");

            // if both children of root are black, set root to red
            if (!IsRed(_root.Left) && !IsRed(_root.Right))
                _root.Color = RED;

            _root = DeleteMax(_root);
            if (!IsEmpty()) _root.Color = BLACK;
            // assert check();
        }

        /// <summary>
        /// delete the key-value pair with the maximum key rooted at h
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> DeleteMax(Node<TKey, TValue> h)
        {
            if (IsRed(h.Left))
                h = RotateRight(h);

            if (h.Right == null)
                return null;

            if (!IsRed(h.Right) && !IsRed(h.Right.Left))
                h = MoveRedRight(h);

            h.Right = DeleteMax(h.Right);

            return Balance(h);
        }


        /// <summary>
        /// Removes the key and associated value from the symbol table
        /// (if the key is in the symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            if (!Contains(key))
            {
                //System. ("symbol table does not contain " + key);
                return;
            }

            // if both children of root are black, set root to red
            if (!IsRed(_root.Left) && !IsRed(_root.Right))
                _root.Color = RED;

            _root = Delete(_root, key);
            if (!IsEmpty()) _root.Color = BLACK;
            // assert check();
        }

        /// <summary>
        /// delete the key-value pair with the given key rooted at h
        /// </summary>
        /// <param name="h"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Delete(Node<TKey, TValue> h, TKey key)
        {
            // assert get(h, key) != null;

            if (key.CompareTo(h.Key) < 0)
            {
                if (!IsRed(h.Left) && !IsRed(h.Left.Left))
                    h = MoveRedLeft(h);
                h.Left = Delete(h.Left, key);
            }
            else
            {
                if (IsRed(h.Left))
                    h = RotateRight(h);
                if (key.CompareTo(h.Key) == 0 && (h.Right == null))
                    return null;
                if (!IsRed(h.Right) && !IsRed(h.Right.Left))
                    h = MoveRedRight(h);
                if (key.CompareTo(h.Key) == 0)
                {
                    var x = Min(h.Right);
                    h.Key = x.Key;
                    h.Value = x.Value;
                    // h.val = get(h.right, Min(h.right).key);
                    // h.key = Min(h.right).key;
                    h.Right = DeleteMin(h.Right);
                }
                else h.Right = Delete(h.Right, key);
            }
            return Balance(h);
        }

        #endregion

        #region Red-black tree helper functions.
        /// <summary>
        /// make a left-leaning link lean to the right
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> h)
        {
            // assert (h != null) && isRed(h.left);
            var x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = x.Right.Color;
            x.Right.Color = RED;
            x.N = h.N;
            h.N = Size(h.Left) + Size(h.Right) + 1;
            return x;
        }

        /// <summary>
        /// make a right-leaning link lean to the left
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> h)
        {
            // assert (h != null) && isRed(h.right);
            var x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = x.Left.Color;
            x.Left.Color = RED;
            x.N = h.N;
            h.N = Size(h.Left) + Size(h.Right) + 1;
            return x;
        }

        /// <summary>
        /// flip the colors of a node and its two children
        /// </summary>
        /// <param name="h"></param>
        private void FlipColors(Node<TKey, TValue> h)
        {
            // h must have opposite color of its two children
            // assert (h != null) && (h.left != null) && (h.right != null);
            // assert (!isRed(h) &&  isRed(h.left) &&  isRed(h.right))
            //    || (isRed(h)  && !isRed(h.left) && !isRed(h.right));
            h.Color = !h.Color;
            h.Left.Color = !h.Left.Color;
            h.Right.Color = !h.Right.Color;
        }

        /// <summary>
        /// Assuming that h is red and both h.left and h.left.left
        /// are black, make h.left or one of its children red.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> MoveRedLeft(Node<TKey, TValue> h)
        {
            // assert (h != null);
            // assert isRed(h) && !isRed(h.left) && !isRed(h.left.left);

            FlipColors(h);
            if (IsRed(h.Right.Left))
            {
                h.Right = RotateRight(h.Right);
                h = RotateLeft(h);
                FlipColors(h);
            }
            return h;
        }

        /// <summary>
        /// Assuming that h is red and both h.right and h.right.left
        /// are black, make h.right or one of its children red.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> MoveRedRight(Node<TKey, TValue> h)
        {
            // assert (h != null);
            // assert isRed(h) && !isRed(h.right) && !isRed(h.right.left);
            FlipColors(h);
            if (IsRed(h.Left.Left))
            {
                h = RotateRight(h);
                FlipColors(h);
            }
            return h;
        }

        /// <summary>
        /// restore red-black tree invariant
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Balance(Node<TKey, TValue> h)
        {
            // assert (h != null);

            if (IsRed(h.Right)) h = RotateLeft(h);
            if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);
            if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);

            h.N = Size(h.Left) + Size(h.Right) + 1;
            return h;
        }
        #endregion

        #region Utility functions.
        /// <summary>
        /// Returns the height of the BST (for debugging).
        /// </summary>
        /// <returns>the height of the BST (a 1-node tree has height 0)</returns>
        public int Height()
        {
            return Height(_root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Height(Node<TKey, TValue> x)
        {
            if (x == null) return -1;
            return 1 + Math.Max(Height(x.Left), Height(x.Right));
        }
        #endregion

        #region Ordered symbol table methods.
        /// <summary>
        /// Returns the smallest key in the symbol table.
        /// </summary>
        /// <returns>the smallest key in the symbol table</returns>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public TKey Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("called Min() with empty symbol table");
            return Min(_root).Key;
        }

        /// <summary>
        /// the smallest key in subtree rooted at x; null if no such key
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Min(Node<TKey, TValue> x)
        {
            // assert x != null;
            return x.Left == null ? x : Min(x.Left);
        }

        /// <summary>
        /// Returns the largest key in the symbol table.
        /// </summary>
        /// <returns>the largest key in the symbol table</returns>
        /// <exception cref="InvalidOperationException">if the symbol table is empty</exception>
        public TKey Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("called Max() with empty symbol table");
            return Max(_root).Key;
        }

        /// <summary>
        /// the largest key in the subtree rooted at x; null if no such key
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Max(Node<TKey, TValue> x)
        {
            // assert x != null;
            return x.Right == null ? x : Max(x.Right);
        }

        /// <summary>
        /// Returns the largest key in the symbol table less than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the largest key in the symbol table less than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Floor(TKey key)
        {
            if (IsEmpty()) throw new InvalidOperationException("called floor() with empty symbol table");
            var x = Floor(_root, key);
            return x?.Key;
        }

        /// <summary>
        /// the largest key in the subtree rooted at x less than or equal to the given key
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Floor(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            var cmp = key.CompareTo(x.Key);
            if (cmp == 0) return x;
            if (cmp < 0) return Floor(x.Left, key);
            var t = Floor(x.Right, key);
            return t ?? x;
        }

        /// <summary>
        /// Returns the smallest key in the symbol table greater than or equal to <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the smallest key in the symbol table greater than or equal to <tt>key</tt></returns>
        /// <exception cref="InvalidOperationException">if there is no such key</exception>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public TKey Ceiling(TKey key)
        {
            if (IsEmpty()) throw new InvalidOperationException("called ceiling() with empty symbol table");
            var x = Ceiling(_root, key);
            return x?.Key;
        }

        /// <summary>
        /// the smallest key in the subtree rooted at x greater than or equal to the given key
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Ceiling(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            var cmp = key.CompareTo(x.Key);
            if (cmp == 0) return x;
            if (cmp > 0) return Ceiling(x.Right, key);
            var t = Ceiling(x.Left, key);
            return t ?? x;
        }


        /// <summary>
        /// Return the kth smallest key in the symbol table.
        /// </summary>
        /// <param name="k">k the order statistic</param>
        /// <returns>the kth smallest key in the symbol table</returns>
        /// <exception cref="ArgumentException">unless <tt>k</tt> is between 0 and <em>N</em> &minus; 1</exception>
        public TKey Select(int k)
        {
            if (k < 0 || k >= Size()) throw new ArgumentException();
            var x = Select(_root, k);
            return x.Key;
        }

        /// <summary>
        /// the key of Rank k in the subtree rooted at x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Select(Node<TKey, TValue> x, int k)
        {
            // assert x != null;
            // assert k >= 0 && k < size(x);
            var t = Size(x.Left);
            if (t > k) return Select(x.Left, k);
            if (t < k) return Select(x.Right, k - t - 1);
            return x;
        }

        /// <summary>
        /// Return the number of keys in the symbol table strictly less than <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the number of keys in the symbol table strictly less than <tt>key</tt></returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public int Rank(TKey key)
        {
            return Rank(key, _root);
        }

        /// <summary>
        /// number of Keys less than key in the subtree rooted at x
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Rank(TKey key, Node<TKey, TValue> x)
        {
            if (x == null) return 0;
            var cmp = key.CompareTo(x.Key);
            if (cmp < 0) return Rank(key, x.Left);
            if (cmp > 0) return 1 + Size(x.Left) + Rank(key, x.Right);
            return Size(x.Left);
        }
        #endregion

        #region Range count and range search.
        /// <summary>
        /// Returns all keys in the symbol table as an <tt>Iterable</tt>.
        /// To iterate over all of the keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation.
        /// </summary>
        /// <returns>all keys in the sybol table as an <tt>Iterable</tt></returns>
        public IEnumerable<TKey> Keys()
        {
            return Keys(Min(), Max());
        }

        /// <summary>
        /// Returns all keys in the symbol table in the given range,
        /// as an <tt>Iterable</tt>.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>all keys in the sybol table between <tt>lo</tt> (inclusive) and <tt>hi</tt> (exclusive) as an <tt>Iterable</tt></returns>
        /// <exception cref="NullReferenceException">if either <tt>lo</tt> or <tt>hi</tt> is <tt>null</tt></exception>
        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            var queue = new Collections.Queue<TKey>();
            // if (isEmpty() || lo.compareTo(hi) > 0) return queue;
            Keys(_root, queue, lo, hi);
            return queue;
        }

        /// <summary>
        /// add the Keys between lo and hi in the subtree rooted at x
        /// to the queue
        /// </summary>
        /// <param name="x"></param>
        /// <param name="queue"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private void Keys(Node<TKey, TValue> x, Collections.Queue<TKey> queue, TKey lo, TKey hi)
        {
            if (x == null) return;
            var cmplo = lo.CompareTo(x.Key);
            var cmphi = hi.CompareTo(x.Key);
            if (cmplo < 0) Keys(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0) queue.Enqueue(x.Key);
            if (cmphi > 0) Keys(x.Right, queue, lo, hi);
        }

        /// <summary>
        /// Returns the number of keys in the symbol table in the given range.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>the number of keys in the sybol table between <tt>lo</tt> (inclusive) and <tt>hi</tt> (exclusive)</returns>
        /// <exception cref="NullReferenceException">if either <tt>lo</tt> or <tt>hi</tt> is <tt>null</tt></exception>
        public int Size(TKey lo, TKey hi)
        {
            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            return Rank(hi) - Rank(lo);
        }
        #endregion
        #region Check integrity of red-black tree data structure.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            if (!IsBST()) Console.WriteLine("Not in symmetric order");
            if (!IsSizeConsistent()) Console.WriteLine("Subtree counts not consistent");
            if (!IsRankConsistent()) Console.WriteLine("Ranks not consistent");
            if (!Is23()) Console.WriteLine("Not a 2-3 tree");
            if (!IsBalanced()) Console.WriteLine("Not balanced");
            return IsBST() && IsSizeConsistent() && IsRankConsistent() && Is23() && IsBalanced();
        }

        /// <summary>
        /// does this binary tree satisfy symmetric order?
        /// Note: this test also ensures that data structure is a binary tree since order is strict
        /// </summary>
        /// <returns></returns>
        private bool IsBST()
        {
            return IsBST(_root, null, null);
        }

        /// <summary>
        /// is the tree rooted at x a BST with all Keys strictly between Min and Max
        /// (if Min or Max is null, treat as empty constraint)
        /// Credit: Bob Dondero's elegant solution
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private bool IsBST(Node<TKey, TValue> x, TKey min, TKey max)
        {
            if (x == null) return true;
            if (min != null && x.Key.CompareTo(min) <= 0) return false;
            if (max != null && x.Key.CompareTo(max) >= 0) return false;
            return IsBST(x.Left, min, x.Key) && IsBST(x.Right, x.Key, max);
        }

        /// <summary>
        /// are the size fields correct?
        /// </summary>
        /// <returns></returns>
        private bool IsSizeConsistent() { return IsSizeConsistent(_root); }
        private bool IsSizeConsistent(Node<TKey, TValue> x)
        {
            if (x == null) return true;
            if (x.N != Size(x.Left) + Size(x.Right) + 1) return false;
            return IsSizeConsistent(x.Left) && IsSizeConsistent(x.Right);
        }

        /// <summary>
        /// check that ranks are consistent
        /// </summary>
        /// <returns></returns>
        private bool IsRankConsistent()
        {
            for (var i = 0; i < Size(); i++)
                if (i != Rank(Select(i))) return false;
            foreach (var key in Keys())
                if (key.CompareTo(Select(Rank(key))) != 0) return false;
            return true;
        }

        /// <summary>
        /// Does the tree have no red right links, and at most one (left)
        /// red links in a row on any path?
        /// </summary>
        /// <returns></returns>
        private bool Is23() { return Is23(_root); }
        private bool Is23(Node<TKey, TValue> x)
        {
            if (x == null) return true;
            if (IsRed(x.Right)) return false;
            if (x != _root && IsRed(x) && IsRed(x.Left))
                return false;
            return Is23(x.Left) && Is23(x.Right);
        }

        /// <summary>
        /// do all paths from root to leaf have same number of black edges?
        /// </summary>
        /// <returns></returns>
        private bool IsBalanced()
        {
            var black = 0;     // number of black links on path from root to Min
            var x = _root;
            while (x != null)
            {
                if (!IsRed(x)) black++;
                x = x.Left;
            }
            return IsBalanced(_root, black);
        }

        /// <summary>
        /// does every path from the root to a leaf have the given number of black links?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="black"></param>
        /// <returns></returns>
        private bool IsBalanced(Node<TKey, TValue> x, int black)
        {
            if (x == null) return black == 0;
            if (!IsRed(x)) black--;
            return IsBalanced(x.Left, black) && IsBalanced(x.Right, black);
        }
        #endregion

    }
}
