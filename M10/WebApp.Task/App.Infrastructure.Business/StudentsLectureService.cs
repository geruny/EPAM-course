using System;
using App.Domain.core;
using App.Domain.Interfaces;
using App.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core.Models;
using App.Services.Models;

namespace App.Infrastructure.Business
{
    public class StudentsLectureService : IStudentsLectureService
    {
        private readonly ILecturesStudentsRepository _repoStudentsLecture;
        private readonly IGenericBaseRepository<Lecture> _repoLecture;
        private readonly IGenericBaseRepository<Student> _repoStudent;
        private readonly IStudentHomeworksService _studentHomeworksService;

        public StudentsLectureService(ILecturesStudentsRepository repoStudentsLecture, IGenericBaseRepository<Lecture> repoLecture, IGenericBaseRepository<Student> repoStudent, IStudentHomeworksService studentHomeworksService)
        {
            _repoStudentsLecture = repoStudentsLecture;
            _repoLecture = repoLecture;
            _repoStudent = repoStudent;
            _studentHomeworksService = studentHomeworksService;
        }

        public StudentsLectureOutput GetStudents(int lectureId)
        {
            var studentsId = _repoStudentsLecture.Get()
                .Where(l => l.LectureId == lectureId)
                .Select(s => s.StudentId);

            var listOutputStudents = studentsId.Select(studentId =>
                new StudentServicesModel()
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

            var existStudentsInDb = studentsId.Except(allStudentsId);
            if (existStudentsInDb.Any())
                throw new ArgumentException("Not all students are exist in DB");

            var absentStudentsId = allStudentsId.Except(studentsId);

            if (absentStudentsId.Any())
            {
                foreach (var studentId in absentStudentsId)
                {
                    var homework = _studentHomeworksService.GetStudentHomework(studentId, lectureId);
                    if (homework != null)
                        _studentHomeworksService.SetHomeworkMark(homework, 0);
                }
            }

            foreach (var studentId in studentsId)
            {
                var createdResult = _repoStudentsLecture.Create(new LecturesStudents()
                {
                    LectureId = lectureId, 
                    StudentId = studentId
                });

                if (createdResult == null)
                    return null;

                _studentHomeworksService.CheckStudentHomework(studentId, lectureId);
            }

            return GetStudents(lectureId);
        }
    }
}
