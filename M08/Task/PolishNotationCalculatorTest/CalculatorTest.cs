using NUnit.Framework;
using PolishNotationCalculator;

namespace PolishNotationCalculatorTest
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void Calculate_StringPolishNotation_CalcResult()
        {
            //Average
            var strPolishNotation = "5 1 2 + 4 * + 3 -";
            var expected = 14;

            //Act & Assert
            Assert.That(()=>Calculator.Calculate(strPolishNotation),Is.EqualTo(expected));
        }
    }
}
