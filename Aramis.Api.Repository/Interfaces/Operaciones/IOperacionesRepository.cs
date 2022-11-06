using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Operaciones
{
    public interface IOperacionesRepository
    {
        bool Insert(BusOperacion entity);
        bool Update(BusOperacion entity);
        BusOperacion Get(string id);
        bool DeleteDetalles(List<BusOperacionDetalle> detalles);
        bool DeleteOperacion(string operacion);
    }
}
