using Microsoft.EntityFrameworkCore;
using Store.G02.Persistence.Data.Contexts;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities;
using Store.G05.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repositries
{
    public class GenericRepoitry<TKey, TEntity>(StoreDbContext _context) : IGenericRepositry<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return changeTracker ?
                    await _context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync() as IEnumerable<TEntity>
                    : await _context.Products.Include(P => P.Brand).Include(P => P.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }

            return changeTracker ?
                await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey key)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == key as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(key);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}
