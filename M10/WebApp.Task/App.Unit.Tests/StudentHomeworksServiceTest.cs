using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Infrastructure.Business;
using App.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace App.Unit.Tests
{
    [TestFixture]
    public class StudentHomeworksServiceTest
    {
        private readonly List<Homework> _homeworks = new()
        {
            new Homework { Id = 1, StudentId = 1, Name = "Module 1", DatePass = new DateTime(2021, 01, 12) },
            new Homework { Id = 2, StudentId = 1, Name = "Module 2", DatePass = new DateTime(2021, 01, 19) },
            new Homework { Id = 3, StudentId = 2, Name = "Module 1", DatePass = new DateTime(2021, 01, 14) },
        };

        private readonly List<Lecture> _lectures = new()
        {
            new Lecture() { Id = 1, LectorId = 2, Name = "Module 1", DateEvent = new DateTime(2021, 02, 01) },
            new Lecture() { Id = 2, LectorId = 1, Name = "Module 2", DateEvent = new DateTime(2021, 02, 08) }
        };

        private int studentId = 1;
        private int lectureId = 1;

        private readonly MockRepository _mockRepo = new(MockBehavior.Default);
        private Mock<IGenericBaseRepository<Lecture>> _mockLectureRepo;
        private Mock<IGenericBaseRepository<Homework>> _mockHomeworkRepo;
        private Mock<IStudentService> _mockStudentService;
        private StudentHomeworksService _service;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockLectureRepo = _mockRepo.Create<IGenericBaseRepository<Lecture>>();
            _mockHomeworkRepo = _mockRepo.Create<IGenericBaseRepository<Homework>>();
            _mockStudentService = _mockRepo.Create<IStudentService>();

            _mockLectureRepo.Setup(repo => repo.GetById(lectureId)).Returns(_lectures[0]);
            _mockHomeworkRepo.Setup(repo => repo.Update(It.IsAny<Homework>()));
        }

        [SetUp]
        public void SetUp()
        {
            _mockHomeworkRepo.Setup(repo => repo.Get()).Returns(_homeworks);
            _mockHomeworkRepo.Setup(repo => repo.Create(It.IsAny<Homework>()));
        }

        [Test]
        public void CheckStudentHomework_StudentIdLectureIdWithHomework_True()
        {
            //Arrange
            _service = new StudentHomeworksService(_mockHomeworkRepo.Object, _mockLectureRepo.Object,_mockStudentService.Object);

            //Act
            var result = _service.CheckStudentHomework(studentId, lectureId);

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CheckStudentHomework_StudentIdLectureIdWithoutHomework_False()
        {
            //Arrange
            Homework callBackCreatedHomework = null;
            List<Homework> homeworks = new()
            {
                new Homework { Id = 5, StudentId = 3, Name = "Module 1", DatePass = new DateTime(2021, 01, 11) },
                new Homework { Id = 6, StudentId = 3, Name = "Module 2", DatePass = new DateTime(2021, 02, 08) }
            };
            var expected = new Homework()
            {
                StudentId = studentId,
                Name = _lectures[lectureId - 1].Name,
                DatePass = _lectures[lectureId - 1].DateEvent,
                Mark = 0
            };

            _mockHomeworkRepo.Setup(repo => repo.Create(It.IsAny<Homework>()))
                .Callback<Homework>(h => callBackCreatedHomework = h); ;
            _mockHomeworkRepo.Setup(repo => repo.Get()).Returns(homeworks);

            _service = new StudentHomeworksService(_mockHomeworkRepo.Object, _mockLectureRepo.Object,_mockStudentService.Object);

            //Act
            var result = _service.CheckStudentHomework(studentId, lectureId);

            //Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(expected.StudentId, callBackCreatedHomework.StudentId);
            Assert.AreEqual(expected.Name, callBackCreatedHomework.Name);
            Assert.AreEqual(expected.DatePass, callBackCreatedHomework.DatePass);
            Assert.AreEqual(expected.Mark, callBackCreatedHomework.Mark);
        }

        [Test]
        public void GetStudentHomework_StudentIdLectureIdWithHomework_Homework()
        {
            //Arrange
            var expected = _homeworks[0];

            _service = new StudentHomeworksService(_mockHomeworkRepo.Object, _mockLectureRepo.Object,_mockStudentService.Object);

            //Act
            var result = _service.GetStudentHomework(studentId, lectureId);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetStudentHomework_StudentIdLectureIdWithoutHomework_Homework()
        {
            //Arrange
            List<Homework> homeworks = new()
            {
                new Homework { Id = 5, StudentId = 3, Name = "Module 1", DatePass = new DateTime(2021, 01, 11) },
                new Homework { Id = 6, StudentId = 3, Name = "Module 2", DatePass = new DateTime(2021, 02, 08) }
            };

            _mockHomeworkRepo.Setup(repo => repo.Get()).Returns(homeworks);

            _service = new StudentHomeworksService(_mockHomeworkRepo.Object, _mockLectureRepo.Object,_mockStudentService.Object);

            //Act
            var result = _service.GetStudentHomework(studentId, lectureId);

            //Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void SetHomeworkMark_StudentIdLectureIdMarkWithHomework_Void()
        {
            //Arrange
            var mark = 5;

            _mockStudentService.Setup(service => service.CheckStudentAverageScore(studentId));
            _service = new StudentHomeworksService(_mockHomeworkRepo.Object, _mockLectureRepo.Object,_mockStudentService.Object);

            //Act
            _service.SetHomeworkMark(studentId, lectureId, mark);

            //Assert
            Assert.AreEqual(mark, _homeworks[0].Mark);
        }

        [Test]
        public void SetHomeworkMark_StudentIdLectureIdMarkWithoutHomework_Void()
        {
            //Arrange
            var mark = 5;
            Homework callBackCreatedHomework = null;
            List<Homework> homeworks = new()
            {
                new Homework { Id = 5, StudentId = 3, Name = "Module 1", DatePass = new DateTime(2021, 01, 11) },
                new Homework { Id = 6, StudentId = 3, Name = "Module 2", DatePass = new DateTime(2021, 02, 08) }
            };

            _mockHomeworkRepo.Setup(repo => repo.Create(It.IsAny<Homework>()))
                .Callback<Homework>(h => callBackCreatedHomework = h);
            _mockHomeworkRepo.Setup(repo => repo.Get()).Returns(homeworks);
            _mockStudentService.Setup(service => service.CheckStudentAverageScore(studentId));

            _service = new StudentHomeworksService(_mockHomeworkRepo.Object, _mockLectureRepo.Object,_mockStudentService.Object);

            //Act
            _service.SetHomeworkMark(studentId, lectureId, mark);

            //Assert
            Assert.AreEqual(0, callBackCreatedHomework.Mark);
        }
    }
}
