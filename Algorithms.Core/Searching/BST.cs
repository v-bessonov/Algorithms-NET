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
    /// It also provides a <em>Keys</em> method for iterating over all of the Keys.
    /// A symbol table implements the <em>associative array</em> abstraction:
    /// when associating a value with a key that is already in the symbol table,
    /// the convention is to replace the old value with the new value.
    /// Unlike {@link java.util.Map}, this class uses the convention that
    /// values cannot be <tt>null</tt>&mdash;setting the
    /// value associated with a key to <tt>null</tt> is equivalent to deleting the key
    /// from the symbol table.
    /// <p>
    /// This implementation uses an (unbalanced) binary search tree. It requires that
    /// the key type implements the <tt>Comparable</tt> interface and calls the
    /// <tt>compareTo()</tt> and method to compare two Keys. It does not call either
    /// <tt>equals()</tt> or <tt>hashCode()</tt>.
    /// The <em>put</em>, <em>contains</em>, <em>remove</em>, <em>minimum</em>,
    /// <em>maximum</em>, <em>ceiling</em>, and <em>floor</em> operations each take
    /// linear time in the worst case, if the tree becomes unbalanced.
    /// The <em>size</em>, and <em>is-empty</em> operations take constant time.
    /// Construction takes constant time.
    /// </p>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class BST<TKey, TValue> where TKey : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {

        private Node<TKey, TValue> _root;             // root of BST

        /// <summary>
        /// Is this symbol table empty?
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Size() == 0;
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
        /// return number of key-value pairs in BST rooted at x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Size(Node<TKey, TValue> x)
        {
            return x?.N ?? 0;
        }

        /// <summary>
        /// Does this symbol table contain the given key?
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns><tt>true</tt> if this symbol table contains <tt>key</tt> and <tt>false</tt> otherwise</returns>
        /// throws NullPointerException if <tt>key</tt> is <tt>null</tt>
        public bool Contains(TKey key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// Returns the value associated with the given key.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the value associated with the given key if the key is in the symbol table and <tt>null</tt> if the key is not in the symbol table</returns>
        /// throws NullPointerException if <tt>key</tt> is <tt>null</tt>
        public TValue Get(TKey key)
        {
            return Get(_root, key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        private static TValue Get(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            var cmp = key.CompareTo(x.Key);
            if (cmp < 0) return Get(x.Left, key);
            if (cmp > 0) return Get(x.Right, key);
            return x.Value;
        }


        /// <summary>
        /// Inserts the key-value pair into the symbol table, overwriting the old value
        /// with the new value if the key is already in the symbol table.
        /// If the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <param name="val">val the value</param>
        /// throws NullPointerException if <tt>key</tt> is <tt>null</tt>
        public void Put(TKey key, TValue val)
        {
            if (val == null)
            {
                Delete(key);
                return;
            }
            _root = Put(_root, key, val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>

        private Node<TKey, TValue> Put(Node<TKey, TValue> x, TKey key, TValue val)
        {
            if (x == null) return new Node<TKey, TValue>(key, val, 1);
            var cmp = key.CompareTo(x.Key);
            if (cmp < 0) x.Left = Put(x.Left, key, val);
            else if (cmp > 0) x.Right = Put(x.Right, key, val);
            else x.Value = val;
            x.N = 1 + Size(x.Left) + Size(x.Right);
            return x;
        }


        /// <summary>
        /// Removes the smallest key and associated value from the symbol table.
        /// throws NoSuchElementException if the symbol table is empty
        /// </summary>
        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow");
            _root = DeleteMin(_root);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>

        private Node<TKey, TValue> DeleteMin(Node<TKey, TValue> x)
        {
            if (x.Left == null) return x.Right;
            x.Left = DeleteMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        /// <summary>
        /// Removes the largest key and associated value from the symbol table.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">if the symbol table is empty</exception>
        public void DeleteMax()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow");
            _root = DeleteMax(_root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Node<TKey, TValue> DeleteMax(Node<TKey, TValue> x)
        {
            if (x.Right == null) return x.Left;
            x.Right = DeleteMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        /// <summary>
        /// Removes the key and associated value from the symbol table
        /// (if the key is in the symbol table).
        /// </summary>
        /// <param name="key">key the key</param>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public void Delete(TKey key)
        {
            _root = Delete(_root, key);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Delete(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            var cmp = key.CompareTo(x.Key);
            if (cmp < 0) x.Left = Delete(x.Left, key);
            else if (cmp > 0) x.Right = Delete(x.Right, key);
            else
            {
                if (x.Right == null) return x.Left;
                if (x.Left == null) return x.Right;
                var t = x;
                x = Min(t.Right);
                x.Right = DeleteMin(t.Right);
                x.Left = t.Left;
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Min(Node<TKey, TValue> x)
        {
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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Max(Node<TKey, TValue> x)
        {
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Ceiling(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            var cmp = key.CompareTo(x.Key);
            if (cmp == 0) return x;
            if (cmp < 0)
            {
                var t = Ceiling(x.Left, key);
                return t ?? x;
            }
            return Ceiling(x.Right, key);
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
        /// Return key of Rank k. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Select(Node<TKey, TValue> x, int k)
        {
            if (x == null) return null;
            var t = Size(x.Left);
            if (t > k) return Select(x.Left, k);
            if (t < k) return Select(x.Right, k - t - 1);
            return x;
        }

        /// <summary>
        /// Return the number of Keys in the symbol table strictly less than <tt>key</tt>.
        /// </summary>
        /// <param name="key">key the key</param>
        /// <returns>the number of Keys in the symbol table strictly less than <tt>key</tt></returns>
        /// <exception cref="NullReferenceException">if <tt>key</tt> is <tt>null</tt></exception>
        public int Rank(TKey key)
        {
            return Rank(key, _root);
        }

        /// <summary>
        /// Number of Keys in the subtree less than key.
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

        /// <summary>
        /// Returns all Keys in the symbol table as an <tt>Iterable</tt>.
        /// To iterate over all of the Keys in the symbol table named <tt>st</tt>,
        /// use the foreach notation
        /// </summary>
        /// <returns>all Keys in the sybol table as an <tt>Iterable</tt></returns>
        public IEnumerable<TKey> Keys()
        {
            return Keys(Min(), Max());
        }


        /// <summary>
        /// Returns all Keys in the symbol table in the given range,
        /// as an <tt>Iterable</tt>.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>all Keys in the sybol table between <tt>lo</tt> (inclusive) and <tt>hi</tt> (exclusive) as an <tt>Iterable</tt></returns>
        /// <exception cref="NullReferenceException">if either <tt>lo</tt> or <tt>hi</tt> is <tt>null</tt></exception>
        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            var queue = new Collections.Queue<TKey>();
            Keys(_root, queue, lo, hi);
            return queue;
        }

        /// <summary>
        /// 
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
        /// Returns the number of Keys in the symbol table in the given range.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>the number of Keys in the sybol table between <tt>lo</tt> (inclusive) and <tt>hi</tt> (exclusive)</returns>
        /// <exception cref="NullReferenceException">if either <tt>lo</tt> or <tt>hi</tt> is <tt>null</tt></exception>
        public int Size(TKey lo, TKey hi)
        {
            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            return Rank(hi) - Rank(lo);
        }

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

        /// <summary>
        /// Returns the Keys in the BST in level order (for debugging).
        /// </summary>
        /// <returns>the Keys in the BST in level order traversal, as an Iterable</returns>
        public IEnumerable<TKey> LevelOrder()
        {
            var keys = new Collections.Queue<TKey>();
            var queue = new Collections.Queue<Node<TKey, TValue>>();
            queue.Enqueue(_root);
            while (!queue.IsEmpty())
            {
                var x = queue.Dequeue();
                if (x == null) continue;
                keys.Enqueue(x.Key);
                queue.Enqueue(x.Left);
                queue.Enqueue(x.Right);
            }
            return keys;
        }


        #region Check integrity of BST data structure.

        private bool Check()
        {
            if (!IsBST()) Console.WriteLine("Not in symmetric order");
            if (!IsSizeConsistent()) Console.WriteLine("Subtree counts not consistent");
            if (!IsRankConsistent()) Console.WriteLine("Ranks not consistent");
            return IsBST() && IsSizeConsistent() && IsRankConsistent();
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
        ///  Credit: Bob Dondero's elegant solution
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
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
        #endregion
    }
}
