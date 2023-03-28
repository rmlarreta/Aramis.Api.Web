using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class BusEstado
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BusOperacion> BusOperacions { get; } = new List<BusOperacion>();

    public virtual ICollection<OpDocumentoProveedor> OpDocumentoProveedors { get; } = new List<OpDocumentoProveedor>();
}
