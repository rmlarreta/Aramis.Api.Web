using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class CobReciboDetalle
{
    public Guid Id { get; set; }

    public Guid ReciboId { get; set; }

    public decimal Monto { get; set; }

    public Guid Tipo { get; set; }

    public string Observacion { get; set; } = null!;

    public Guid? PosId { get; set; }

    public string? CodAut { get; set; }

    public virtual CobPo? Pos { get; set; }

    public virtual CobRecibo Recibo { get; set; } = null!;

    public virtual CobTipoPago TipoNavigation { get; set; } = null!;
}
