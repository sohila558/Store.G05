using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities;
using System.Linq.Expressions;

namespace Store.G05.Services.Specifications
{
    public class BaseSpecifications<TKey, TEntity> : ISpecefications<TKey, TEntity> where TEntity : BaseEntity<TEntity>
    {
        public List<Expression<Func<TKey, object>>> Includes { get; set; } = new List<Expression<Func<TKey, object>>>();
        public Expression<Func<TKey, bool>>? Criteria { get; set; }
       
        public BaseSpecifications(Expression<Func<TKey, bool>>? expression)
        {
            Criteria = expression;
        }
    }
}
