
using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Ordenes;

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

        #region Remitos
        BusOperacionesDto NuevoRemito(string id);
        #endregion

        #region #Ordenes
        BusOrdenesTicketDto NuevaOrden(string id);
        #endregion
        #region Observas
        bool InsertObservacion(BusObservacionesDto observacion);
        bool DeleteObservacion(string id);
        bool UpdateObservacion(BusObservacionesDto observacion);

        #endregion Observas
    }
}
