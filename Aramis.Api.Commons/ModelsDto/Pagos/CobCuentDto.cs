namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobCuentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Saldo { get; set; }
    }
}
