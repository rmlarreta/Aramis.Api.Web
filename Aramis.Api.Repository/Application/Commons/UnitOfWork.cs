using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly AramisbdContext _modelContext;

        public UnitOfWork(AramisbdContext modelContext)
        {
            _modelContext = modelContext;
        }
        public AramisbdContext GetContext<T>() where T : class, IEntity
        {
            return GetModelContext();
        }

        public AramisbdContext GetModelContext()
        {

            return _modelContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return new Repository<T>(GetContext<T>());
        }
        int IUnitOfWork.SaveChanges<T>()
        {
            return GetContext<T>().SaveChanges();
        }

        async Task<int> IUnitOfWork.SaveChangesAsync<T>()
        {
            return await GetContext<T>().SaveChangesAsync();
        }
        public bool IsDisposed()
        {
            return _disposed;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _modelContext.Dispose();
            }
            _disposed = true;
        }
    }
}
