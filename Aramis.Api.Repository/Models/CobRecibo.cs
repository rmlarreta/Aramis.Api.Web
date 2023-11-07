using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class CobRecibo : Entity
{
    public Guid ClienteId { get; set; }

    public DateTime Fecha { get; set; }

    public string Operador { get; set; } = null!;

    public int Numero { get; set; }

    public virtual ICollection<BusOperacionPago> BusOperacionPagos { get; } = new List<BusOperacionPago>();

    public virtual OpCliente Cliente { get; set; } = null!;

    public virtual ICollection<CobReciboDetalle> CobReciboDetalles { get; } = new List<CobReciboDetalle>();
}
