﻿using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class CobReciboDetalle : Entity
{
    public Guid ReciboId { get; set; }

    public decimal Monto { get; set; }

    public Guid Tipo { get; set; }

    public string? Observacion { get; set; }

    public Guid? PosId { get; set; }

    public string? CodAut { get; set; }

    public bool? Cancelado { get; set; }

    public virtual CobPo? Pos { get; set; }

    public virtual CobRecibo Recibo { get; set; } = null!;

    public virtual CobTipoPago TipoNavigation { get; set; } = null!;
}
