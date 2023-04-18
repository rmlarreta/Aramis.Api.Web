namespace Aramis.Api.Commons.ModelsDto.Suppliers
{
    public class OpDocumentoProveedorDto
    {
        public Guid? Id { get; set; }

        public Guid ProveedorId { get; set; }

        public DateTime Fecha { get; set; }

        public string Razon { get; set; } = null!;

        public Guid TipoDocId { get; set; }

        public Guid? EstadoId { get; set; }

        public int Pos { get; set; }

        public int Numero { get; set; }

        public decimal Monto { get; set; }

    }

}
