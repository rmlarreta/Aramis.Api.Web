using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface ITipoPagoService
    {
        CobTipoPago Insert(CobTipoPago cobTipoPago);
        CobTipoPago Update(CobTipoPago cobTipoPago);
        bool Delete(string id );
        CobTipoPago GetById(string id);
        List<CobTipoPago> GetAll();
        
    }
}
