using Aramis.Api.Repository.Application.Operaciones;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Interfaces.Reports;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Reports
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly AramisbdContext _context;
        private IOperacionesRepository _operacionsRepository = null!;
        private IGenericRepository<SystemEmpresa> _empresaRepository = null!;
        public ReportsRepository(AramisbdContext context)
        {
            _context = context;
        }
        public IOperacionesRepository Operacions => _operacionsRepository ??= new OperacionesRepository(_context);

        public IGenericRepository<SystemEmpresa> Empresa => _empresaRepository ??= new GenericRepository<SystemEmpresa>(_context);
    }
}
