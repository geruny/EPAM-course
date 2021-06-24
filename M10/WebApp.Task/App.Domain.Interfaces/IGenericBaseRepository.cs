using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.core;

namespace App.Domain.Interfaces
{
    public interface IGenericBaseRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity GetById(int id);
        TEntity Create(TEntity item);
        void Update(TEntity item);
        void Remove(int id);
    }
}
