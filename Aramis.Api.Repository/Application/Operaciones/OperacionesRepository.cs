using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Operaciones
{
    public class OperacionesRepository : IOperacionesRepository
    {
        private readonly IGenericRepository<BusOperacion> _operacionRespository;
        private readonly AramisbdContext _context;
        public OperacionesRepository(IGenericRepository<BusOperacion> operacionRespository, AramisbdContext context)
        {
            _operacionRespository = operacionRespository;
            _context = context;
        }

        public BusOperacion Get(string id)
        {
            return _context.BusOperacions
                 .Include(x => x.Cliente)
                 .Include(x => x.Cliente.RespNavigation)
                 .Include(x => x.TipoDoc)
                 .Include(x => x.Estado)
                 .Include(x => x.BusOperacionDetalles)
                 .Include(x => x.BusOperacionObservacions)
                 .Where(x => x.Id.Equals(Guid.Parse(id)))
                 .FirstOrDefault()!;
        }

        public bool Insert(BusOperacion entity)
        {
            return _operacionRespository.Add(entity);
        }
    }
}
