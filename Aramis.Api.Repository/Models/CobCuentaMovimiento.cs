using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class CobCuentaMovimiento : Entity
{

    public Guid Cuenta { get; set; }

    public bool Debito { get; set; }

    public bool Computa { get; set; }

    public string Detalle { get; set; } = null!;

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public string Operador { get; set; } = null!;

    public virtual CobCuentum CuentaNavigation { get; set; } = null!;
}
