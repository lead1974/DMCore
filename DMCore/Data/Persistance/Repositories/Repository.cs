using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DMCore.Data.Core.Repositories;
using DMCore.Data.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DMCore.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DMDbContext _context;

        public Repository(DMDbContext context)
        {
            _context = context;

        }


        public Task<TEntity> Get(long Id)
        {
            return  _context.Set<TEntity>().FindAsync(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        
        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<int> GetCount()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<bool> Exist(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id) != null;
        }
        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
        public async void Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async void AddRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Remove(long Id)
        {
            var entity = _context.Set<TEntity>().Find(Id);
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }


        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }
    }
}
