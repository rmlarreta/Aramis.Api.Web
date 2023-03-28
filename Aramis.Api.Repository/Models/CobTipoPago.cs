using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class CobTipoPago
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? CuentaId { get; set; }

    public virtual ICollection<CobReciboDetalle> CobReciboDetalles { get; } = new List<CobReciboDetalle>();

    public virtual CobCuentum? Cuenta { get; set; }

    public virtual ICollection<OpPago> OpPagos { get; } = new List<OpPago>();
}
