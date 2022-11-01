using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Security
{
    public interface IRoleRepository
    {
        SecRole GetByName(string name);
        List<SecRole> GetAll();
        SecRole GetById(Guid id);
        bool Add(SecRole secRole);
        bool Update(SecRole secRole);
        bool Delete(SecRole secRole);
    }
}
