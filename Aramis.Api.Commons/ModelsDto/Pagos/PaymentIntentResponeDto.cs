namespace Aramis.Api.Commons.ModelsDto.Pagos
{

    public class PaymentIntentResponeDto
    {
        public AddionalInfo? Additional_info { get; set; }
        public int Amount { get; set; }
        public string? Id { get; set; }
        public string? Device_id { get; set; }
    }
}
