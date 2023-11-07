using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class SecRole : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<SecUser> SecUsers { get; } = new List<SecUser>();
}
