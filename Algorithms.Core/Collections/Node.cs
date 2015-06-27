namespace Algorithms.Core.Collections
{
    public class Node<T> where T : class 
    {
        public T Item;
        public Node<T> Next;
    }
}
