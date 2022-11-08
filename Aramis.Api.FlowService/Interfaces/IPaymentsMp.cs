using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IPaymentsMp
    {
        Task<PaymentIntentResponseDto>? CreatePaymentIntent(PaymentIntentDto PaymentIntent, string id);
        Task<CancelIntentPayDto> CancelPaymentIntent(string PaymentIntent, string id);
        Task<StateIntentPayDto> StatePaymentIntent(string paymentIntentId, string id);
        Task Getpaymentintentlist(string id);
    }
}
