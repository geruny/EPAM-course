using System;
using App.Domain.core;

namespace App.AppModels
{
    public class LectureApp:BaseModel
    {
        public int LectorId { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
