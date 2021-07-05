using System;
using App.Domain.core;

namespace App.AppModels
{
    public class HomeworkApp : BaseModel
    {
        public int StudentId { get; set; }
        public int Mark { get; set; }
        public DateTime DatePass { get; set; }
    }
}
