using Aramis.Api.Repository.Application.Operaciones;
using Aramis.Api.Repository.Application.Recibos;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Pagos
{
    public class PagosRepository : IPagosRepository
    {
        private readonly AramisbdContext _context;
        private IRecibosRepository _cobRecibosRepository = null!;
        private IGenericRepository<CobCuentum> _cobCuentumRepository = null!;
        private IGenericRepository<CobCuentaMovimiento> _cobCuentaMovimientoRepository = null!;
        private IGenericRepository<CobTipoPago> _cobTipoPagoRepository = null!;
        private IOperacionesRepository _busOperacionRepository = null!;
        private IGenericRepository<BusEstado> _busEstadosRepository = null!;
        private IOperacionPagosRepository _operacionPagosRepository = null!;
        public PagosRepository(AramisbdContext context)
        {
            _context = context; 
        }

        public IRecibosRepository Recibos => _cobRecibosRepository ??= new RecibosRepository(_context);

        public IGenericRepository<CobCuentum> Cuentas => _cobCuentumRepository ??= new GenericRepository<CobCuentum>(_context);

        public IGenericRepository<CobTipoPago> TipoPagos => _cobTipoPagoRepository ??= new GenericRepository<CobTipoPago>(_context);

        public IOperacionesRepository Operaciones => _busOperacionRepository ??= new OperacionesRepository(_context);

        public IGenericRepository<BusEstado> Estados => _busEstadosRepository ??= new GenericRepository<BusEstado>(_context);
        public IOperacionPagosRepository OperacionPagos => _operacionPagosRepository ??= new OperacionPagosRepository(_context);

        public IGenericRepository<CobCuentaMovimiento> CuentasMovimiento => _cobCuentaMovimientoRepository ??= new GenericRepository<CobCuentaMovimiento>(_context);

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
