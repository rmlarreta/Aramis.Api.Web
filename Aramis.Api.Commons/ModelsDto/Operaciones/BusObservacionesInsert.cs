namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusObservacionesInsert
    {
        public Guid Id { get; set; }

        public Guid OperacionId { get; set; }

        public DateTime Fecha { get; set; }

        public string Observacion { get; set; } = null!;

        public string Operador { get; set; } = null!;
    }
}
