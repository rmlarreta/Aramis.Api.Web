using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly IRepository<SecRole> _roleRepository;
        private readonly AramisbdContext _aramisbdContext;
        public SecurityRepository(IRepository<SecRole> roleRepository, AramisbdContext aramisbdContext)
        {
            _roleRepository = roleRepository;
            _aramisbdContext = aramisbdContext;
        }

        #region Roles
        public bool AltaRole(SecRole role)
        {
            return _roleRepository.Add(role);
        }

        public SecRole GetRoleByName(string name)
        {
            return _aramisbdContext.SecRoles.SingleOrDefault(x => x.Name.Equals(name))!;
        }
        #endregion Roles

    }
}
