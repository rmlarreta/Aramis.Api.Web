using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.SecurityService.Interfaces
{
    public interface ISecurityService
    {
        //USERS
        UserAuth Authenticate(string username, string password);
        UserAuth ChangePassword(string username, string password, string npassword);
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(string id);
        UserDto GetUserByName(string name);
        UserDto CreateUser(SecUser user, string password);
        UserDto UpdateUser(UserDto user);
        void DeleteUser(string id);

        //ROLES
        IEnumerable<RoleDto> GetAllRoles();
        RoleDto GetRoleById(string id);
        RoleDto GetRoleByName(string name);
        void CreateRole(RoleDto role);
        void UpdateRole(RoleDto role);
        void DeleteRole(string id);

        //CLAIMS
        public string GetUserAuthenticated();
        public string GetPerfilAuthenticated();
    }
}
