namespace Aramis.Api.Repository.Models;

public partial class BusOperacionPago
{
    public Guid Id { get; set; }

    public Guid OperacionId { get; set; }

    public Guid ReciboId { get; set; }

    public virtual BusOperacion Operacion { get; set; } = null!;

    public virtual CobRecibo Recibo { get; set; } = null!;
}
