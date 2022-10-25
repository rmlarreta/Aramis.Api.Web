namespace Aramis.Api.Repository.Models;

public partial class SecUser
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Role { get; set; }

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public DateTime EndOfLife { get; set; }

    public bool Active { get; set; }

    public virtual SecRole RoleNavigation { get; set; } = null!;
}
