using System.Collections.Generic;

namespace App.Domain.core.Models
{
    public class Student : BaseModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<LecturesStudents> LecturesStudents { get; set; }
    }
}
