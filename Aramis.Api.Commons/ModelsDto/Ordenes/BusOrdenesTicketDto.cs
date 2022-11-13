using Aramis.Api.Repository.Models;

namespace Aramis.Api.Commons.ModelsDto.Ordenes
{
    public class BusOrdenesTicketDto
    {
        public Guid Id { get; set; }
        public string? Cui { get; set; }
        public string? Nombre { get; set; }
        public int? Numero { get; set; }
        public List<BusOperacionObservacion>? Observaciones { get; set; } = new List<BusOperacionObservacion>();
        public DateTime? Fecha { get; set; }
    }
}
