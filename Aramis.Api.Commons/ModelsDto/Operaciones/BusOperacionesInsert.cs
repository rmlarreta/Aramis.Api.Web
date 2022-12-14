namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusOperacionesInsert
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public int? Numero { get; set; } = 0;
        public Guid? ClienteId { get; set; }

        public DateTime? Fecha { get; set; }

        public DateTime? Vence { get; set; }

        public string? Razon { get; set; } = null!;

        public string? CodAut { get; set; } = null!;

        public Guid? TipoDocId { get; set; }

        public Guid? EstadoId { get; set; }

        public int? Pos { get; set; }

        public string Operador { get; set; } = null!;
    }
}
