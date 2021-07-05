using App.Infrastructure.Data;
using App.Services.Models.StudentLectureServiceModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Integration.Tests
{
    [TestFixture]
    public class HomeworkServiceControllerTest
    {
        private WebApplicationFactory<Startup> _webHost;

        [SetUp]
        public void SetUp()
        {
            _webHost = TestsConfigurationSetUp.WebConfigSetUp();
        }

        [Test]
        public async Task CheckStudentHomeworkExistence_StudentIdLectureId_Ok()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            //Act
            var response = await httpClient.GetAsync("HomeworkService/CheckStudentHomeworkExistence/1/2");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<bool>(stringResponse);

            Assert.AreEqual(true, resultResponse);
        }

        [Test]
        public async Task SetHomeworkMark_HomeworkServiceAppPost_NoContent()
        {
            //Arrange
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "HomeworkService/SetHomeworkMark",
                Body = new
                {
                    LectureId = 2,
                    StudentId = 1,
                    Mark=4
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PostAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}