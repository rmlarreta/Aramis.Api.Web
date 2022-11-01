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
        void CreateUser(SecUser user, string password);
        void UpdateUser(UserDto user);
        void DeleteUser(string id);

        //ROLES
        IEnumerable<SecRole> GetAllRoles();
        SecRole GetRoleById(Guid id);
        SecRole GetRoleByName(string name);
        void CreateRole(SecRole role);
        void UpdateRole(SecRole role);
        void DeleteRole(Guid id); 
    }
}
