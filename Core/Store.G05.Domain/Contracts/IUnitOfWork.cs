using Store.G05.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Contracts
{
    public interface IUnitOfWork
    {
        // Generate Repositry

        IGenericRepositry<TKey, TEntity> GetRepositry<TKey, TEntity>() where TEntity  : BaseEntity<TKey>;

        // Save Changes

        Task<int> SaveChangesAsync();
    }
}
