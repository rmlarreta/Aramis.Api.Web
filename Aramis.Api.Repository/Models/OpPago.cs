using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class OpPago : Entity
{
    public DateTime Fecha { get; set; }

    public Guid Tipo { get; set; }

    public Guid Documento { get; set; }

    public string Operador { get; set; } = null!;

    public virtual OpDocumentoProveedor DocumentoNavigation { get; set; } = null!;

    public virtual CobTipoPago TipoNavigation { get; set; } = null!;
}
