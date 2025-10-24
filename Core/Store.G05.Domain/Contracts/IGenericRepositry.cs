using Store.G05.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Contracts
{
    public interface IGenericRepositry<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecefications<TKey, TEntity> spec, bool changeTracker = false);
        Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);
        Task<TEntity?> GetByIdAsync(ISpecefications<TKey, TEntity> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);   
        void Delete(TEntity entity);
    }
}
