namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class ReciboDetallesInsert
    {
        public Guid? Id { get; set; } 
        public Guid? ReciboId { get; set; }

        public decimal Monto { get; set; }

        public Guid Tipo { get; set; }

        public string? Observacion { get; set; } = null!;

        public Guid? PosId { get; set; }

        public string? CodAut { get; set; }
    }
}
