namespace Aramis.Api.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(Guid id);
        bool Add(TEntity data);
        bool Delete(Guid id);
        bool Update(TEntity data);
        bool Save();
    }
}
