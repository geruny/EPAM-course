using System.Collections;

namespace MyLibrary
{
    internal class StackIterator<T> : IEnumerator
    {
        private readonly Node<T> _top;
        private Node<T> _node;

        public StackIterator(Node<T> top)
        {
            _top = new Node<T>(top.Data) { Next = top };
            _node = _top;
        }

        public bool MoveNext()
        {
            if (_node.Next == null) return false;

            _node = _node.Next;
            return true;
        }

        public void Reset() => _node = _top;

        public object Current => _node.Data;
    }
}
