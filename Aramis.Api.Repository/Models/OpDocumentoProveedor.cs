using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class OpDocumentoProveedor
{
    public Guid Id { get; set; }

    public Guid ProveedorId { get; set; }

    public DateTime Fecha { get; set; }

    public string Razon { get; set; } = null!;

    public Guid TipoDocId { get; set; }

    public Guid EstadoId { get; set; }

    public int Pos { get; set; }

    public int Numero { get; set; }

    public decimal Monto { get; set; }

    public virtual BusEstado Estado { get; set; } = null!;

    public virtual ICollection<OpPago> OpPagos { get; } = new List<OpPago>();

    public virtual OpCliente Proveedor { get; set; } = null!;

    public virtual BusOperacionTipo TipoDoc { get; set; } = null!;
}
