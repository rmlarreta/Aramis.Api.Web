using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Pagos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AramisbdContext _context;
        private IGenericRepository<CobCuentum> _cobCuentumRepository=null!;
        private IGenericRepository<CobTipoPago> _cobTipoPagoRepository=null!;
        private IGenericRepository<BusOperacion> _busOperacionRepository = null!;
        private IGenericRepository<BusEstado> _busEstadosRepository = null!;
        private IGenericRepository<BusOperacionPago> _operacionPagosRepository=null!;
        public UnitOfWork(AramisbdContext context)
        {
            _context = context;
        }
        public IGenericRepository<CobCuentum> Cuentas
        {
            get
            {
                return _cobCuentumRepository ??= new GenericRepository<CobCuentum>(_context);
            }
        }

        public IGenericRepository<CobTipoPago> TipoPagos
        {
            get
            {
                return _cobTipoPagoRepository ??= new GenericRepository<CobTipoPago>(_context);
            }
        }

        public IGenericRepository<BusOperacion> Operaciones
        {
            get
            {
                return _busOperacionRepository ??= new GenericRepository<BusOperacion>(_context);
            }
        }

        public IGenericRepository<BusEstado> Estados
        {
            get
            {
                return _busEstadosRepository ??= new GenericRepository<BusEstado>(_context);
            }
        }
        public IGenericRepository<BusOperacionPago> OperacionPagos
        {
            get
            {
                return _operacionPagosRepository ??= new GenericRepository<BusOperacionPago>(_context);
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
