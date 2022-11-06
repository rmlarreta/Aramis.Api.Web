namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class PaymentIntentDto
    {
        public AddionalInfo? Additional_info { get; set; }
        public int Amount { get; set; }
    }

    public class AddionalInfo
    {
        public string? External_reference { get; set; }
        public bool Print_on_terminal { get; set; }
        public string? Ticket_number { get; set; }
    }
}
