using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Operaciones
{
    public interface IOperacionesRepository
    {
        bool Insert(BusOperacion entity);
        BusOperacion Get(string id);
    }
}
