namespace Aramis.Api.Repository.Models;

public partial class CobCuentum
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Saldo { get; set; }

    public virtual ICollection<CobTipoPago> CobTipoPagos { get; } = new List<CobTipoPago>();
}
