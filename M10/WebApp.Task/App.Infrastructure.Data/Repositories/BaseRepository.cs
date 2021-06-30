using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.core;
using App.Domain.Interfaces;

namespace App.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity> : IGenericBaseRepository<TEntity> where TEntity : BaseModel
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> Get()
        {
            var itemList = _context.Set<TEntity>().ToList();
            if (!itemList.Any())
                throw new NullReferenceException($"Items {typeof(TEntity)} in DB not found");

            return itemList;
        }

        public TEntity GetById(int id)
        {
            var item = _context.Set<TEntity>().Find(id) ??
                     throw new NullReferenceException($"Item {typeof(TEntity)} in DB not found");

            return item;
        }

        public TEntity Create(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();

            TEntity createdItem;
            try
            {
                createdItem = GetById(item.Id);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException($"Item {typeof(TEntity)} in DB was not created");
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
