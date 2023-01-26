namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobReciboDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public string Operador { get; set; } = null!;

        public int Numero { get; set; }

        public List<CobReciboDetalleDto>? Detalles { get; set; } = new List<CobReciboDetalleDto>();
    }
}
