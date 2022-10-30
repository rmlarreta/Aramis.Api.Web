using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.ModelsDto;

namespace Aramis.Api.SecurityService.Interfaces
{
    public interface ISecurityService
    {
        //USERS
        UserAuth Authenticate(string username, string password);
        void ChangePassword(string username, string password, string npassword);
        IEnumerable<SecUser> GetAllUsers();
        SecUser GetUserById(string id);
        void CreateUser(SecUser user, string password);
        void UpdateUser(SecUser user);
        void DeleteUser(string id);

        //ROLES
        IEnumerable<SecRole> GetAllRoles();
        SecRole GetRoleById(string id);
        SecRole GetRoleByName(string name);
        void CreateRole(SecRole role);
        void UpdateRole(SecRole role);
        void DeleteRole(string id);

    }
}
