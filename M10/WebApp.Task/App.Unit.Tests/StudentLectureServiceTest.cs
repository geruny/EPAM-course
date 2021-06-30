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
            new LecturesStudents() { LectureId = 2, StudentId = 1 },
            new LecturesStudents() { LectureId = 2, StudentId = 3 }
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

        private readonly MockRepository _mockRepo = new(MockBehavior.Default);
        private StudentsLectureService _service;

        [Test]
        public void GetStudents_LectureId_StudentsLectureOutput()
        {
            //Arrange
            var lectureId = _lectures[0].Id;
            var lectureName = _lectures[0].Name;

            var mockLecturesStudentsRepo = _mockRepo.Create<ILecturesStudentsRepository>();
            mockLecturesStudentsRepo.Setup(repo => repo.Get()).Returns(_lecturesStudents);

            var mockLectureRepo = _mockRepo.Create<IGenericBaseRepository<Lecture>>();
            mockLectureRepo.Setup(repo => repo.GetById(lectureId)).Returns(_lectures[lectureId - 1]);

            var mockStudentsRepo = _mockRepo.Create<IGenericBaseRepository<Student>>();
            mockStudentsRepo.Setup(repo => repo.GetById(It.IsInRange(1, 2, Range.Inclusive))).Returns(_students[0]);

            var mockStudentHomeworkService = _mockRepo.Create<IStudentHomeworksService>();

            _service = new StudentsLectureService(mockLecturesStudentsRepo.Object, mockLectureRepo.Object,
                mockStudentsRepo.Object, mockStudentHomeworkService.Object);

            //Act
            var result = _service.GetStudents(lectureId);

            //Assert
            Assert.That(result, Is.TypeOf<StudentsLectureOutput>());
            Assert.That(result.Students, Is.TypeOf<List<StudentServicesModel>>());
            Assert.AreEqual(lectureId, result.LectureId);
            Assert.AreEqual(lectureName, result.LectureName);
            Assert.That(result.Students.Count == 2);
        }

        [Test]
        public void AddStudents_LectureIdStudentsId_StudentsLectureOutput()
        {
            //Arrange
            var lectureId = _lectures[0].Id;
            var lectureName = _lectures[0].Name;

            var mockLecturesStudentsRepo = _mockRepo.Create<ILecturesStudentsRepository>();
            mockLecturesStudentsRepo.Setup(repo => repo.Get()).Returns(_lecturesStudents);

            var mockLectureRepo = _mockRepo.Create<IGenericBaseRepository<Lecture>>();
            mockLectureRepo.Setup(repo => repo.GetById(lectureId)).Returns(_lectures[lectureId - 1]);

            var mockStudentsRepo = _mockRepo.Create<IGenericBaseRepository<Student>>();
            mockStudentsRepo.Setup(repo => repo.GetById(It.IsInRange(1, 2, Range.Inclusive))).Returns(_students[0]);

            var mockStudentHomeworkService = _mockRepo.Create<IStudentHomeworksService>();

            _service = new StudentsLectureService(mockLecturesStudentsRepo.Object, mockLectureRepo.Object,
                mockStudentsRepo.Object, mockStudentHomeworkService.Object);
        }
    }
}
