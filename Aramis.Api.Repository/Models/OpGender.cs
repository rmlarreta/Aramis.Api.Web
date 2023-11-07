using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class OpGender : Entity
{  
    public string Name { get; set; } = null!;

    public virtual ICollection<OpCliente> OpClientes { get; } = new List<OpCliente>();
}
