using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Test
{
    [TestFixture]
    public class SearchHandlerTest
    {
        public static List<string> Flags = new()
        {
            "-name",
            "-minmark",
            "-maxmark",
            "-datefrom",
            "-dateto",
            "-test"
        };
        public static string Input =
            "-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc";
        public static List<string> Values = new()
        {
            "Ivan",
            "3",
            "5",
            "20.11.2019",
            "20.12.2021",
            "Maths"
        };

        [TestCase("")]
        [TestCase("    ")]
        public void SearchHandler_EmptyOrWhiteSpaceString_ArgumentException(string input)
        {
            //Arrange
            SearchHandler searchHandler;

            //Act & Assert
            Assert.That(() => searchHandler = new SearchHandler(input),
                Throws.ArgumentException);
        }

        [Test, Sequential]
        public void GetInputValues_Flag_ExpectedValue([ValueSource(nameof(Flags))] string flag, [ValueSource(nameof(Values))] string expectedValue)
        {
            //Arrange
            var searchHandler = new SearchHandler(Input);

            //Act
            var value = searchHandler.GetInputValues(flag);

            //Assert
            Assert.That(value, Is.EqualTo(expectedValue));
        }

        [TestCase("-name -name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        public void GetInputValues_BadCountFlag_ArgumentException(string input)
        {
            //Arrange
            var searchHandler = new SearchHandler(input);

            //Act & Assert
            Assert.That(() => searchHandler.GetInputValues("-name"), Throws.ArgumentException);
        }

        [TestCase("-name -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        public void GetInputValues_FlagWithoutValue_ArgumentException(string input)
        {
            //Arrange
            var searchHandler = new SearchHandler(input);

            //Act & Assert
            Assert.That(() => searchHandler.GetInputValues("-name"), Throws.ArgumentException);
        }

        [Test]
        public void GetSortMethod_SortFlag_SortMethod()
        {
            //Arrange
            var searchHandler = new SearchHandler(Input);

            //Act
            var value = searchHandler.GetSortMethod("-sort");
            var expectedValue = "mark";

            //Assert
            Assert.That(value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void GetSortMethod_sortValue_ArgumentException()
        {
            //Arrange
            var inputWithoutSortMethod =
                "-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark  ";
            var searchHandler = new SearchHandler(inputWithoutSortMethod);

            //Act & Assert
            Assert.That(() => searchHandler.GetSortMethod("mark"), Throws.ArgumentException);
        }

        [Test]
        public void SearchQuery_Query_SearchedStudents()
        {
            //Arrange
            var searchHandler = new SearchHandler(Input);
            var jsonStudents = new JsonStudents("TestResults.json");
            var expected = new[]
            {
                "Ivan Ivanovich Maths 3 06.03.2020",
                "Ivan Pupkin Maths 5 12.03.2021"
            };

            //Act
            var result = searchHandler.SearchQuery(jsonStudents.Read());

            //Assert
            for (var i = 0; i < result.Count(); i++)
                Assert.That(result.ElementAt(i).ToString(), Is.EqualTo(expected[i]));
        }

        [Test]
        public void SearchQuery_BadQuery_KeyNotFoundException()
        {
            //Arrange
            var badQuery =
                "-name Ivan -minmark 6 -maxmark 7 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc";
            var searchHandler = new SearchHandler(badQuery);
            var jsonStudents = new JsonStudents("TestResults.json");

            //Act & Assert
            Assert.Throws<KeyNotFoundException>(() => searchHandler.SearchQuery(jsonStudents.Read()));
        }
    }
}
