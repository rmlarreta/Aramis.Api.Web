namespace Aramis.Api.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(Guid id);
        void Add(TEntity data);
        void Delete(Guid id);
        void Update(TEntity data);
        bool Save();
    }
}
