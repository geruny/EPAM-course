using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace ConsoleApp.Test
{
    [TestFixture]
    public class StudentSorterTest
    {
        public StudentSorter StudentSorter = new(
            new JsonStudents("TestResults.json"),
            new SearchHandler("-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")
            );

        [TestCase("-name Ivann -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test AAaaaa -sort mark desc")]
        public void GetStudents_StringWithBadSearchValues_KeyNotFoundException(string input)
        {
            //Arrange
            StudentSorter.SearchHandler = new SearchHandler(input);

            //Act & Assert
            Assert.That(() => StudentSorter.GetStudents(),
                Throws.Exception.TypeOf<KeyNotFoundException>());
        }

        [TestCase("-name  -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-namen Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-name -name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("Ivan")]
        public void GetStudents_StringWithBadFlagsOrBadCountFlags_ArgumentException(string input)
        {
            //Arrange
            StudentSorter.SearchHandler = new SearchHandler(input);

            //Act & Assert
            Assert.That(() => StudentSorter.GetStudents(),
                Throws.ArgumentException);
        }

        [TestCase("-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-minmark 3 -name Ivan -maxmark 5 -dateto 20.12.2021 -test Maths -datefrom 20.11.2019 -sort mark desc")]
        [TestCase("rerege -minmark 3 erfergrege  -name Ivan   ///  rgregeg -maxmark 5 -dateto 20.12.2021 -test Maths -datefrom 20.11.2019 -sort mark desc")]
        [TestCase("-minmark 3     -name Ivan      -maxmark 5 -dateto 20.12.2021 -test Maths    -datefrom 20.11.2019 -sort mark desc")]
        public void GetStudents_String_SortedStudents(string input)
        {
            //Arrange
            StudentSorter.SearchHandler = new SearchHandler(input);
            var expected = new[]
            {
                "Ivan Pupkin Maths 5 12.03.2021",
                "Ivan Ivanovich Maths 3 06.03.2020"
            };

            //Act
            var result = StudentSorter.GetStudents();

            //Assert
            for (var i = 0; i < result.Count(); i++)
                Assert.That(result.ElementAt(i).ToString(), Is.EqualTo(expected[i]));
        }
    }
}
