using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Repositries;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        //private Dictionary<string, object> _repositries = new Dictionary<string, object>();
        private ConcurrentDictionary<string, object> _repositries = new ConcurrentDictionary<string, object>();


        //public IGenericRepositry<TKey, TEntity> GetRepositry<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        //{
        //    var type = typeof(TEntity).Name;

        //    if (!_repositries.ContainsKey(type))
        //    {
        //        var repositry = new GenericRepoitry<TKey, TEntity>(_context);
        //        _repositries.Add(type, repositry);
        //    }

        //    return (IGenericRepositry<TKey, TEntity>)_repositries[type];
        //}
        public IGenericRepositry<TKey, TEntity> GetRepositry<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {
            return (IGenericRepositry<TKey, TEntity>) _repositries.GetOrAdd(typeof(TEntity).Name, new GenericRepoitry<TKey, TEntity>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
