using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Customers
{
    public interface ICustomersAttributesRepository
    {
        List<OpPai> GetPaisList();
        OpPai GetPais(string id);
        OpGender GetGender(string id);
        List<OpGender> GetGenderList();
        OpResp GetResp(string id);
        List<OpResp> GetRespList();
    }
}
