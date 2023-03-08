namespace Aramis.Api.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(Guid id);
        void Add(TEntity data);
        void Add(List<TEntity> data);
        void Delete(Guid id);
        void Update(TEntity data);
        void UpdateRange(List<TEntity> data);
        bool Save();
    }
}
