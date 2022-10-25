namespace Aramis.Api.Repository.Models;

public partial class SecModule
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SecModuleAction> SecModuleActions { get; } = new List<SecModuleAction>();
}
