
using Aramis.Api.Commons.ModelsDto.Operaciones;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public interface IOperacionesService
    {
        BusOperacionesDto NuevaOperacion(BusOperacionesInsert busoperacionesinsert);
        BusOperacionesDto UpdateOperacion(BusOperacionesInsert busoperacionesinsert);
        BusOperacionesDto GetOperacion(string id);
        bool DeleteOperacion(string operacionid);
        BusOperacionesDto InsertDetalle(BusDetalleOperacionesInsert detalle);
        BusOperacionesDto DeleteDetalle(string id);
        BusOperacionesDto UpdateDetalle(BusDetalleOperacionesInsert detalle);

        #region Observas
        bool InsertObservacion(BusObservacionesInsert observacion);
        bool DeleteObservacion(string id);
        bool UpdateObservacion(BusObservacionesInsert observacion);

        #endregion Observas
    }
}
