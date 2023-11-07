using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class BusEstado : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<BusOperacion> BusOperacions { get; } = new List<BusOperacion>();

    public virtual ICollection<OpDocumentoProveedor> OpDocumentoProveedors { get; } = new List<OpDocumentoProveedor>();
}
