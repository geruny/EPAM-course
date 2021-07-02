using System.Collections.Generic;

namespace App.Services.Models.ReporterModels
{
    public class StudentReportOutput
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<StudentReportSubmodel> Lectures { get; set; }
    }
}