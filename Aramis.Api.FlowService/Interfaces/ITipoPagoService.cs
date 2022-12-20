using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface ITipoPagoService
    {
        CobTipoPagoDto Insert(CobTipoPagoDto cobTipoPago);
        CobTipoPagoDto Update(CobTipoPagoDto cobTipoPago);
        bool Delete(string id);
        CobTipoPagoDto GetById(string id);
        List<CobTipoPagoDto> GetAll(); 
        List<CobPosDto> GetPost(); 
    }
}
