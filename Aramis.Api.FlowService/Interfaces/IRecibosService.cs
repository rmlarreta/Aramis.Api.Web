using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IRecibosService
    {
        CobReciboInsert InsertRecibo(CobReciboInsert recibo);

        Task<PaymentIntentResponseDto> PagoMP(PaymentIntentDto intent, string PosId); 

    }
}
