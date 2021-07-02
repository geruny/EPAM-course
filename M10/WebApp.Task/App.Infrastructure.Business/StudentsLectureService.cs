using System;
using App.Domain.core;
using App.Domain.Interfaces;
using App.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core.Models;
using App.Services.Models;
using App.Services.Models.StudentLectureServiceModels;

namespace App.Infrastructure.Business
{
    public class StudentsLectureService : IStudentsLectureService
    {
        private readonly ILecturesStudentsRepository _repoStudentsLecture;
        private readonly IGenericBaseRepository<Lecture> _repoLecture;
        private readonly IGenericBaseRepository<Student> _repoStudent;
        private readonly IStudentHomeworksService _studentHomeworksService;
        private readonly IStudentService _studentService;

        public StudentsLectureService(ILecturesStudentsRepository repoStudentsLecture, IGenericBaseRepository<Lecture> repoLecture, IGenericBaseRepository<Student> repoStudent, IStudentHomeworksService studentHomeworksService, IStudentService studentService)
        {
            _repoStudentsLecture = repoStudentsLecture;
            _repoLecture = repoLecture;
            _repoStudent = repoStudent;
            _studentHomeworksService = studentHomeworksService;
            _studentService = studentService;
        }

        public StudentsLectureOutput GetStudents(int lectureId)
        {
            var studentsId = _repoStudentsLecture.Get()
                .Where(l => l.LectureId == lectureId)
                .Select(s => s.StudentId);

            var listOutputStudents = studentsId.Select(studentId =>
                new StudentsLectureSubmodel()
                {
                    StudentId = studentId,
                    StudentName = _repoStudent.GetById(studentId).Name
                }
            ).ToList();

            var outputItem = new StudentsLectureOutput()
            {
                LectureId = lectureId,
                LectureName = _repoLecture.GetById(lectureId).Name,
                Students = listOutputStudents
            };

            return outputItem;
        }

        public StudentsLectureOutput AddStudents(int lectureId, List<int> studentsId)
        {
            var allStudentsId = _repoStudent.Get().Select(s => s.Id);

            var absentStudentsId = allStudentsId.Except(studentsId);
            if (absentStudentsId.Any())
            {
                foreach (var studentId in absentStudentsId)
                {
                    _studentService.CheckStudentTurnout(studentId);
                    _studentHomeworksService.SetHomeworkMark(studentId, lectureId, 0);
                }
            }

            foreach (var studentId in studentsId)
            {
                _repoStudentsLecture.Create(new LecturesStudents()
                {
                    LectureId = lectureId,
                    StudentId = studentId
                });

                _studentHomeworksService.SetHomeworkMark(studentId, lectureId, new Random().Next(1, 5));
            }

            return GetStudents(lectureId);
        }
    }
}
