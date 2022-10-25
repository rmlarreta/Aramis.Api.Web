using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AramisbdContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(AramisbdContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(string id)
        {
            return _dbSet.Find(id)!;
        }

        public void Delete(string id)
        {
            TEntity? dataToDelete = _dbSet.Find(id)!;
            _dbSet.Remove(dataToDelete);
        }

        public void Add(TEntity data)
        {
            _dbSet.Add(data);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }
    }
}
