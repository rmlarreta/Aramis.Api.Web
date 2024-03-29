﻿using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class StockProduct : Entity
{
    public decimal Cantidad { get; set; }

    public string Plu { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public Guid Rubro { get; set; }

    public Guid Iva { get; set; }

    public decimal Neto { get; set; }

    public decimal Internos { get; set; }

    public decimal Tasa { get; set; }

    public bool Servicio { get; set; }

    public virtual ICollection<BusOperacionDetalle> BusOperacionDetalles { get; } = new List<BusOperacionDetalle>();

    public virtual StockIva IvaNavigation { get; set; } = null!;

    public virtual StockRubro RubroNavigation { get; set; } = null!;
}
