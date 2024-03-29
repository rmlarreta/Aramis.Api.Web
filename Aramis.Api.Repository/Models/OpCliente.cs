﻿using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class OpCliente : Entity
{
    public string Cui { get; set; } = null!;

    public Guid Resp { get; set; }

    public string Razon { get; set; } = null!;

    public Guid Gender { get; set; }

    public string Domicilio { get; set; } = null!;

    public Guid Pais { get; set; }

    public string? Contacto { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<BusOperacion> BusOperacions { get; } = new List<BusOperacion>();

    public virtual ICollection<CobRecibo> CobRecibos { get; } = new List<CobRecibo>();

    public virtual OpGender GenderNavigation { get; set; } = null!;

    public virtual ICollection<OpDocumentoProveedor> OpDocumentoProveedors { get; } = new List<OpDocumentoProveedor>();

    public virtual OpPai PaisNavigation { get; set; } = null!;

    public virtual OpResp RespNavigation { get; set; } = null!;
}
