using Aramis.Api.Commons.ModelsDto.Operaciones;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public interface IOperacionesService
    {
        Task<BusOperacionesDto> NuevaOperacion(BusOperacionBaseDto? busoperacionesinsert, string operador);
        Task<BusOperacionesDto> UpdateOperacion(BusOperacionBaseDto busoperacionesinsert);
        Task<BusOperacionesDto> GetOperacion(Guid id);
        Task<int> DeleteOperacion(Guid id);
    }
}
