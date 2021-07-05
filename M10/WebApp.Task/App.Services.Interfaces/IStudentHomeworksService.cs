using App.Domain.core.Models;

namespace App.Services.Interfaces
{
    public interface IStudentHomeworksService
    {
        public bool CheckHomeworkExistence(int studentId, int lectureId);
        public Homework GetStudentHomework(int studentId, int lectureId);
        public void SetHomeworkMark(int studentId, int lectureId,int mark);
    }
}
