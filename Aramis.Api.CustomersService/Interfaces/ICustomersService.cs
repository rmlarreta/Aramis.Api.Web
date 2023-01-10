using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.Repository.Interfaces.Customers;

namespace Aramis.Api.CustomersService.Interfaces
{
    public interface ICustomersService
    {
        List<OpClienteDto> GetAll();
        OpClienteDto GetById(string id);
        OpClienteDto GetByCui(string cui);
        bool Delete(string id);
        OpClienteDto Update(OpClienteInsert entity);
        OpClienteDto Insert(OpClienteInsert entity);
        ICustomersAttributesRepository Attributes { get; }

    }
}
