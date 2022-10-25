namespace Aramis.Api.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(string id);
        void Add(TEntity data);
        void Delete(string id);
        void Update(TEntity data);
        void Save();
    }
}
