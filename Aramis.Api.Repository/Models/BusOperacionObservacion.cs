using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class BusOperacionObservacion : Entity
{

    public Guid OperacionId { get; set; }

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;

    public string Operador { get; set; } = null!;

    public virtual BusOperacion Operacion { get; set; } = null!;
}
