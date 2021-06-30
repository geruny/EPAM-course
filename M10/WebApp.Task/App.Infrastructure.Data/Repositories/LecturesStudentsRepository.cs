using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core;
using App.Domain.Interfaces;

namespace App.Infrastructure.Data.Repositories
{
    public class LecturesStudentsRepository : ILecturesStudentsRepository
    {
        private readonly ApplicationDbContext _context;

        public LecturesStudentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LecturesStudents> Get()
        {
            var itemList = _context.LecturesStudents.ToList();
            if (!itemList.Any())
                throw new NullReferenceException($"Items {typeof(LecturesStudents)} in DB not found");

            return itemList;
        }

        public LecturesStudents Create(LecturesStudents item)
        {
            _context.LecturesStudents.Add(item);
            _context.SaveChanges();

            var createdItem = _context.LecturesStudents.Find(item.LectureId, item.StudentId) ??
                            throw new NullReferenceException($"Item {typeof(LecturesStudents)} in DB was not created");

            return createdItem;
        }
    }
}
