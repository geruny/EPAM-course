using App.Controllers;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace App.Unit.Tests
{
    [TestFixture]
    public class StudentControllerTest
    {
        private StudentController _controller;
        private FakeBaseRepository<Student> _repo;

        [OneTimeSetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<StudentController>>();
            _repo = new FakeBaseRepository<Student>();

            var mockRepo = new Mock<IGenericBaseRepository<Student>>();

            //_controller = new StudentController(mockLogger.Object, _repo);
        }

        [Test]
        public void GetMethod_Call_OkResult()
        {
            //Arrange
            //Act
            var response = _controller.Get().Result as ObjectResult;
            var responseValue = response.Value;


            //Assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);


            Assert.AreEqual(_repo.List, ((List<Student>)responseValue));

            //CollectionAssert.AreEqual(_repo.List,((List<Student>)responseValue));


        }
    }
}