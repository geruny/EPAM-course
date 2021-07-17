using System.Collections.Generic;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Services.Interfaces;
using System.Linq;

namespace App.Infrastructure.Business
{
    public class StudentHomeworksService : IStudentHomeworksService
    {
        private readonly IGenericBaseRepository<Homework> _repoHomework;
        private readonly IGenericBaseRepository<Lecture> _repoLecture;
        private readonly IStudentService _studentService;

        public StudentHomeworksService(IGenericBaseRepository<Homework> repoHomework, IGenericBaseRepository<Lecture> repoLecture, IStudentService studentService)
        {
            _repoHomework = repoHomework;
            _repoLecture = repoLecture;
            _studentService = studentService;
        }

        public bool CheckHomeworkExistence(int studentId, int lectureId)
        {
            var homework = GetStudentHomework(studentId, lectureId);

            return homework != null && homework.Mark != 0;
        }

        public Homework GetStudentHomework(int studentId, int lectureId)
        {
            var lecture = _repoLecture.GetById(lectureId);
            IEnumerable<Homework> homework;

            try
            {
                homework = _repoHomework.Get(h => h.StudentId == studentId && h.Name == lecture.Name);
            }
            catch (KeyNotFoundException)
            {
                _repoHomework.Create(new Homework()
                {
                    StudentId = studentId,
                    Name = lecture.Name,
                    DatePass = lecture.DateEvent,
                    Mark = 0
                });
                return null;
            }

            return homework.First();
        }

        public void SetHomeworkMark(int studentId, int lectureId, int mark)
        {
            var isHomeworkExist = CheckHomeworkExistence(studentId, lectureId);

            if (isHomeworkExist)
            {
                var homework = GetStudentHomework(studentId, lectureId);
                homework.Mark = mark;
                _repoHomework.Update(homework);
            }

            _studentService.CheckStudentAverageScore(studentId);
        }
    }
}
