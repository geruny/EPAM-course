using System;
using System.Collections.Generic;

namespace App.Domain.core.Models
{
    public class Student : BaseModel
    {
        public DateTime DateBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<LecturesStudents> LecturesStudents { get; set; }
    }
}
