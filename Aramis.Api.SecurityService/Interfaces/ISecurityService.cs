
using Aramis.Api.Repository.Models;

namespace Aramis.Api.SecurityService.Interfaces
{
    public interface ISecurityService
    {
        SecUser Authenticate(string username, string password);
        SecUser ChangePassword(string username, string password, string npassword);
        IEnumerable<SecUser> GetAll();
        SecUser GetById(string id);
        SecUser Create(SecUser user, string password);
        void Update(SecUser user, string? password);
        void Delete(string id);
    }
}
