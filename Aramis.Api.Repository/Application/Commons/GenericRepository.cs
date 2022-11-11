using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AramisbdContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(AramisbdContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(Guid id)
        {
            return _dbSet.Find(id)!;
        }

        public void Delete(Guid id)
        {
            TEntity? dataToDelete = _dbSet.Find(id)!;
            _dbSet.Remove(dataToDelete);
        }
        public void Add(TEntity data)
        {
            _dbSet.Add(data);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }
    }
}
