using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IPagosService
    {
        Task<bool> NuevoPago(PagoInsert pago);

        ConciliacionCliente ConciliacionCliente(string clienteId);
        bool ImputarRecibo(string reciboId);
    }
}