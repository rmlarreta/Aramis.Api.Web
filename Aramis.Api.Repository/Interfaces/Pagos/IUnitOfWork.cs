using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Pagos
{
    public interface IUnitOfWork
    {
        public IGenericRepository<CobCuentum> Cuentas { get; }
        public IGenericRepository<CobTipoPago> TipoPagos { get; }
        public IGenericRepository<BusOperacion> Operaciones { get; }
        public IGenericRepository<BusEstado> Estados { get; }
        public IGenericRepository<BusOperacionPago> OperacionPagos { get; }
        public bool Save();
    }
}
