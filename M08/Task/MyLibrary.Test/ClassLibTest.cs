using NUnit.Framework;

namespace MyLibrary.Test
{
    [TestFixture]
    public class ClassLibTest
    {
        [TestCase(7, ExpectedResult = 3)]
        [TestCase(12, ExpectedResult = 4)]
        public int BinarySearch_IntArray_Index(int search)
        {
            //Average
            var array = new[]
            {
                2, 5, 6, 7, 12
            };

            //Act & Assert
            return ClassLib.BinarySearch(array, search);
        }

        [Test]
        public void Fibonacci_Int_IEnumerableFibonacci()
        {
            //Average
            var countNums = 4;
            var expected = new[]
            {
                1, 1, 2, 3
            };

            //Act & Assert
            Assert.That(() => ClassLib.Fibonacci(countNums), Is.EquivalentTo(expected));
        }
    }
}
