using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class OpGender
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OpCliente> OpClientes { get; } = new List<OpCliente>();
}
