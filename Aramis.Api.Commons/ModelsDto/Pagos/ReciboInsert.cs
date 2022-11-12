namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class ReciboInsert
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? Numero { get; set; }
        public Guid ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public string? Operador { get; set; } = null!;

        public List<ReciboDetallesInsert> Detalles { get; set; } = new List<ReciboDetallesInsert>();
    }
}
