using System.Collections.Generic;
using App.Domain.core;
using App.Services.Models;

namespace App.Services.Interfaces
{
    public interface IStudentsLectureService
    {
        public StudentsLectureOutput GetStudents(int lectureId);
        public StudentsLectureOutput AddStudents(int lecture, List<int> studentsId);
    }
}
