namespace Aramis.Api.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(string id);
        bool Add(TEntity data);
        bool Delete(string id);
        bool Update(TEntity data);
        bool Save();
    }
}
