namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusObservacionesDto
    {
        public Guid Id { get; set; }=Guid.NewGuid();

        public Guid OperacionId { get; set; }

        public DateTime Fecha { get; set; }

        public string Observacion { get; set; } = null!;

        public string Operador { get; set; } = null!;
    }
}
