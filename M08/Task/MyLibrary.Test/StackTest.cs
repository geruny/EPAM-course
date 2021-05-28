using System;
using NUnit.Framework;

namespace MyLibrary.Test
{
    [TestFixture]
    public class StackTest
    {
        public Stack<object> Stack = new Stack<object>();

        [Test]
        public void Push_NullData_ArgumentNullException()
        {
            //Act & Assert
            Assert.That(() => Stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Pop_InEmptyStack_IndexOutOfRangeException()
        {
            //Act & Assert
            Assert.That(() => Stack.Pop(), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Peek_InEmptyStack_IndexOutOfRangeException()
        {
            //Act & Assert
            Assert.That(() => Stack.Peek(), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        }
    }
}
