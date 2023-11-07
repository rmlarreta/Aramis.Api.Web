using Aramis.Api.Commons.ModelsDto.Security;

namespace Aramis.Api.SecurityService.Interfaces
{
    public interface IRoleServices
    {
        IEnumerable<RoleDto> GetAllRoles();
        RoleDto GetRoleById(string id);
        RoleDto GetRoleByName(string name);
        void CreateRole(RoleDto role);
        void UpdateRole(RoleDto role);
        void DeleteRole(string id);
    }
}
