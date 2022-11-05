namespace Aramis.Api.Repository.Models;

public partial class CobRecibo
{
    public Guid Id { get; set; }

    public Guid ClienteId { get; set; }

    public DateTime Fecha { get; set; }

    public string CodAut { get; set; } = null!;

    public string Operador { get; set; } = null!;

    public virtual ICollection<BusOperacionPago> BusOperacionPagos { get; } = new List<BusOperacionPago>();

    public virtual OpCliente Cliente { get; set; } = null!;

    public virtual ICollection<CobReciboDetalle> CobReciboDetalles { get; } = new List<CobReciboDetalle>();
}
