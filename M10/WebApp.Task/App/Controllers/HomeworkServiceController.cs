using App.Models.AppPostModels;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeworkServiceController : ControllerBase
    {
        private readonly ILogger<HomeworkServiceController> _logger;
        private readonly IStudentHomeworksService _studentHomeworkService;

        public HomeworkServiceController(ILogger<HomeworkServiceController> logger,
            IStudentHomeworksService studentHomeworkService)
        {
            _logger = logger;
            _studentHomeworkService = studentHomeworkService;
        }

        [HttpGet("CheckStudentHomeworkExistence/{studentId:int}/{lectureId:int}")]
        public ActionResult<bool> CheckStudentHomeworkExistence(int studentId, int lectureId)
        {
            var isHomeworkExist = _studentHomeworkService.CheckHomeworkExistence(studentId, lectureId);

            return Ok(isHomeworkExist);
        }

        [HttpPost("SetHomeworkMark")]
        public IActionResult SetHomeworkMark(HomeworkServiceAppPost item)
        {
            _studentHomeworkService.SetHomeworkMark(item.StudentId, item.LectureId, item.Mark);

            return NoContent();
        }
    }
}