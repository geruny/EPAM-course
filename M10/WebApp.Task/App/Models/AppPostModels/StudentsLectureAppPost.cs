using System.Collections.Generic;

namespace App.AppPostModels
{
    public class StudentsLectureAppPost
    {
        public int LectureId { get; set; }
        public List<int> StudentsId { get; set; }
    }
}
