namespace Aramis.Api.Repository.Models;

public partial class CobReciboNumero
{
    public Guid Id { get; set; }

    public Guid OperacionId { get; set; }

    public int Numero { get; set; }

    public virtual CobRecibo Operacion { get; set; } = null!;
}
