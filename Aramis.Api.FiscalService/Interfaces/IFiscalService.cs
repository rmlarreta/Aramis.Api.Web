using Aramis.Api.Commons.ModelsDto.Operaciones;

namespace Aramis.Api.FiscalService.Interfaces
{
    public interface IFiscalService
    {
        Task<BusOperacionesDto> GenerarFactura(List<BusDetalleOperacionesInsert> busDetalles);
    }
}
