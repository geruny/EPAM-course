using App.Domain.core;
using App.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace App.Infrastructure.Data
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
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity Create(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();

            return GetById(item.Id);
        }

        public void Update(TEntity item)
        {
            var itemToUpdate = _context.Set<TEntity>().Find(item.Id);
            _context.Entry(itemToUpdate).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(itemToRemove);
            _context.SaveChanges();
        }
    }
}
