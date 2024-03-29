﻿using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class SystemEmpresa : Entity
{
    public string Cuit { get; set; } = null!;

    public string Razon { get; set; } = null!;

    public string Domicilio { get; set; } = null!;

    public string Fantasia { get; set; } = null!;

    public string Iibb { get; set; } = null!;

    public DateTime Inicio { get; set; }

    public string Respo { get; set; } = null!;

    public int PtoVenta { get; set; }
}
