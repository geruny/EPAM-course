using App.Domain.core.Models;

namespace App.Domain.core
{
    public class LecturesStudents
    {
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public  int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
