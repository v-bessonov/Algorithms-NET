using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Core.Searching
{
    public class BST<TKey, TValue> where TKey  : class, IComparable<TKey> where TValue : class, IComparable<TValue>
    {

        private Node<TKey, TValue> _root;             // root of BST
        
        /**
         * Is this symbol table empty?
         * @return <tt>true</tt> if this symbol table is empty and <tt>false</tt> otherwise
         */
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /**
         * Returns the number of key-value pairs in this symbol table.
         * @return the number of key-value pairs in this symbol table
         */
        public int Size()
        {
            return Size(_root);
        }

        // return number of key-value pairs in BST rooted at x
        private int Size(Node<TKey, TValue> x)
        {
            return x == null ? 0 : x.N;
        }

        /**
     * Does this symbol table contain the given key?
     * @param key the key
     * @return <tt>true</tt> if this symbol table contains <tt>key</tt> and
     *     <tt>false</tt> otherwise
     * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
     */
        public bool Contains(TKey key)
        {
            return Get(key) != null;
        }

        /**
         * Returns the value associated with the given key.
         * @param key the key
         * @return the value associated with the given key if the key is in the symbol table
         *     and <tt>null</tt> if the key is not in the symbol table
         * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
         */
        public TValue Get(TKey key)
        {
            return Get(_root, key);
        }

        private static TValue Get(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            var cmp = key.CompareTo(x.Key);
            if (cmp < 0) return Get(x.Left, key);
            if (cmp > 0) return Get(x.Right, key);
            return x.Value;
        }


        /**
    * Inserts the key-value pair into the symbol table, overwriting the old value
    * with the new value if the key is already in the symbol table.
    * If the value is <tt>null</tt>, this effectively deletes the key from the symbol table.
    * @param key the key
    * @param val the value
    * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
    */
        public void Put(TKey key, TValue val)
        {
            if (val == null)
            {
                Delete(key);
                return;
            }
            _root = Put(_root, key, val);
        }

        private Node<TKey, TValue> Put(Node<TKey, TValue> x, TKey key, TValue val)
        {
            if (x == null) return new Node<TKey, TValue>(key, val, 1);
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0) x.Left = Put(x.Left, key, val);
            else if (cmp > 0) x.Right = Put(x.Right, key, val);
            else x.Value = val;
            x.N = 1 + Size(x.Left) + Size(x.Right);
            return x;
        }


        /**
    * Removes the smallest key and associated value from the symbol table.
    * @throws NoSuchElementException if the symbol table is empty
    */
        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow");
            _root = DeleteMin(_root);
        }

        private Node<TKey, TValue> DeleteMin(Node<TKey, TValue> x)
        {
            if (x.Left == null) return x.Right;
            x.Left = DeleteMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        /**
         * Removes the largest key and associated value from the symbol table.
         * @throws NoSuchElementException if the symbol table is empty
         */
        public void DeleteMax()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow");
            _root = DeleteMax(_root);
        }

        private Node<TKey, TValue> DeleteMax(Node<TKey, TValue> x)
        {
            if (x.Right == null) return x.Left;
            x.Right = DeleteMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        /**
         * Removes the key and associated value from the symbol table
         * (if the key is in the symbol table).
         * @param key the key
         * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
         */
        public void Delete(TKey key)
        {
            _root = Delete(_root, key);
        }

        private Node<TKey, TValue> Delete(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0) x.Left = Delete(x.Left, key);
            else if (cmp > 0) x.Right = Delete(x.Right, key);
            else
            {
                if (x.Right == null) return x.Left;
                if (x.Left == null) return x.Right;
                Node<TKey, TValue> t = x;
                x = Min(t.Right);
                x.Right = DeleteMin(t.Right);
                x.Left = t.Left;
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        /**
     * Returns the smallest key in the symbol table.
     * @return the smallest key in the symbol table
     * @throws NoSuchElementException if the symbol table is empty
     */
        public TKey Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("called min() with empty symbol table");
            return Min(_root).Key;
        }

        private Node<TKey, TValue> Min(Node<TKey, TValue> x)
        {
            if (x.Left == null) return x;
            return Min(x.Left);
        }

        /**
    * Returns the largest key in the symbol table.
    * @return the largest key in the symbol table
    * @throws NoSuchElementException if the symbol table is empty
    */
        public TKey Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("called max() with empty symbol table");
            return Max(_root).Key;
        }

        private Node<TKey, TValue> Max(Node<TKey, TValue> x)
        {
            if (x.Right == null) return x;
            return Max(x.Right);
        }


        /**
    * Returns the largest key in the symbol table less than or equal to <tt>key</tt>.
    * @param key the key
    * @return the largest key in the symbol table less than or equal to <tt>key</tt>
    * @throws NoSuchElementException if there is no such key
    * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
    */
        public TKey floor(TKey key)
        {
            if (IsEmpty()) throw new InvalidOperationException("called floor() with empty symbol table");
            Node<TKey, TValue> x = floor(_root, key);
            if (x == null) return null;
            else return x.Key;
        }

        private Node<TKey, TValue> floor(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0) return x;
            if (cmp < 0) return floor(x.Left, key);
            Node<TKey, TValue> t = floor(x.Right, key);
            if (t != null) return t;
            else return x;
        }

        /**
     * Returns the smallest key in the symbol table greater than or equal to <tt>key</tt>.
     * @param key the key
     * @return the smallest key in the symbol table greater than or equal to <tt>key</tt>
     * @throws NoSuchElementException if there is no such key
     * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
     */
        public TKey ceiling(TKey key)
        {
            if (IsEmpty()) throw new InvalidOperationException("called ceiling() with empty symbol table");
            Node<TKey, TValue> x = ceiling(_root, key);
            if (x == null) return null;
            else return x.Key;
        }

        private Node<TKey, TValue> ceiling(Node<TKey, TValue> x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0) return x;
            if (cmp < 0)
            {
                Node<TKey, TValue> t = ceiling(x.Left, key);
                if (t != null) return t;
                else return x;
            }
            return ceiling(x.Right, key);
        }

        /**
    * Return the kth smallest key in the symbol table.
    * @param k the order statistic
    * @return the kth smallest key in the symbol table
    * @throws IllegalArgumentException unless <tt>k</tt> is between 0 and
    *     <em>N</em> &minus; 1
    */
        public TKey select(int k)
        {
            if (k < 0 || k >= Size()) throw new ArgumentException();
            Node<TKey, TValue> x = select(_root, k);
            return x.Key;
        }

        // Return key of rank k. 
        private Node<TKey, TValue> select(Node<TKey, TValue> x, int k)
        {
            if (x == null) return null;
            int t = Size(x.Left);
            if (t > k) return select(x.Left, k);
            else if (t < k) return select(x.Right, k - t - 1);
            else return x;
        }

        /**
    * Return the number of keys in the symbol table strictly less than <tt>key</tt>.
    * @param key the key
    * @return the number of keys in the symbol table strictly less than <tt>key</tt>
    * @throws NullPointerException if <tt>key</tt> is <tt>null</tt>
    */
        public int rank(TKey key)
        {
            return rank(key, _root);
        }

        // Number of keys in the subtree less than key.
        private int rank(TKey key, Node<TKey, TValue> x)
        {
            if (x == null) return 0;
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0) return rank(key, x.Left);
            else if (cmp > 0) return 1 + Size(x.Left) + rank(key, x.Right);
            else return Size(x.Left);
        }

        /**
     * Returns all keys in the symbol table as an <tt>Iterable</tt>.
     * To iterate over all of the keys in the symbol table named <tt>st</tt>,
     * use the foreach notation: <tt>for (Key key : st.keys())</tt>.
     * @return all keys in the sybol table as an <tt>Iterable</tt>
     */
        public IEnumerable<TKey> keys()
        {
            return keys(Min(), Max());
        }


        /**
    * Returns all keys in the symbol table in the given range,
    * as an <tt>Iterable</tt>.
    * @return all keys in the sybol table between <tt>lo</tt> 
    *    (inclusive) and <tt>hi</tt> (exclusive) as an <tt>Iterable</tt>
    * @throws NullPointerException if either <tt>lo</tt> or <tt>hi</tt>
    *    is <tt>null</tt>
    */
        public IEnumerable<TKey> keys(TKey lo, TKey hi)
        {
            Algorithms.Core.Collections.Queue<TKey> queue = new Algorithms.Core.Collections.Queue<TKey>();
            keys(_root, queue, lo, hi);
            return queue;
        }

        private void keys(Node<TKey, TValue> x, Algorithms.Core.Collections.Queue<TKey> queue, TKey lo, TKey hi)
        {
            if (x == null) return;
            int cmplo = lo.CompareTo(x.Key);
            int cmphi = hi.CompareTo(x.Key);
            if (cmplo < 0) keys(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0) queue.Enqueue(x.Key);
            if (cmphi > 0) keys(x.Right, queue, lo, hi);
        }

        /**
    * Returns the number of keys in the symbol table in the given range.
    * @return the number of keys in the sybol table between <tt>lo</tt> 
    *    (inclusive) and <tt>hi</tt> (exclusive)
    * @throws NullPointerException if either <tt>lo</tt> or <tt>hi</tt>
    *    is <tt>null</tt>
    */
        public int size(TKey lo, TKey hi)
        {
            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return rank(hi) - rank(lo) + 1;
            else return rank(hi) - rank(lo);
        }

        /**
         * Returns the height of the BST (for debugging).
         * @return the height of the BST (a 1-node tree has height 0)
         */
        public int height()
        {
            return height(_root);
        }
        private int height(Node<TKey, TValue> x)
        {
            if (x == null) return -1;
            return 1 + Math.Max(height(x.Left), height(x.Right));
        }

        /**
     * Returns the keys in the BST in level order (for debugging).
     * @return the keys in the BST in level order traversal,
     *    as an Iterable
     */
        public IEnumerable<TKey> levelOrder()
        {
            Algorithms.Core.Collections.Queue<TKey> keys = new Algorithms.Core.Collections.Queue<TKey>();
            Algorithms.Core.Collections.Queue<Node<TKey, TValue>> queue = new Algorithms.Core.Collections.Queue<Node<TKey, TValue>>();
            queue.Enqueue(_root);
            while (!queue.IsEmpty())
            {
                Node<TKey, TValue> x = queue.Dequeue();
                if (x == null) continue;
                keys.Enqueue(x.Key);
                queue.Enqueue(x.Left);
                queue.Enqueue(x.Right);
            }
            return keys;
        }


        /*************************************************************************
    *  Check integrity of BST data structure.
    ***************************************************************************/
        private bool check()
        {
            if (!isBST()) Console.WriteLine("Not in symmetric order");
            if (!isSizeConsistent()) Console.WriteLine("Subtree counts not consistent");
            if (!isRankConsistent()) Console.WriteLine("Ranks not consistent");
            return isBST() && isSizeConsistent() && isRankConsistent();
        }

        // does this binary tree satisfy symmetric order?
        // Note: this test also ensures that data structure is a binary tree since order is strict
        private bool isBST()
        {
            return isBST(_root, null, null);
        }

        // is the tree rooted at x a BST with all keys strictly between min and max
        // (if min or max is null, treat as empty constraint)
        // Credit: Bob Dondero's elegant solution
        private bool isBST(Node<TKey, TValue> x, TKey min, TKey max)
        {
            if (x == null) return true;
            if (min != null && x.Key.CompareTo(min) <= 0) return false;
            if (max != null && x.Key.CompareTo(max) >= 0) return false;
            return isBST(x.Left, min, x.Key) && isBST(x.Right, x.Key, max);
        }

        // are the size fields correct?
        private bool isSizeConsistent() { return isSizeConsistent(_root); }
        private bool isSizeConsistent(Node<TKey, TValue> x)
        {
            if (x == null) return true;
            if (x.N != Size(x.Left) + Size(x.Right) + 1) return false;
            return isSizeConsistent(x.Left) && isSizeConsistent(x.Right);
        }

        // check that ranks are consistent
        private bool isRankConsistent()
        {
            for (int i = 0; i < Size(); i++)
                if (i != rank(select(i))) return false;
            foreach (TKey key in keys())
                if (key.CompareTo(select(rank(key))) != 0) return false;
            return true;
        }
    }
}
