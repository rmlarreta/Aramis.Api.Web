using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Commons
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity;

        AramisbdContext GetContext<T>() where T : class, IEntity;

        AramisbdContext GetModelContext();

        int SaveChanges<T>() where T : class, IEntity;    

        Task<int> SaveChangesAsync<T>() where T : class, IEntity;

        bool IsDisposed();

    }
}
