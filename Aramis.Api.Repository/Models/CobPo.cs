using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class CobPo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CobReciboDetalle> CobReciboDetalles { get; } = new List<CobReciboDetalle>();
}
