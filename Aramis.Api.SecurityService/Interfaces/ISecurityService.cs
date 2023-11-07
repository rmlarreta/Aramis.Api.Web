using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.SecurityService.Interfaces
{
    public interface ISecurityService
    {
        //USERS
        Task<UserAuth> Authenticate(string username, string password);
        Task<UserAuth> ChangePassword(string username, string password, string npassword);
        IEnumerable<UserAuth> GetAllUsers();
        Task<UserAuth> GetUserById(Guid id);
        Task<UserAuth> GetUserByName(string name);
        Task<UserAuth> CreateUser(UserInsertDto user);
        Task UpdateUser(UserBaseDto user);
        Task DeleteUser(Guid id);

        //CLAIMS
        public string GetUserAuthenticated();
        public string GetPerfilAuthenticated();
    }
}
