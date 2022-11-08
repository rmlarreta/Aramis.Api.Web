using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IPagosService
    {
        bool NuevoPago(PagoInsert pago);
    }
}
