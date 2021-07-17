using App.Domain.core.Models;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Integration.Tests
{
    [TestFixture]
    public class StudentControllerTest
    {
        private WebApplicationFactory<Startup> _webHost;

        [SetUp]
        public void SetUp()
        {
            _webHost = TestsConfigurationSetUp.WebConfigSetUp();
        }

        [Test]
        public async Task Get_SendRequest_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Student/");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var studentsRespons = JsonConvert.DeserializeObject<IEnumerable<Student>>(stringResponse);

            Assert.AreEqual(dbContext.Students.Count(), studentsRespons.Count());
        }

        [Test]
        public async Task Get_StudentId_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Student/1");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var studentsRespons = JsonConvert.DeserializeObject<Student>(stringResponse);

            Assert.AreEqual(dbContext.Students.Find(1).Name, studentsRespons.Name);
        }

        [Test]
        public async Task Get_NonExistentId_NotFound()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Student/0");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Post_Student_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Student/",
                Body = new
                {
                    Name = "Test",
                    PhoneNumber = "89523459674",
                    Email = "test@epam.com",
                    DateBirth = "2021-07-02T23:18:38.047Z"
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PostAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var studentsRespons = JsonConvert.DeserializeObject<Student>(stringResponse);

            Assert.That(studentsRespons, Is.TypeOf<Student>());
            Assert.AreEqual("Test", studentsRespons.Name);
        }

        [Test]
        public async Task Post_InvalidData_BadRequest()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Student/",
                Body = new
                {
                    Name = "Test",
                    PhoneNumber = 89523459674,
                    Email = "test@epam.com",
                    DateBirth = 11
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PostAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Put_Student_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Student/",
                Body = new
                {
                    Id = 1,
                    Name = "Test",
                    PhoneNumber = "89523459674",
                    Email = "test@epam.com",
                    DateBirth = "2021-07-02T23:18:38.047Z"
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PutAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(dbContext.Students.Find(1).Name, "Test");
        }

        [Test]
        public async Task Put_InvalidData_NotFound()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Student/",
                Body = new
                {
                    Id = 1,
                    Name = "Test",
                    PhoneNumber = 89523459674,
                    Email = "test@epam.com",
                    DateBirth = 111
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PutAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Delete_StudentId_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync("Student/1");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(dbContext.Students.Find(1), null);
        }

        [Test]
        public async Task Delete_NonExistentId_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync("Student/0");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}