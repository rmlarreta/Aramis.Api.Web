
using Aramis.Api.Commons.ModelsDto.Operaciones;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public interface IOperacionesService
    {
        BusOperacionesDto NuevaOperacion(BusOperacionesInsert busoperacionesinsert);
        BusOperacionesDto GetOperacion(string id);
        bool DeleteOperacion(string operacionid);
        BusOperacionesDto InsertDetalle(BusDetalleOperacionesInsert detalle);
        BusOperacionesDto DeleteDetalle(string id);
        BusOperacionesDto UpdateDetalle(BusDetalleOperacionesInsert detalle);
    }
}
