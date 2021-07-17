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
    public class StudentsLectureControllerTest
    {
        private WebApplicationFactory<Startup> _webHost;

        [SetUp]
        public void SetUp()
        {
            _webHost = TestsConfigurationSetUp.WebConfigSetUp();
        }

        [Test]
        public async Task AddStudentsOnLecture_StudentLectureAppPost_Created()
        {
            //Arrange
            var dbContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var httpClient = _webHost.CreateClient();

            var request = new
            {
                Url = "StudentsLecture/AddStudentsOnLecture",
                Body = new
                {
                    LectureId = 2,
                    StudentsId = new int[] { 3 }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default,
                "application/json");

            //Act
            var response = await httpClient.PostAsync(request.Url, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<StudentsLectureOutput>(stringResponse);

            Assert.That(resultResponse, Is.TypeOf<StudentsLectureOutput>());
            Assert.AreEqual(dbContext.LecturesStudents.Find(1, 3).StudentId, resultResponse.Students.Last().StudentId);
        }
    }
}