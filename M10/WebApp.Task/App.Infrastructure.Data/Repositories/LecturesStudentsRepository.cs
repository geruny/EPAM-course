using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core;
using App.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Data.Repositories
{
    public class LecturesStudentsRepository : ILecturesStudentsRepository
    {
        private readonly ILogger<LecturesStudentsRepository> _logger;
        private readonly ApplicationDbContext _context;

        public LecturesStudentsRepository(ApplicationDbContext context, ILogger<LecturesStudentsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<LecturesStudents> Get(Func<LecturesStudents, bool> predicate = null)
        {
            IEnumerable<LecturesStudents> itemList;
            if (predicate != null)
                itemList = _context.LecturesStudents.Where(predicate);
            else
                itemList = _context.LecturesStudents;

            if (!itemList.Any())
            {
                var ex = new KeyNotFoundException($"Items {nameof(LecturesStudents)} in DB not found");
                _logger.LogError(ex, "Error in LecturesStudents repository");
                throw ex;
            }

            return itemList.ToList();
        }

        public LecturesStudents Create(LecturesStudents item)
        {
            _context.LecturesStudents.Add(item);
            _context.SaveChanges();

            var createdItem = _context.LecturesStudents.Find(item.LectureId, item.StudentId);

            if (createdItem == null)
            {
                var ex = new NullReferenceException($"Item {nameof(LecturesStudents)} in DB was not created");
                _logger.LogError(ex, "Error in LecturesStudents repository");
                throw ex;
            }

            return createdItem;
        }
    }
}
