using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    // Generate Dynamic Query

    public static class SpecificationsEvaluater
    {
        public static IQueryable<TEntity> GetQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecefications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _context.Products

            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); //_context.Products.Where(P => P.id == 12)
            }

            // _context.Proucts.Where(P => P.id == 12).Include(P => P.Brand)
            // _context.Proucts.Where(P => P.id == 12).Include(P => P.Brand).Include(P => P.Type)
            query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));

            return query;
        }
    }
}
