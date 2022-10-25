namespace Aramis.Api.Repository.Models;

public partial class SecRoleModuleAction
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public Guid ModuleActionId { get; set; }

    public virtual SecModuleAction ModuleAction { get; set; } = null!;

    public virtual SecRole Role { get; set; } = null!;
}
