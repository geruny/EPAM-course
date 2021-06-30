using App.Domain.core;
using App.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace App.Unit.Tests
{
    public class FakeBaseRepository<TEntity> : IGenericBaseRepository<TEntity> where TEntity : BaseModel, new()
    {
        public readonly List<TEntity> List = new()
        {
            new TEntity() { Id = 1, Name = "Test1" },
            new TEntity() { Id = 2, Name = "Test2"},
            new TEntity() { Id = 3, Name = "Test3" }
        };

        public IEnumerable<TEntity> Get()
        {
            return List;
        }

        public TEntity GetById(int id)
        {
            return List.Find(n => n.Id == id);
        }

        public TEntity Create(TEntity item)
        {
            List.Add(item);

            return GetById(item.Id);
        }

        public void Update(TEntity item)
        {
            var itemToUpdateIndex = List.FindIndex(n => n.Id == item.Id);
            List[itemToUpdateIndex] = item;
        }

        public void Remove(int id)
        {
            var itemToRemoveIndex = GetById(id);
            List.Remove(itemToRemoveIndex);
        }
    }
}
