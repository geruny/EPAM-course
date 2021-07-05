using System;
using App.Domain.core;

namespace App.AppModels
{
    public class LectorApp : BaseModel
    {
        public DateTime DateBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
