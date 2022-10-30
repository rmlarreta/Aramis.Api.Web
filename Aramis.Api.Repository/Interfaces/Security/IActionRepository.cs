using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Security
{
    public interface IActionRepository
    {
        SecAction GetByName(string name);
        List<SecAction> GetAll();
        SecAction GetById(string id);
        bool Add(SecAction secAction);
        bool Update(SecAction secAction);
        bool Delete(SecAction secAction);
    }
}
