namespace Aramis.Api.Repository.Models;

public partial class CobPo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? DeviceId { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<CobReciboDetalle> CobReciboDetalles { get; } = new List<CobReciboDetalle>();
}
