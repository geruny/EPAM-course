using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Services.Interfaces;
using App.Services.Models.ReporterModels;
using App.Services.Models.StudentLectureServiceModels;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Business
{
    public class Reporter:IReporter
    {
        private readonly ILogger<Reporter> _logger;
        private readonly ILecturesStudentsRepository _repoStudentsLecture;
        private readonly IGenericBaseRepository<Student> _repoStudent;
        private readonly IGenericBaseRepository<Lecture> _repoLecture;
        private readonly IStudentsLectureService _studentsLectureService;

        public Reporter(ILecturesStudentsRepository repoStudentsLecture, IGenericBaseRepository<Student> repoStudent, ILogger<Reporter> logger, IGenericBaseRepository<Lecture> repoLecture, IStudentsLectureService studentsLectureService)
        {
            _repoStudentsLecture = repoStudentsLecture;
            _repoStudent = repoStudent;
            _logger = logger;
            _repoLecture = repoLecture;
            _studentsLectureService = studentsLectureService;
        }

        public StudentReportOutput GenerateStudentAttendanceReport(string studentName)
        {
            var studentEnumerable = _repoStudent.Get().Where(s => s.Name == studentName);

            if (studentEnumerable.Count() > 1)
            {
                var ex = new ArgumentException("There are two students with same name in DB");
                _logger.LogError(ex,"Error in reporter");
                return null;
            }

            var student = studentEnumerable.First();
            var allLectures = _repoLecture.Get();
            var lecturesStudents = _repoStudentsLecture.Get();
            var lectureOutputList = new List<StudentReportSubmodel>();

            foreach (var lecture in allLectures)
            {
                var isAttend = lecturesStudents
                    .Any(ls => ls.LectureId == lecture.Id && ls.StudentId == student.Id);

                var outputLecture = new StudentReportSubmodel()
                {
                    LectureId = lecture.Id,
                    LectureName = lecture.Name,
                    LectureEvent = lecture.DateEvent,
                    IsAttend = isAttend
                };
                lectureOutputList.Add(outputLecture);
            }

            var studentReportOutput = new StudentReportOutput()
            {
                StudentId = student.Id,
                StudentName = student.Name,
                Lectures = lectureOutputList
            };

            return studentReportOutput;
        }

        public StudentsLectureOutput GenerateLectureAttendanceReport(string lectureName)
        {
            var lectureId = _repoLecture.Get().Where(l => l.Name == lectureName).Select(l=>l.Id);
            if (!lectureId.Any())
            {
                var ex = new ArgumentException("Lecture is not found");
                _logger.LogError(ex,"Error in reporter");
                return null;
            }

            var report = _studentsLectureService.GetStudents(lectureId.First());

            return report;
        }
    }
}