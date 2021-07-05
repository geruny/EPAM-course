using App.Domain.core;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Infrastructure.Business;
using App.Services.Interfaces;
using App.Services.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using App.Services.Models.StudentLectureServiceModels;
using Range = Moq.Range;

namespace App.Unit.Tests
{
    [TestFixture]
    public class StudentLectureServiceTest
    {
        private readonly List<LecturesStudents> _lecturesStudents = new()
        {
            new LecturesStudents() { LectureId = 1, StudentId = 1 },
            new LecturesStudents() { LectureId = 1, StudentId = 2 },
            new LecturesStudents() { LectureId = 1, StudentId = 3 },
            new LecturesStudents() { LectureId = 2, StudentId = 1 },
            new LecturesStudents() { LectureId = 2, StudentId = 3 },
        };

        private readonly List<Student> _students = new()
        {
            new Student()
            {
                Id = 1,
                Name = "Tom",
                DateBirth = new DateTime(1995, 03, 23),
                Email = "Tom@Epam.com",
                PhoneNumber = "89112329485"
            },
            new Student()
            {
                Id = 2,
                Name = "Alice",
                DateBirth = new DateTime(2000, 11, 03),
                Email = "Alice@Epam.com",
                PhoneNumber = "89522281488"
            },
            new Student()
            {
                Id = 3,
                Name = "Sam",
                DateBirth = new DateTime(1995, 07, 14),
                Email = "Sam@Epam.com",
                PhoneNumber = "88005553535"
            }
        };

        private readonly List<Lecture> _lectures = new()
        {
            new Lecture() { Id = 1, LectorId = 2, Name = "Module 1", DateEvent = new DateTime(2021, 02, 01) },
            new Lecture() { Id = 2, LectorId = 1, Name = "Module 2", DateEvent = new DateTime(2021, 02, 08) }
        };

        private int _lectureId;
        private string _lectureName;

        private readonly MockRepository _mockRepo = new(MockBehavior.Default);
        private Mock<ILecturesStudentsRepository> _mockLecturesStudentsRepo;
        private Mock<IGenericBaseRepository<Lecture>> _mockLectureRepo;
        private Mock<IGenericBaseRepository<Student>> _mockStudentRepo;
        private Mock<IStudentHomeworksService> _mockStudentHomeworkService;
        private Mock<IStudentService> _mockStudentService;
        private StudentsLectureService _service;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _lectureId = _lectures[0].Id;
            _lectureName = _lectures[0].Name;

            _mockLecturesStudentsRepo = _mockRepo.Create<ILecturesStudentsRepository>();
            _mockLectureRepo = _mockRepo.Create<IGenericBaseRepository<Lecture>>();
            _mockStudentRepo = _mockRepo.Create<IGenericBaseRepository<Student>>();
            _mockStudentHomeworkService = _mockRepo.Create<IStudentHomeworksService>();
            _mockStudentService = _mockRepo.Create<IStudentService>();

            _mockLecturesStudentsRepo.Setup(repo => repo.Get(It.IsAny<Func<LecturesStudents,bool>>()))
                .Returns(_lecturesStudents.Where(l => l.LectureId == _lectureId));
            _mockLectureRepo.Setup(repo => repo.GetById(_lectureId)).Returns(_lectures[0]);
            _mockStudentRepo.Setup(repo => repo.GetById(It.Is<int>(id=>id>=0))).Returns(_students[0]);
        }

        [Test]
        public void GetStudents_LectureId_StudentsLectureOutput()
        {
            //Arrange
            const int expectedCount = 3;

            _service = new StudentsLectureService(_mockLecturesStudentsRepo.Object, _mockLectureRepo.Object,
                _mockStudentRepo.Object, _mockStudentHomeworkService.Object,_mockStudentService.Object);

            //Act
            var result = _service.GetStudents(_lectureId);

            //Assert
            Assert.That(result, Is.TypeOf<StudentsLectureOutput>());
            Assert.That(result.Students, Is.TypeOf<List<StudentsLectureSubmodel>>());
            Assert.AreEqual(_lectureId, result.LectureId);
            Assert.AreEqual(_lectureName, result.LectureName);
            Assert.AreEqual(expectedCount,result.Students.Count);
        }

        [Test]
        public void AddStudents_LectureIdStudentsId_StudentsLectureOutput()
        {
            //Arrange
            const int expectedCount = 3;
            var callBackCreatedLecturesStudents = new List<LecturesStudents>();

            _mockLecturesStudentsRepo.Setup(repo =>
                    repo.Create(It.Is<LecturesStudents>(ls => ls.LectureId == _lectureId && ls.StudentId >= 1)))
                .Callback<LecturesStudents>(ls => callBackCreatedLecturesStudents.Add(ls));
            _mockStudentRepo.Setup(repo => repo.Get(It.IsAny<Func<Student, bool>>())).Returns(_students);
            _mockStudentHomeworkService.Setup(repo =>
                repo.SetHomeworkMark(It.IsAny<int>(), _lectureId, It.IsInRange(1, 5, Range.Inclusive)));
            _mockStudentService.Setup(service => service.CheckStudentTurnout(It.Is<int>(id => id >= 0)));

            _service = new StudentsLectureService(_mockLecturesStudentsRepo.Object, _mockLectureRepo.Object,
                _mockStudentRepo.Object, _mockStudentHomeworkService.Object,_mockStudentService.Object);

            //Act
            var result= _service.AddStudents(_lectureId, new List<int> {1, 2, 3});

            //Assert
            Assert.That(result, Is.TypeOf<StudentsLectureOutput>());
            Assert.AreEqual(expectedCount,callBackCreatedLecturesStudents.Count);
        }
    }
}
