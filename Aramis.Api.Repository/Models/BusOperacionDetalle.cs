using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class BusOperacionDetalle
{
    public Guid Id { get; set; }

    public Guid OperacionId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal Neto { get; set; }

    public decimal Internos { get; set; }

    public decimal Iva { get; set; }

    public decimal Facturado { get; set; }

    public virtual BusOperacion Operacion { get; set; } = null!;
}
