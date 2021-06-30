using System.Collections.Generic;

namespace App.Services.Models
{
    public class StudentsLectureOutput
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public List<StudentServicesModel> Students { get; set; }
    }
}
