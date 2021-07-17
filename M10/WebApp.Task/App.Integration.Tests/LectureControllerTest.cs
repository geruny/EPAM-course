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
    public class LectureControllerTest
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
            var response = await httpClient.GetAsync("Lecture/");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<IEnumerable<Lecture>>(stringResponse);

            Assert.AreEqual(dbContext.Lectures.Count(), resultResponse.Count());
        }

        [Test]
        public async Task Get_LectureId_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Lecture/1");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<Lecture>(stringResponse);

            Assert.AreEqual(dbContext.Lectures.Find(1).Name, resultResponse.Name);
        }

        [Test]
        public async Task Get_NonExistentId_NotFound()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Lecture/0");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Post_Lecture_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Lecture/",
                Body = new
                {
                    Name = "Test",
                    LectorId = 1,
                    DateEvent = "2021-07-02T23:18:38.047Z"
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PostAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<Lecture>(stringResponse);

            Assert.That(resultResponse, Is.TypeOf<Lecture>());
            Assert.AreEqual("Test", resultResponse.Name);
        }

        [Test]
        public async Task Post_InvalidData_BadRequest()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Lecture/",
                Body = new
                {
                    Name = "Test",
                    LectorId = "test@epam.com",
                    DateEvent = 11
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
        public async Task Put_Lecture_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Lecture/",
                Body = new
                {
                    Id=1,
                    Name = "Test",
                    LectorId = 1,
                    DateEvent = "2021-07-02T23:18:38.047Z"
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PutAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(dbContext.Lectures.Find(1).Name, "Test");
        }

        [Test]
        public async Task Put_InvalidData_NotFound()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Lecture/",
                Body = new
                {
                    Id = 1,
                    Name = "Test",
                    LectorId = "test@epam.com",
                    DateEvent = 111
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
        public async Task Delete_LectureId_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync("Lecture/1");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(dbContext.Lectures.Find(1), null);
        }

        [Test]
        public async Task Delete_NonExistentId_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync("Lecture/0");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}