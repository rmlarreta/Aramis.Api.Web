using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aramis.Api.SecurityService.Application
{
    internal class RoleService
    {
        #region ROLES
        public void DeleteRole(string id)
        {
            _rolesRepository.Delete(_rolesRepository.GetById(Guid.Parse(id)));
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            List<SecRole>? roles = _rolesRepository.GetAll();
            return _mapper.Map<List<SecRole>, List<RoleDto>>(roles);
        }

        public RoleDto GetRoleById(string id)
        {
            SecRole? role = _rolesRepository.GetById(Guid.Parse(id));
            return _mapper.Map<SecRole, RoleDto>(role);
        }

        public RoleDto GetRoleByName(string name)
        {
            SecRole? role = _rolesRepository.GetByName(name);
            return _mapper.Map<SecRole, RoleDto>(role);
        }

        public void UpdateRole(RoleDto roledto)
        {
            SecRole? role = _mapper.Map<RoleDto, SecRole>(roledto);
            _rolesRepository.Update(role);
        }

        public void CreateRole(RoleDto roledto)
        {
            SecRole? role = _mapper.Map<RoleDto, SecRole>(roledto);
            _rolesRepository.Add(role);
        }
        #endregion ROLES
    }
}
