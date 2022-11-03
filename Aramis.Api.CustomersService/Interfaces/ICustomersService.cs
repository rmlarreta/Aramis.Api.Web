using Aramis.Api.Commons.ModelsDto.Customers;

namespace Aramis.Api.CustomersService.Interfaces
{
    public interface ICustomersService
    {
        List<OpClienteDto> GetAll();
        OpClienteDto GetById(string id);
        bool Delete(string id);
        bool Update(OpClienteInsert entity);
        bool Insert(OpClienteInsert entity);
    }
}
