namespace Aramis.Api.Repository.Models;

public partial class SecRole
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SecUser> SecUsers { get; } = new List<SecUser>();
}
