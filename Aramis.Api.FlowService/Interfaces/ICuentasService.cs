using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface ICuentasService
    {
        CobCuentum Insert(CobCuentum cobCuentum);
        CobCuentum Update(CobCuentum cobCuentum);
        bool Delete(string id);
        List<CobCuentum> GetAll();
        CobCuentum GetById(string id);
    }
}
