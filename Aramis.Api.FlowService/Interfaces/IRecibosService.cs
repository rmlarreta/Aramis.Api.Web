using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IRecibosService
    {
        string InsertRecibo(ReciboInsert recibo);

        public Task<PaymentIntentResponseDto> PagoMP(PaymentIntentDto intent, string point);

    }
}
