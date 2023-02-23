using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Pagos
{
    public class OperacionPagosRepository : GenericRepository<BusOperacionPago>, IOperacionPagosRepository
    {
        private readonly AramisbdContext _context;

        public OperacionPagosRepository(AramisbdContext context) : base(context)
        {
            _context = context;
        }

        public new void Add(BusOperacionPago busOperacionPago)
        {
            _context.Add(busOperacionPago); 
        }

        public new List<BusOperacionPago> Get()
        {
            return _context.BusOperacionPagos.ToList();
        }
    }
}
