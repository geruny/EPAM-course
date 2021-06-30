using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Services.Interfaces;
using System;
using System.Linq;

namespace App.Infrastructure.Business
{
    public class StudentHomeworksService : IStudentHomeworksService
    {
        private readonly IGenericBaseRepository<Homework> _repoHomework;
        private readonly IGenericBaseRepository<Lecture> _repoLecture;

        public StudentHomeworksService(IGenericBaseRepository<Homework> repoHomework, IGenericBaseRepository<Lecture> repoLecture)
        {
            _repoHomework = repoHomework;
            _repoLecture = repoLecture;
        }

        public void CheckStudentHomework(int studentId, int lectureId)
        {
            var studentHomework = GetStudentHomework(studentId, lectureId);
            if (studentHomework != null)
                SetHomeworkMark(studentHomework, new Random().Next(1, 5));
        }

        public Homework GetStudentHomework(int studentId, int lectureId)
        {
            var lecture = _repoLecture.GetById(lectureId);
            var homework = _repoHomework.Get()
                .Where(h => h.StudentId == studentId && h.Name == lecture.Name);

            if (!homework.Any())
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

        public void SetHomeworkMark(Homework homework, int mark)
        {
            if (homework == null) throw new ArgumentNullException(nameof(homework));

            homework.Mark = mark;
            _repoHomework.Update(homework);
        }
    }
}
