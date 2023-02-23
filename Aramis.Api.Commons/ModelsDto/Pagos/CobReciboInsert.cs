namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobReciboInsert
    {
        public Guid? Id { get; set; }
        public int? Numero { get; set; }
        public Guid ClienteId { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Operador { get; set; } = null!; 
        public List<CobReciboDetallesInsert>? Detalles { get; set; } = new List<CobReciboDetallesInsert>();
    }
}
