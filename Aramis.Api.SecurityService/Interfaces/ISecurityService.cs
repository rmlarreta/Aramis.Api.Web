using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.ModelsDto;

namespace Aramis.Api.SecurityService.Interfaces
{
    public interface ISecurityService
    {
        UserAuth Authenticate(string username, string password);
        void ChangePassword(string username, string password, string npassword);
        IEnumerable<SecUser> GetAllUsers();
        SecUser GetUserById(string id);
        void CreateUser(SecUser user, string password);
        void UpdateUser(SecUser user);
        void DeleteUser(string id);
    }
}
