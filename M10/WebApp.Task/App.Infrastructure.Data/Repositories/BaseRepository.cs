using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate = null)
        {
            IEnumerable<TEntity> itemList;
            if (predicate != null)
                itemList = _context.Set<TEntity>().Where(predicate);
            else
                itemList = _context.Set<TEntity>();

            if (!itemList.Any())
            {
                var ex = new KeyNotFoundException($"Items {typeof(TEntity).Name} in DB not found");
                _logger.LogError(ex, "Error in Base repository");
                throw ex;
            }

            return itemList.ToList();
        }

        public TEntity GetById(int id)
        {
            var item = _context.Set<TEntity>().Find(id);

            if (item == null)
            {
                var ex = new KeyNotFoundException($"Item {typeof(TEntity).Name} in DB not found");
                _logger.LogError(ex, "Error in Base repository");
                throw ex;
            }

            return item;
        }

        public TEntity Create(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();

            var createdItem = _context.Set<TEntity>().Find(item.Id);

            if (createdItem == null)
            {
                var ex = new NullReferenceException($"Item {typeof(TEntity).Name} in DB was not created");
                _logger.LogError(ex, "Error in Base repository");
                throw ex;
            }

            return createdItem;
        }

        public void Update(TEntity item)
        {
            var itemToUpdate = _context.Set<TEntity>().Find(item.Id);

            if (itemToUpdate == null)
            {
                var ex = new KeyNotFoundException($"Item {typeof(TEntity).Name} in DB not found");
                _logger.LogError(ex, "Error in Base repository");
                throw ex;
            }

            _context.Entry(itemToUpdate).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = _context.Set<TEntity>().Find(id);

            if (itemToRemove == null)
            {
                var ex = new KeyNotFoundException($"Item {typeof(TEntity).Name} in DB not found");
                _logger.LogError(ex, "Error in Base repository");
                throw ex;
            }

            _context.Set<TEntity>().Remove(itemToRemove);
            _context.SaveChanges();
        }
    }
}
