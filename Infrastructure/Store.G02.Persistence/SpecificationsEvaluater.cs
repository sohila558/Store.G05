using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public static class SpecificationsEvaluater<TEntity>
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(ISpecefications<TEntity, TKey> spec) where TEntity : BaseEntity<TKey>
        {

        }
    }
}
