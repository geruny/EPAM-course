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

        public bool CheckStudentHomework(int studentId, int lectureId)
        {
            var homework = GetStudentHomework(studentId, lectureId);

            if (homework == null)
            {
                var lecture = _repoLecture.GetById(lectureId);
                _repoHomework.Create(new Homework()
                {
                    StudentId = studentId,
                    Name = lecture.Name,
                    DatePass = lecture.DateEvent,
                    Mark = 0
                });

                return false;
            }

            return true;
        }

        public Homework GetStudentHomework(int studentId, int lectureId)
        {
            var lecture = _repoLecture.GetById(lectureId);
            var homework = _repoHomework.Get()
                .Where(h => h.StudentId == studentId && h.Name == lecture.Name);

            if (!homework.Any())
                return null;

            return homework.First();
        }

        public void SetHomeworkMark(int studentId, int lectureId, int mark)
        {
            var isHomeworkExist = CheckStudentHomework(studentId, lectureId);

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
