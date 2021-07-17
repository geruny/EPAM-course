using System;
using System.Collections.Generic;

namespace App.Domain.core.Models
{
    public class Lecture:BaseModel
    {
        public DateTime DateEvent { get; set; }
        public int LectorId { get; set; }
        public Lector Lector { get; set; }
        public ICollection<LecturesStudents> LecturesStudents { get; set; }
    }
}
