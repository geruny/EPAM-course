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
    public class HomeworkControllerTest
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
            var response = await httpClient.GetAsync("Homework/");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<IEnumerable<Homework>>(stringResponse);

            Assert.AreEqual(dbContext.Homeworks.Count(), resultResponse.Count());
        }

        [Test]
        public async Task Get_HomeworkId_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Homework/1");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<Homework>(stringResponse);

            Assert.AreEqual(dbContext.Homeworks.Find(1).Name, resultResponse.Name);
        }

        [Test]
        public async Task Get_NonExistentId_NotFound()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("Homework/0");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Post_Homework_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Homework/",
                Body = new
                {
                    Name = "Test",
                    DatePass = "2021-07-02T23:18:38.047Z",
                    StudentId = 1
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PostAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<Homework>(stringResponse);

            Assert.That(resultResponse, Is.TypeOf<Homework>());
            Assert.AreEqual("Test", resultResponse.Name);
        }

        [Test]
        public async Task Post_InvalidData_BadRequest()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Homework/",
                Body = new
                {
                    Name = "Test",
                    DatePass = 11,
                    StudentId = 1
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
        public async Task Put_Homework_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Homework/",
                Body = new
                {
                    Id = 1,
                    Name = "Test",
                    DatePass = "2021-07-02T23:18:38.047Z",
                    StudentId = 1
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PutAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(dbContext.Homeworks.Find(1).Name, "Test");
        }

        [Test]
        public async Task Put_InvalidData_NotFound()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "Homework/",
                Body = new
                {
                    Id = 1,
                    Name = 2,
                    DatePass = "2021-07-02T23:18:38.047Z",
                    StudentId = 1
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
        public async Task Delete_HomeworkId_Ok()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync("Homework/1");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(dbContext.Homeworks.Find(1), null);
        }

        [Test]
        public async Task Delete_NonExistentId_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync("Homework/0");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}