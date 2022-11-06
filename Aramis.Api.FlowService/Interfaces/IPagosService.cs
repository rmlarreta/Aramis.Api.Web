using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    internal interface IPagosService
    {
        bool NuevoPago(PagoInsert pago);
    }
}
