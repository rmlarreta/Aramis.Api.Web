using Aramis.Api.Repository.Application.Commons;
using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class BusOperacionPago:Entity
{
   
    public Guid OperacionId { get; set; }

    public Guid ReciboId { get; set; }

    public virtual BusOperacion Operacion { get; set; } = null!;

    public virtual CobRecibo Recibo { get; set; } = null!;
}
