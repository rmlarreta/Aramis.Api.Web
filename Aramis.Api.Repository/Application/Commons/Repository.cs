using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aramis.Api.Repository.Application.Commons
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AramisbdContext _aramisbdContext;

        public Repository(AramisbdContext aramisbdContext)
        {
            _aramisbdContext = aramisbdContext;
        }

        public bool Any(Expression<Func<T, bool>> expression) => _aramisbdContext.Set<T>().Any(expression);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await _aramisbdContext.Set<T>().AnyAsync(expression);

        public void Add(T entity)
        {
            _aramisbdContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            if (_aramisbdContext.Entry(entity).State == EntityState.Detached)
            {
                _aramisbdContext.Set<T>().Attach(entity);
            }
            _aramisbdContext.Entry(entity).State = EntityState.Deleted;
            _aramisbdContext.Set<T>().Remove(entity);
        }

        public async Task<T> Get(Guid id)
        {
            return await _aramisbdContext.Set<T>().FindAsync(id);
        }
        public async Task<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _aramisbdContext.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(expression)!;
        }
        public async Task<T> Get(Guid id,Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _aramisbdContext.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
   
        public DbSet<T> GetAll()
        {
            return  _aramisbdContext.Set<T>();
        }

        public void Update(T entity)
        {
            _aramisbdContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> GetReloadAsync(Guid id)
        {
            var changedEntries = _aramisbdContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
            return await _aramisbdContext.Set<T>().FindAsync(id);
        } 
    }
}
