using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Pagos
{
    public interface IOperacionPagosRepository  
    {
        void Add(BusOperacionPago busOperacionPago);

        List<BusOperacionPago> Get();
    }
}
