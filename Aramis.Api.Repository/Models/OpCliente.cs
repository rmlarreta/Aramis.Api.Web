using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class OpCliente
{
    public Guid Id { get; set; }

    public string Cui { get; set; } = null!;

    public Guid Resp { get; set; }

    public string Razon { get; set; } = null!;

    public Guid Gender { get; set; }

    public string Domicilio { get; set; } = null!;

    public Guid Pais { get; set; }

    public string? Contacto { get; set; }

    public string? Mail { get; set; }

    public virtual OpGender GenderNavigation { get; set; } = null!;

    public virtual OpPai PaisNavigation { get; set; } = null!;

    public virtual OpResp RespNavigation { get; set; } = null!;
}
