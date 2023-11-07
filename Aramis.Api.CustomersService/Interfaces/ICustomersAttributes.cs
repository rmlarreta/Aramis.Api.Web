using Aramis.Api.Commons.ModelsDto.Customers;

namespace Aramis.Api.CustomersService.Interfaces
{
    public interface ICustomersAttributes
    {
        List<OpPaiDto> GetPaisList();
        OpPaiDto GetPais(Guid id);
        OpGenderDto GetGender(Guid id);
        List<OpGenderDto> GetGenderList();
        OpRespDto GetResp(Guid id);
        List<OpRespDto> GetRespList();
    }
}
