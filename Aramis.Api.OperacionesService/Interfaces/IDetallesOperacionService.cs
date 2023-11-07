using Aramis.Api.Commons.ModelsDto.Operaciones;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public interface IDetallesOperacionService
    {
        Task<BusOperacionesDto> UpdateDetalles(List<BusDetalleOperacionBase> detalles);
        Task<BusOperacionesDto> InsertDetalles(List<BusDetalleOperacionBase> detalles);
        Task<BusOperacionesDto> DeleteDetalles(List<BusDetalleOperacionBase> detalles);
    }
}
