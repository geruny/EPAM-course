using App.AppPostModels;
using App.Domain.core;
using App.Services.Interfaces;
using App.Services.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentHomeworksController : ControllerBase
    {
        private readonly ILogger<StudentHomeworksController> _logger;
        private readonly IStudentsLectureService _service;

        public StudentHomeworksController(ILogger<StudentHomeworksController> logger, IStudentsLectureService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<StudentsLectureOutput> GetStudentHomeworks(int id)
        {
            return Ok(_service.GetStudents(id));
        }
    }
}
