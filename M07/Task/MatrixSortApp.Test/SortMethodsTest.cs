using NUnit.Framework;

namespace MatrixSortApp.Test
{
    [TestFixture]
    public class SortMethodsTest
    {
        public int[,] Matrix = {
            {7, 2, 6},
            {10, 7, 6},
            {8, 2, -3}
        };

        [Test]
        public void SumRowValues_Matrix_ArraySumOfRows()
        {
            //Average
            var expected = new[]
            {
                15, 23, 7
            };

            //Act & assert
            Assert.That(()=> SortMethods.SumRowValues(Matrix),Is.EquivalentTo(expected));
        }

        [Test]
        public void MaxValueInRow_Matrix_ArrayMaxValuesOfRows()
        {
            //Average
            var expected = new[]
            {
                7, 10, 8
            };

            //Act & assert
            Assert.That(() => SortMethods.MaxValueInRow(Matrix), Is.EquivalentTo(expected));
        }

        [Test]
        public void MinValueInRow_Matrix_ArrayMinValuesOfRows()
        {
            //Average
            var expected = new[]
            {
                2, 6, -3
            };

            //Act & assert
            Assert.That(() => SortMethods.MinValueInRow(Matrix), Is.EquivalentTo(expected));
        }
    }
}
