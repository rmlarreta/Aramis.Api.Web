using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Ordenes; 

namespace Aramis.Api.OperacionesService.Interfaces
{
    public interface IOperacionesService
    {
        BusOperacionesDto NuevaOperacion(string operador);
        BusOperacionesDto UpdateOperacion(BusOperacionesInsert busoperacionesinsert);
        BusOperacionesDto GetOperacion(string id);
        bool DeleteOperacion(string operacionid);
        BusOperacionesDto InsertDetalle(BusDetalleOperacionesInsert detalle);
        BusOperacionesDto DeleteDetalle(string id);
        BusOperacionesDto UpdateDetalle(BusDetalleOperacionesInsert detalle);

        #region Presupuestos
        List<BusOperacionesDto> Presupuestos();
        #endregion
        #region Remitos
        BusOperacionesDto NuevoRemito(string id);
        List<BusOperacionesDto> RemitosPendientes();

        #endregion

        #region #Ordenes
        BusOrdenesTicketDto NuevaOrden(string id);
        List<BusOperacionesDto> OrdenesByEstado(string estado);

        #endregion
        #region Observas
        bool InsertObservacion(BusObservacionesDto observacion);
        bool DeleteObservacion(string id);
        bool UpdateObservacion(BusObservacionesDto observacion);

        #endregion Observas

        #region Utils
        bool OperacionEstado(string id, string status);
        List<BusOperacionTipoDto> TipoOperacions();
        List<BusEstadoDto> Estados();
        #endregion
    }
}
