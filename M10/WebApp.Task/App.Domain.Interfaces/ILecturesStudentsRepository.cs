using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.core;

namespace App.Domain.Interfaces
{
    public interface ILecturesStudentsRepository
    {
        public IEnumerable<LecturesStudents> Get();
        public LecturesStudents Create(LecturesStudents item);
    }
}
