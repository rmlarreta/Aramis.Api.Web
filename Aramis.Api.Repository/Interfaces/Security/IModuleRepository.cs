using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Security
{
    public interface IModuleRepository
    {
        SecModule GetByName(string name);
        List<SecModule> GetAll();
        SecModule GetById(string id);
        bool Add(SecModule secModule);
        bool Update(SecModule secModule);
        bool Delete(SecModule secModule);
    }
}
