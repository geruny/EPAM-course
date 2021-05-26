using NUnit.Framework;
using System.Linq;

namespace ConsoleApp.Test
{
    [TestFixture]
    public class StudentSorterTest
    {
        public string Path = "TestResults.json";

        [TestCase("")]
        [TestCase("    ")]
        public void GetStudents_EmptyOrWhiteSpaceString_ArgumentException(string input)
        {
            //Act & Assert
            Assert.That(() => StudentSorter.GetStudents(Path, input),
                Throws.ArgumentException);
        }

        [TestCase("-name Ivann -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test AAaaaa -sort mark desc")]
        public void GetStudents_StringWithBadSearchValues_ArgumentNullException(string input)
        {
            //Act & Assert
            Assert.That(() => StudentSorter.GetStudents(Path, input),
                Throws.ArgumentNullException);
        }

        [TestCase("-namen Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-name -name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("Ivan")]
        public void GetStudents_StringWithBadFlagsOrBadCountFlags_ArgumentException(string input)
        {
            //Act & Assert
            Assert.That(() => StudentSorter.GetStudents(Path, input),
                Throws.ArgumentException);
        }

        [TestCase("-name Ivan -minmark 3 -maxmark 5 -datefrom 20.11.2019 -dateto 20.12.2021 -test Maths -sort mark desc")]
        [TestCase("-minmark 3 -name Ivan -maxmark 5 -dateto 20.12.2021 -test Maths -datefrom 20.11.2019 -sort mark desc")]
        [TestCase("rerege -minmark 3 erfergrege  -name Ivan   ///  rgregeg -maxmark 5 -dateto 20.12.2021 -test Maths -datefrom 20.11.2019 -sort mark desc")]
        [TestCase("-minmark 3     -name Ivan      -maxmark 5 -dateto 20.12.2021 -test Maths    -datefrom 20.11.2019 -sort mark desc")]
        public void GetStudents_String_SortedStudents(string input)
        {
            //Arrange
            var expected = new[]
            {
                "Ivan Pupkin Maths 5 12.03.2021",
                "Ivan Ivanovich Maths 3 06.03.2020"
            };

            //Act
            var result = StudentSorter.GetStudents(Path, input);

            //Assert
            for (var i = 0; i < result.Count(); i++)
                Assert.That(result.ElementAt(i).ToString(), Is.EqualTo(expected[i]));
        }
    }
}
