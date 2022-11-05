using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Customers
{
    public interface ICustomersRepository
    {
        List<OpCliente> GetAll();
        OpCliente Get(string id);
        bool Add(OpCliente opCliente);
        bool Update(OpCliente opCliente);
        bool Delete(OpCliente opCliente);
    }
}
