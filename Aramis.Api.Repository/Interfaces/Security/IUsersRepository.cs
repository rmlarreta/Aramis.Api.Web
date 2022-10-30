using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces
{
    public interface IUsersRepository
    {
        List<SecUser> GetAll();
        SecUser GetByName(string name);
        SecUser GetById(string id);
        bool Add(SecUser secUser);
        bool Update(SecUser secUser);
        bool Delete(SecUser secUser);
    }
}
