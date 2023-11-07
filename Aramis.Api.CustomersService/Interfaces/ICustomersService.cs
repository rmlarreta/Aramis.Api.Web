using Aramis.Api.Commons.ModelsDto.Customers;

namespace Aramis.Api.CustomersService.Interfaces
{
    public interface ICustomersService
    {
        Task<List<OpClienteDto>> GetAllClientes();
        Task<OpClienteDto> GetById(Guid id);
        Task<OpClienteDto> GetByCui(string cui);
        Task<int> DeleteCliente(Guid id);
        Task Update(OpClienteBase entity);
        Task Insert(OpClienteBase entity);
    }
}
