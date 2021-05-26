using NUnit.Framework;

namespace MatrixSortApp.Test
{
    [TestFixture]
    public class MatrixTest
    {
        [Test]
        public void Sort_NullMatrix_ArgumentNullException()
        {
            //Average
            int[,] matrix = null;

            //Act & Assert
            Assert.That(() => Matrix.Sort(matrix, Matrix.Operation.Asc,
                SortMethods.MinValueInRow), Throws.ArgumentNullException);
        }

        [Test]
        public void Sort_Matrix_SortedMatrix()
        {
            //Average
            var matrix = new[,] {
                {7, 2, 6},
                {10, 7, 6},
                {8, 2, -3}
            };

            var expected = new[,] {
                {8, 2, -3},
                {7, 2, 6},
                {10, 7, 6}
            };

            //Act
            Matrix.Sort(matrix, Matrix.Operation.Asc,
                SortMethods.MinValueInRow);

            //Assert
            Assert.That(matrix,Is.EquivalentTo(expected));
        }
    }
}
