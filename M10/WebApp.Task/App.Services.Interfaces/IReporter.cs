using App.Services.Models.ReporterModels;
using App.Services.Models.StudentLectureServiceModels;

namespace App.Services.Interfaces
{
    public interface IReporter
    {
        public StudentReportOutput GenerateStudentAttendanceReport(string studentName);
        public StudentsLectureOutput GenerateLectureAttendanceReport(string lectureName);
    }
}