using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class CobCuentum : Entity
{

    public string Name { get; set; } = null!;

    public decimal Saldo { get; set; }

    public virtual ICollection<CobCuentaMovimiento> CobCuentaMovimientos { get; } = new List<CobCuentaMovimiento>();

    public virtual ICollection<CobTipoPago> CobTipoPagos { get; } = new List<CobTipoPago>();
}
