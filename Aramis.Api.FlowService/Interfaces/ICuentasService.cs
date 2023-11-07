using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Commons.ModelsDto.Suppliers;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface ICuentasService
    {
        Task<CobCuentDto> Insert(CobCuentDto cobCuentum);
        Task<CobCuentDto> Update(CobCuentDto cobCuentum);
        Task DebitarPago(OpDocumentProveedorPago documentProveedorPago);
        Task Delete(string id);
        List<CobCuentDto> GetAllCuentas();
        Task<CobCuentDto> GetById(string id);
    }
}
