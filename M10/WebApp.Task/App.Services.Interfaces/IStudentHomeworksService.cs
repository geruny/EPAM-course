using App.Domain.core.Models;

namespace App.Services.Interfaces
{
    public interface IStudentHomeworksService
    {
        public void CheckStudentHomework(int studentId, int lectureId);
        public Homework GetStudentHomework(int studentId, int lectureId);
        public void SetHomeworkMark(Homework homework, int mark);
    }
}
