using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core;
using App.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity> : IGenericBaseRepository<TEntity> where TEntity : BaseModel
    {
        private readonly ILogger<BaseRepository<TEntity>> _logger;
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context, ILogger<BaseRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<TEntity> Get()
        {
            var itemList = _context.Set<TEntity>().ToList();
            if (!itemList.Any())
            {
                var ex = new NullReferenceException($"Items {typeof(TEntity)} in DB not found");
                _logger.LogError(ex, "Error in Base repository");
            }

            return itemList;
        }

        public TEntity GetById(int id)
        {
            var item = _context.Set<TEntity>().Find(id);

            if (item == null)
            {
                var ex = new NullReferenceException($"Item {typeof(TEntity)} in DB not found");
                _logger.LogError(ex, "Error in Base repository");
            }

            return item;
        }

        public TEntity Create(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();

            var createdItem = GetById(item.Id);

            if (createdItem == null)
            {
                var ex= new NullReferenceException($"Item {typeof(TEntity)} in DB was not created");
                _logger.LogError(ex, "Error in Base repository");
            }

            return createdItem;
        }

        public void Update(TEntity item)
        {
            var itemToUpdate = GetById(item.Id);

            _context.Entry(itemToUpdate).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = GetById(id);

            _context.Set<TEntity>().Remove(itemToRemove);
            _context.SaveChanges();
        }
    }
}
