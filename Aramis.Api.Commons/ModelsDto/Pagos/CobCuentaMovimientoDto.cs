namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobCuentaMovimientoDto
    {
        public string? Id { get; set; }

        public string? Cuenta { get; set; }

        public bool Debito { get; set; }

        public bool Computa { get; set; }

        public string Detalle { get; set; } = null!;

        public decimal Monto { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Operador { get; set; }
    }
}
