namespace Aramis.Api.Repository.Models;

public partial class SecModuleAction
{
    public Guid Id { get; set; }

    public Guid ActionId { get; set; }

    public Guid ModuleId { get; set; }

    public virtual SecAction Action { get; set; } = null!;

    public virtual SecModule Module { get; set; } = null!;

    public virtual ICollection<SecRoleModuleAction> SecRoleModuleActions { get; } = new List<SecRoleModuleAction>();
}
