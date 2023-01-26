namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobReciboDetalleDto
    {
        public Guid Id { get; set; }

        public Guid ReciboId { get; set; }

        public decimal Monto { get; set; }

        public Guid Tipo { get; set; }

        public string? Observacion { get; set; }

        public Guid? PosId { get; set; }

        public string? CodAut { get; set; }
    }
}
