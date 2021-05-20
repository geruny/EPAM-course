using System;
using System.Collections;

namespace MyLibrary
{
    public class Stack<T>
    {
        private Node<T> _top;
        public int Length { get; private set; } = 0;

        public void Push(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var node = new Node<T>(data) { Next = _top };
            _top = node;
            ++Length;
        }

        public T Pop()
        {
            CheckLength();

            var node = _top;
            _top = _top.Next;
            --Length;

            return node.Data;
        }

        public T Peek()
        {
            CheckLength();
            return _top.Data;
        }

        internal void CheckLength()
        {
            if (Length == 0)
                throw new IndexOutOfRangeException("Stack is empty");
        }

        public IEnumerator GetEnumerator()
        {
            return new StackIterator<T>(_top);
        }
    }
}
