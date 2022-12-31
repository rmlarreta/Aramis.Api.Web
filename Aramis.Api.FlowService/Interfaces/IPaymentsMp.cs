using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IPaymentsMp
    {
        Task<PaymentIntentResponseDto>? CreatePaymentIntent(PaymentIntentDto PaymentIntent, string PosId);
        Task<CancelIntentPayDto> CancelPaymentIntent(string PaymentIntent, string id);
        Task<StateIntentPayDto> StatePaymentIntent(string paymentIntentId, string id);
        Task Getpaymentintentlist(string id);
    }
}
