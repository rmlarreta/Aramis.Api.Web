namespace Aramis.Api.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(string id);
        bool Add(TEntity data);
        bool Delete(string id);
        bool Update(TEntity data);
        bool Save();
    }
}
