using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class BusOperacionNumero
{
    public Guid Id { get; set; }

    public Guid OperacionId { get; set; }

    public int Numero { get; set; }

    public virtual BusOperacion Operacion { get; set; } = null!;
}
