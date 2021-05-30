using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MyLib.Tests
{
    [TestFixture]
    public class ConverterTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger<Converter>>();
            var converter = new Converter(logger.Object);
        }

        [Test]
        public void StringToInt_NullOrWhiteSpace_ArgumentException([Values("", " ")] string str)
        {
            //Act & assert
            Assert.That(() => Converter.StringToInt(str),
                    Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void StringToInt_StringWithLetters_ArgumentException()
        {
            //Arrange
            var str = "123fgdr";

            //Act & assert
            Assert.That(() => Converter.StringToInt(str),
                Throws.Exception.TypeOf<ArgumentException>());
        }

        [TestCase("2147483648")]
        [TestCase("-2147483649")]
        public void StringToInt_StringWithOutOfIntRange_ArgumentOutOfRangeException(string str)
        {
            //Act & assert
            Assert.That(() => Converter.StringToInt(str),
                Throws.Exception.TypeOf<OverflowException>());
        }

        [TestCase("123", 123)]
        [TestCase("-123", -123)]
        public void StringToInt_StringWithDigits_Int(string str, int expected)
        {
            //Act
            var result = Converter.StringToInt(str);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
