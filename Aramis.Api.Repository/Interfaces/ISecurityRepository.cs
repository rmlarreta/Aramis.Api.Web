using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces
{
    public interface ISecurityRepository
    {
        bool AltaRole(SecRole role);
        SecRole GetRoleByName(string name);

    }
}
