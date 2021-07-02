using System;
using System.Linq;
using App.Domain.core.Models;
using App.Domain.Interfaces;
using App.Services.Interfaces;
using static App.Infrastructure.Business.ServiceFactory;

namespace App.Infrastructure.Business
{
    public class StudentService : IStudentService
    {
        private readonly ILecturesStudentsRepository _repoStudentsLecture;
        private readonly IGenericBaseRepository<Lecture> _repoLecture;
        private readonly IGenericBaseRepository<Lector> _repoLector;
        private readonly IGenericBaseRepository<Student> _repoStudent;
        private readonly IGenericBaseRepository<Homework> _repoHomework;
        private readonly ISender _mailSender;
        private readonly ISender _messageSender;

        public StudentService(ILecturesStudentsRepository repoStudentsLecture, IGenericBaseRepository<Lecture> repoLecture, IGenericBaseRepository<Student> repoStudent, IGenericBaseRepository<Lector> repoLector, IGenericBaseRepository<Homework> repoHomework, ServiceResolver serviceResolver)
        {
            _repoStudentsLecture = repoStudentsLecture;
            _repoLecture = repoLecture;
            _repoStudent = repoStudent;
            _repoLector = repoLector;
            _repoHomework = repoHomework;

            _mailSender = serviceResolver(ServiceType.MailSender);
            _messageSender = serviceResolver(ServiceType.MessageSender);
        }

        public void CheckStudentTurnout(int studentId)
        {
            var countStudentLectures = _repoStudentsLecture.Get()
                .Where(sl => sl.StudentId == studentId)
                .Select(ls => ls.LectureId).Count();
            var countAllLectures = _repoLecture.Get().Count();

            var difference = countAllLectures - countStudentLectures;
            if (difference <= 3) return;

            var lectorId = _repoLecture.Get().Last().LectorId;
            var lectorEmail = _repoLector.GetById(lectorId).Email;
            var studentEmail = _repoStudent.GetById(studentId).Email;

            _mailSender.Send(lectorEmail);
            _mailSender.Send(studentEmail);
        }

        public void CheckStudentAverageScore(int studentId)
        {
            var studentHomeworkMarks = _repoHomework.Get()
                .Where(h => h.StudentId == studentId)
                .Select(h => h.Mark);

            var studentAverageScore = studentHomeworkMarks.Sum() / studentHomeworkMarks.Count();
            if (studentAverageScore >= 4) return;

            var studentPhoneNumber = _repoStudent.GetById(studentId).PhoneNumber;
            _messageSender.Send(studentPhoneNumber);
        }
    }
}