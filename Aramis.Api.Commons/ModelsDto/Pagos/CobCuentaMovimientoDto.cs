namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobCuentaMovimientoDto
    {
        public Guid Id { get; set; }

        public Guid Cuenta { get; set; }

        public bool Debito { get; set; }

        public bool Computa { get; set; }

        public string Detalle { get; set; } = null!;

        public decimal Monto { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Operador { get; set; }
    }
}
