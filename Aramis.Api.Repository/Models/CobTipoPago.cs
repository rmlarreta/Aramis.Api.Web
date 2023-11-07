using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class CobTipoPago : Entity
{
    public string Name { get; set; } = null!;

    public Guid? CuentaId { get; set; }

    public virtual ICollection<CobReciboDetalle> CobReciboDetalles { get; } = new List<CobReciboDetalle>();

    public virtual CobCuentum? Cuenta { get; set; }

    public virtual ICollection<OpPago> OpPagos { get; } = new List<OpPago>();
}
