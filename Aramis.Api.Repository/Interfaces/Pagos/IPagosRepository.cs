using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Pagos
{
    public interface IPagosRepository
    {
        public IRecibosRepository Recibos { get; }
        public IGenericRepository<CobCuentum> Cuentas { get; }
        public IGenericRepository<CobTipoPago> TipoPagos { get; }
        public IOperacionesRepository Operaciones { get; }
        public IGenericRepository<BusEstado> Estados { get; }
        public IOperacionPagosRepository OperacionPagos { get; }
        public bool Save();
    }
}
