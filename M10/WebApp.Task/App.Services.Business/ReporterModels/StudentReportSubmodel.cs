using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Models.ReporterModels
{
    public class StudentReportSubmodel
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public DateTime LectureEvent { get; set; }
        public bool IsAttend { get; set; }
    }
}
