using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities;
using System.Linq.Expressions;

namespace Store.G05.Services.Specifications
{
    public class BaseSpecifications<TKey, TEntity> : ISpecefications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }

        public void ApplyPagination(int pageSize, int pageIndex)
        {
            // PageIndex = 3
            // PageSize = 5
            // Skip = [(PageIndex - 1) * PageSize] = ( 3 - 1) * 5
            // Take = PageSize

            IsPagination = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }

        public void AddOrderBy(Expression<Func<TEntity, object>>? expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDescending(Expression<Func<TEntity, object>>? expression)
        {
            OrderByDescending = expression;
        }
    }
}
