using System;
using System.Collections.Generic;

namespace App.Domain.core.Models
{
    public class Lector : BaseModel
    {
        public DateTime DateBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
    }
}
