using System.Collections.Generic;

namespace App.Services.Models.StudentLectureServiceModels
{
    public class StudentsLectureOutput
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public List<StudentsLectureSubmodel> Students { get; set; }
    }
}
