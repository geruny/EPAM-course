using App.AppPostModels;
using App.Services.Interfaces;
using App.Services.Models;
using App.Services.Models.StudentLectureServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsLectureController : ControllerBase
    {
        private readonly ILogger<StudentsLectureController> _logger;
        private readonly IStudentsLectureService _service;

        public StudentsLectureController(ILogger<StudentsLectureController> logger, IStudentsLectureService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost( "AddStudentsOnLecture")]
        public ActionResult<StudentsLectureOutput> AddStudentsOnLecture(StudentsLectureAppPost studentsLecture)
        {
            var created = _service.AddStudents(studentsLecture.LectureId, studentsLecture.StudentsId);

            return Created(nameof(AddStudentsOnLecture), created);
        }
    }
}
