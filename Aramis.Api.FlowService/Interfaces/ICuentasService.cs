using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface ICuentasService
    {
        CobCuentDto Insert(CobCuentDto cobCuentum);
        CobCuentDto Update(CobCuentDto cobCuentum);
        bool Delete(string id);
        List<CobCuentDto> GetAll();
        CobCuentDto GetById(string id);
    }
}
