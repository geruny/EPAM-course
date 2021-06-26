using System;
using NUnit.Framework;

namespace ConsoleApp.Test
{
    [TestFixture]
    internal class StringerTest
    {
        private readonly char[] _delimiterChars = {' ', ',', '.', ':', '\t', '\n', '(', ')', '/', '@', '#', '!'};

        [Test]
        public void GetWordsFromString_NullEmptyWhiteSpaceString_ArgumentException([Values(null, "", " ")] string str)
        {
            //Act & assert
            Assert.That(() => Stringer.GetWordsFromString(str, _delimiterChars),
                Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetWordsFromString_GivenStringWithoutWords_ArgumentException()
        {
            //Arrange
            var str = " / . @#";

            //Act & assert
            Assert.That(() => Stringer.GetWordsFromString(str, _delimiterChars),
                Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetWordsFromString_GivenString_WordsArr()
        {
            //Arrange
            var str = "Create a class@ that implements a method!";
            var expected = new[] {"Create", "a", "class", "that", "implements", "a", "method"};

            //Act
            var result = Stringer.GetWordsFromString(str, _delimiterChars);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetAverageWordLength_GivenString_AverageWordsLength()
        {
            //Arrange
            var str = "Create a class that implements";
            var expected = 5.2;

            //Act
            var result = Stringer.GetAverageWordLength(str);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DoubleDefChars_NullOrWhiteSpaceStrings_ArgumentException([Values(null, "", " ")] string str, [Values(null, "", " ")] string defChars)
        {
            //Act & assert
            Assert.That(() => Stringer.DoubleDefChars(str, defChars),
                Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void DoubleDefChars_String_StringDoubleDefChars()
        {
            //Arrange
            var str = "omg i love shrek";
            var defChars = "o kek";
            var expected = "oomg i loovee shreekk";

            //Act
            var result = Stringer.DoubleDefChars(str, defChars);

            //Assert
            Assert.That(result,Is.EqualTo(expected));
        }

        [Test]
        public void GetSumOfNums_NullOrWhiteSpaceStrings_ArgumentException([Values(null, "", " ")] string str1, [Values(null, "", " ")] string str2)
        {
            //Act & assert
            Assert.That(() => Stringer.GetSumOfNums(str1, str2),
                Throws.Exception.TypeOf<ArgumentException>());
        }

        [TestCase( "123","123","246")]
        public void GetSumOfNums_Strings_StringWithSumNums(string str1,string str2,string expected)
        {
            //Act
            var result = Stringer.GetSumOfNums(str1, str2);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReverseString_String_ReverseString()
        {
            //Arrange
            var str = "The greatest victory is that which requires no battle";
            var expected = "battle no requires which that is victory greatest The";

            //Act
            var result = Stringer.ReverseString(str);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
