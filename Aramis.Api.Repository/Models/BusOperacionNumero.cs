using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class BusOperacionNumero : Entity
{

    public Guid OperacionId { get; set; }

    public int Numero { get; set; }

    public virtual BusOperacion Operacion { get; set; } = null!;
}
