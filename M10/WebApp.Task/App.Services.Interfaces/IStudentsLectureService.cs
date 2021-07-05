using System.Collections.Generic;
using App.Domain.core;
using App.Services.Models;
using App.Services.Models.StudentLectureServiceModels;

namespace App.Services.Interfaces
{
    public interface IStudentsLectureService
    {
        public StudentsLectureOutput GetStudents(int lectureId);
        public StudentsLectureOutput AddStudents(int lectureId, List<int> studentsId);
    }
}
