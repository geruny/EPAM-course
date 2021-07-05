using App.Services.Interfaces;
using App.Services.Models.ReporterModels;
using App.Services.Models.StudentLectureServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReporter _reporter;

        public ReportController(ILogger<ReportController> logger, IReporter reporter)
        {
            _logger = logger;
            _reporter = reporter;
        }

        [HttpGet("studentReport/{studentName}")]
        public ActionResult<StudentReportOutput> GetStudentAttendance(string studentName)
        {
            var report = _reporter.GenerateStudentAttendanceReport(studentName);

            return Ok(report);
        }

        [HttpGet("lectureReport/{lectureName}")]
        public ActionResult<StudentsLectureOutput> GetLectureAttendance(string lectureName)
        {
            var report = _reporter.GenerateLectureAttendanceReport(lectureName);

            return report;
        }
    }
}