using App.AppPostModels;
using App.Services.Interfaces;
using App.Services.Models;
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

        [HttpGet("{id:int}")]
        public ActionResult<StudentsLectureOutput> GetStudents(int id)
        {
            var result = _service.GetStudents(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<StudentsLectureOutput> AddStudentsOnLecture(StudentsLectureAppPost studentsLecture)
        {
            var created = _service.AddStudents(studentsLecture.LectureId, studentsLecture.StudentsId);

            if (created == null)
                return BadRequest();

            return Created(nameof(AddStudentsOnLecture), created);
        }
    }
}
