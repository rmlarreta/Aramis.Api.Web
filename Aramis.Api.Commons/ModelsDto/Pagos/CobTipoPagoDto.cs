namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobTipoPagoDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid? CuentaId { get; set; }

    }
}
